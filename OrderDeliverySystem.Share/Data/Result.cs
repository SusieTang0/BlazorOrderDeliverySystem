﻿using System.Text.Json.Serialization;

namespace OrderDeliverySystem.Share.Data
{
public class Result
{
    private readonly string error;

    internal Result(bool succeeded, string error = null)
    {
        this.Succeeded = succeeded;
        this.error = error;
    }

    public bool Succeeded { get; }

    public string Error
        => this.Succeeded ? null : this.error;

    public static Result Success
        => new Result(true);

    public static Result Failure(string error)
        => new Result(false, error);

    public static implicit operator Result(string error)
        => Failure(error);

    public static implicit operator bool(Result result)
        => result.Succeeded;
}

    public class Result<TData> : Result
    {
        private readonly TData data;

        [JsonConstructor]
        private Result(bool succeeded, TData data, string errors)
            : base(succeeded, errors)
            => this.data = data;

        public TData Data
            => this.Succeeded
                ? this.data
                : throw new InvalidOperationException(
                    $"{nameof(this.Data)} is not available with a failed result. Use {this.Error} instead.");

        public static Result<TData> SuccessWith(TData data)
            => new Result<TData>(true, data, string.Empty);

        public new static Result<TData> Failure(IEnumerable<string> errors)
            => new Result<TData>(false, default, string.Empty);

        public static implicit operator Result<TData>(string error)
            => Failure(new List<string> { error });

        public static implicit operator Result<TData>(List<string> errors)
            => Failure(errors);

        public static implicit operator Result<TData>(TData data)
            => SuccessWith(data);

        public static implicit operator bool(Result<TData> result)
            => result.Succeeded;
    }
}
