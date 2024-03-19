using API.DAL.Contracts;
using API.DAL.Repo;
using API.Features.Common;
using FluentValidation;

namespace API.Features.Directories.CreateDirectory;

public class Endpoint : IEndpoint
{
    private async Task LogicHandler(LogicTransaction<CreateDirectoryRq, CreateDirectoryRs> lt)
    {
        // Data Transaction
        var dtr = new DataTransactionReturn<DirectoryDo> { SpName = "[dbo].[CreateDirectorySp]" };
        dtr.Params.Add(nameof(lt.Request.ParentDirId), lt.Request?.ParentDirId);
        dtr.Params.Add(nameof(lt.Request.Name), lt.Request!.Name);
        dtr.Params.Add(nameof(lt.Request.Path), lt.Request!.Path);
        
        await lt.DataAccess!.ExecuteSP(dtr);

        // Logic Transaction
        if (dtr.Status is not DataTransaction.TransactionStatus.Failure)
        {
            var directoryDo = dtr.Results.FirstOrDefault();

            if (directoryDo is not null && directoryDo.Id is not null)
            {
                lt.Result = new CreateDirectoryRs
                {
                    Id = directoryDo.Id
                };

                lt.Status = LogicTransactionStatus.Created;
            }
            else
                lt.Status = LogicTransactionStatus.Conflict;
        }
    }
    
    public void Map(WebApplication app)
    {
        app.MapPost("api/v1/directories", async (CreateDirectoryRq rq, IValidator<CreateDirectoryRq> va, IDataAccess da) =>
        {
            var vaRs = va.ValidateAsync(rq);

            if (!vaRs.Result.IsValid)
                return Results.BadRequest(vaRs.Result.Errors);
            
            var lt = new LogicTransaction<CreateDirectoryRq, CreateDirectoryRs>
            {
                DataAccess = da,
                Request = rq
            };

            await LogicHandler(lt);

            switch (lt.Status)
            {
                case LogicTransactionStatus.Created:
                    return Results.CreatedAtRoute("GetDirectory", lt.Result);
                case LogicTransactionStatus.Conflict:
                    return Results.Conflict();
                case LogicTransactionStatus.InternalError:
                default:
                    return Results.Problem();
            }
        })
        .Produces<CreateDirectoryRs>(201)
        .Produces(400)
        .Produces(409)
        .Produces(500);
    }
}