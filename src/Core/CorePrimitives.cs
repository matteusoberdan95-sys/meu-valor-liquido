namespace MeuValorLiquido.Core.Errors
{
    public sealed record Error(string Code, string Message)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
    }
}

namespace MeuValorLiquido.Core.Results
{
    using MeuValorLiquido.Core.Errors;

    public class Result
    {
        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
            {
                throw new InvalidOperationException("A successful result cannot contain an error.");
            }

            if (!isSuccess && error == Error.None)
            {
                throw new InvalidOperationException("A failed result must contain an error.");
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        public static Result Success() => new(true, Error.None);

        public static Result Failure(Error error) => new(false, error);
    }

    public sealed class Result<T> : Result
    {
        private readonly T? value;

        private Result(T value)
            : base(true, Error.None)
        {
            this.value = value;
        }

        private Result(Error error)
            : base(false, error)
        {
        }

        public T Value => IsSuccess
            ? value!
            : throw new InvalidOperationException("Cannot access the value of a failed result.");

        public static Result<T> Success(T value) => new(value);

        public static new Result<T> Failure(Error error) => new(error);
    }
}

namespace MeuValorLiquido.Core.Domain
{
    public abstract class BaseEntity<TId>
        where TId : notnull
    {
        protected BaseEntity(TId id)
        {
            Id = id;
        }

        public TId Id { get; }

        public DateTimeOffset CreatedAt { get; protected set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset? UpdatedAt { get; protected set; }
    }

    public abstract class ValueObject
    {
        protected abstract IEnumerable<object?> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            return obj is ValueObject other
                && GetType() == other.GetType()
                && GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(1, (current, obj) => HashCode.Combine(current, obj));
        }
    }

    public abstract record DomainEvent(Guid Id, DateTimeOffset OccurredAt);
}

namespace MeuValorLiquido.Core.Money
{
    public readonly record struct Money
    {
        public Money(decimal amount, string currency = "BRL")
        {
            if (string.IsNullOrWhiteSpace(currency))
            {
                throw new ArgumentException("Currency is required.", nameof(currency));
            }

            Amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
            Currency = currency.ToUpperInvariant();
        }

        public decimal Amount { get; }

        public string Currency { get; }

        public static Money Zero(string currency = "BRL") => new(0m, currency);

        public static Money From(decimal amount, string currency = "BRL") => new(amount, currency);

        public static Money operator +(Money left, Money right)
        {
            EnsureSameCurrency(left, right);
            return new Money(left.Amount + right.Amount, left.Currency);
        }

        public static Money operator -(Money left, Money right)
        {
            EnsureSameCurrency(left, right);
            return new Money(left.Amount - right.Amount, left.Currency);
        }

        public static Money operator *(Money money, decimal multiplier)
        {
            return new Money(money.Amount * multiplier, money.Currency);
        }

        public override string ToString()
        {
            return Amount.ToString("C", CultureInfo.GetCultureInfo("pt-BR"));
        }

        private static void EnsureSameCurrency(Money left, Money right)
        {
            if (left.Currency != right.Currency)
            {
                throw new InvalidOperationException("Money values must use the same currency.");
            }
        }
    }
}

namespace MeuValorLiquido.Core.Percentage
{
    public readonly record struct Percentage
    {
        public Percentage(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; }

        public decimal AsRate => Value / 100m;

        public static Percentage FromPercent(decimal value) => new(value);

        public decimal ApplyTo(decimal amount) => decimal.Round(amount * AsRate, 2, MidpointRounding.AwayFromZero);
    }
}

namespace MeuValorLiquido.Core.DateRange
{
    public readonly record struct DateRange
    {
        public DateRange(DateOnly start, DateOnly end)
        {
            if (end < start)
            {
                throw new ArgumentException("End date must be greater than or equal to start date.", nameof(end));
            }

            Start = start;
            End = end;
        }

        public DateOnly Start { get; }

        public DateOnly End { get; }

        public int Days => End.DayNumber - Start.DayNumber + 1;
    }
}

namespace MeuValorLiquido.Core.Abstractions
{
    public interface IClock
    {
        DateTimeOffset UtcNow { get; }
    }

    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }

        string? UserId { get; }
    }

    public interface IEmailSender
    {
        Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default);
    }

    public sealed record EmailMessage(string To, string Subject, string Body);

    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public interface IAppDbContext : IUnitOfWork;
}
