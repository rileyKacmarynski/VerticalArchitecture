using Application.Abstractions;
using Application.Shared.ResultType;
using Dapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.GetProducts
{
    public class GetProductsUseCaseHandler : IUseCaseHandler<GetProductsUseCase, IEnumerable<ProductDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetProductsUseCaseHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<IEnumerable<ProductDto>>> Handle(GetProductsUseCase request, CancellationToken cancellationToken)
        {
            const string query = "SELECT * FROM Products";

            var connection = _sqlConnectionFactory.GetOpenConnection();
            var results = await connection.QueryAsync<ProductDto>(query);

            return Result.Ok(results);
        }
    }
}
