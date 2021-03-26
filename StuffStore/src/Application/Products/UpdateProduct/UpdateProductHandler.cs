using Application.Abstractions;
using Application.Shared.ResultType;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.UpdateProduct
{
    public class UpdateProductHandler : IUseCaseHandler<UpdateProductUseCase>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public UpdateProductHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result> Handle(UpdateProductUseCase request, CancellationToken cancellationToken)
        {
            const string sql =
                "UPDATE Products " +
                "SET (Name = @Name, Price = @Price) " +
                "WHERE Id = @Id";

            var connection = _sqlConnectionFactory.GetOpenConnection();
            await connection.ExecuteAsync(sql, new 
            { 
                request.Name, 
                request.Price,
                request.Id
            });

            return Result.Ok();
        }
    }

}

