using System;
using System.Collections.Generic;

namespace Application.Shared.ResultType
{
    public class Result
    {
        public string ErrorMessage { get; private set; }
        public ResultStatuses Status { get; private set; }
        public bool Success => Status == ResultStatuses.Ok;
        public bool Failure => !Success;
        public IReadOnlyCollection<ValidationError> ValidationErrors => _validationErrors.AsReadOnly();
        private readonly List<ValidationError> _validationErrors = new List<ValidationError>();

        protected Result(ResultStatuses status, string error)
        {
            Status = status;
            ErrorMessage = error;
        }

        public Result(ResultStatuses status, string error, IEnumerable<(string property, string message)> validationErrors) 
            : this(status, error)
        {
            foreach (var (property, message) in validationErrors)
            {
                AddValidationError(property, message);
            }
        }

        protected void AddValidationError(string identifier, string errorMessage)
        {
            _validationErrors.Add(new ValidationError
            {
                Identifier = identifier,
                Error = errorMessage
            });
        }

        public static Result Fail(string message) =>
            new Result(ResultStatuses.Error, message);

        public static Result<T> Fail<T>(string message) =>
            new Result<T>(default, ResultStatuses.Error, message);

        public static Result Ok() =>
            new Result(ResultStatuses.Ok, string.Empty);

        public static Result<T> Ok<T>(T value) =>
            new Result<T>(value, ResultStatuses.Ok, string.Empty);

        public static Result NotFound(string message) =>
            new Result(ResultStatuses.NotFound, message);

        public static Result<T> NotFound<T>(string message) =>
            new Result<T>(default, ResultStatuses.NotFound, message);

        // We need this one to make the compiler happy.
        public static Result Invalid(IEnumerable<(string, string)> validationErrors)
        {
            var errorMessage = $"Error validating.";
            return new Result(ResultStatuses.Invalid, errorMessage, validationErrors);
        }

        // We need this one to make the compiler happy.
        public static Result<T> Invalid<T>(IEnumerable<(string, string)> validationErrors)
        {
            var errorMessage = $"Error validating {typeof(T)}";
            return new Result<T>(ResultStatuses.Invalid, errorMessage, validationErrors);
        }
    }

    public class Result<T> : Result
    {
        private T _value;

        public T Value { get; }

        protected internal Result(T value, ResultStatuses status, string error)
            : base(status, error)
        {
            _value = value;
            Value = value;
        }

        public Result(ResultStatuses status, string errorMessage, IEnumerable<(string Property, string Message)> validationErrors)
            : base(status, errorMessage)
        {
            foreach(var (property, message) in validationErrors)
            {
                AddValidationError(property, message);
            }
        }


        // Not sure how I feel about this yet.
        // Leaning towards client apps looking at the status and deciding what to do
        // based on that
        public TResult GetValue<TResult>(Func<T, TResult> onSuccess, Func<Result, TResult> onFail)
        {
            return Success
                ? onSuccess(_value)
                : onFail(this);
        }
    }

    /// <summary>
    /// Marker interface for Result. This allows us to specify <c>T</c> as covariant.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //public interface IResult<out T>
    //{
    //    T Value { get; }

    //    TResult GetResult<TResult>(Func<T, TResult> onSuccess, Func<string, TResult> onFail);
    //}

    //public interface IResult
    //{
    //    string Error { get; }
    //    bool Failure { get; }
    //    ResultStatus Status { get; set; }
    //    bool Success { get; }
    //    IReadOnlyCollection<ValidationError> ValidationErrors { get; }

    //    void AddValidationError(string identifier, string errorMessage);
    //}
}
