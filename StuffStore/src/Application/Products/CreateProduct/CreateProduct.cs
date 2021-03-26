using Application.Abstractions;
using Application.Shared.ResultType;
using Dapper;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.CreateProduct
{
    public class CreateProduct
    {
        public class Handler : IUseCaseHandler<UseCase>
        {
            private readonly ISqlConnectionFactory _sqlConnectionFactory;

            public Handler(ISqlConnectionFactory sqlConnectionFactory)
            {
                _sqlConnectionFactory = sqlConnectionFactory;
            }

            public async Task<Result> Handle(UseCase request, CancellationToken cancellationToken)
            {
                const string sql =
                    "INSERT INTO Products (Name, Price)" +
                    "VALUES (@Name, @Price)";

                var connection = _sqlConnectionFactory.GetOpenConnection();
                await connection.ExecuteAsync(sql, new { request.Name, request.Price });

                return Result.Ok();
            }
        }

        public class UseCase : IUseCase
        {
            public string Name { get; init; }
            public decimal Price { get; init; }
        }

        public class Validator : AbstractValidator<UseCase>
        {
            public Validator()
            {
                RuleFor(u => u.Name).NotEmpty();
                RuleFor(u => u.Price)
                    .NotEmpty()
                    .GreaterThanOrEqualTo(0m);
            }
        }

    }
}
