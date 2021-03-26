using Application.Abstractions;
using Application.Shared.ResultType;
using Dapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.GetProduct
{
    public class GetProduct
    {
        public class UseCase : IUseCase<ProductDto>
        {
            public int Id { get; set; }
        }

        public class Handler : IUseCaseHandler<UseCase, ProductDto>
        {
            private readonly ISqlConnectionFactory _sqlConnectionFactory;

            public Handler(ISqlConnectionFactory sqlConnectionFactory)
            {
                _sqlConnectionFactory = sqlConnectionFactory;
            }

            public async Task<Result<ProductDto>> Handle(UseCase request, CancellationToken cancellationToken)
            {
                const string query =
                    "SELECT * FROM Products " +
                    "WHERE Id = @Id";              

                var connection = _sqlConnectionFactory.GetOpenConnection();
                var result = await connection.QueryAsync<ProductDto>(query, new { request.Id });

                return Result.Ok(result.First());
            }
        }
    }
}
