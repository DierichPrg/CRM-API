using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Domain.Interfaces
{
    internal enum ResultStatus : byte
    {
        Empty,
        IsOk,
        IsError,
    }

    /// <summary>Represents a returned value or an error.</summary>
    /// <typeparam name="T">The type of returned value.</typeparam>
    /// <typeparam name="TError">The type of error.</typeparam>
    [Serializable]
    public readonly struct Result<T, TError> : IEquatable<Result<T, TError>>
    {
        /// <summary>Represents an empty result.</summary>
        public static readonly Result<T, TError> Empty;

        private readonly T resultValue;
        private readonly TError errorValue;
        private readonly ResultStatus state;

        private Result(T resultValue)
        {
            this.resultValue = resultValue;
            errorValue = default!;
            state = ResultStatus.IsOk;
        }

        private Result(TError errorValue)
        {
            this.errorValue = errorValue;
            resultValue = default!;
            state = ResultStatus.IsError;
        }

        /// <summary>Gets the reference value for pattern matching usage.</summary>
        public object? Case
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get =>
                state switch
                {
                    ResultStatus.IsOk => resultValue,
                    ResultStatus.IsError => errorValue,
                    _ => null,
                };
        }

        /// <summary>Gets a value indicating whether current result is empty.</summary>
        public bool IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => state == ResultStatus.Empty;
        }

        /// <summary>Gets a value indicating whether a value was returned.</summary>
        public bool IsOk
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => state == ResultStatus.IsOk;
        }

        /// <summary>Gets a value indicating whether an error occurred.</summary>
        public bool IsError
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => state == ResultStatus.IsError;
        }

        public static implicit operator Result<T, TError>(T value) => Ok(value);

        public static implicit operator Result<T, TError>(TError error) => Error(error);

        public static bool operator !=(Result<T, TError> left, Result<T, TError> right) => !left.Equals(right);

        public static bool operator ==(Result<T, TError> left, Result<T, TError> right) => left.Equals(right);

        /// <summary>Creates a result based on the specified intance, allowing covariant static cast.</summary>
        /// <param name="result">The result to cast.</param>
        /// <typeparam name="TDerived">The type of derived value to cast.</typeparam>
        /// <typeparam name="TDerivedError">The type of derived error to cast.</typeparam>
        /// <returns>
        /// A new instance of the <see cref="Result{T,TError}"/> struct based on the contents of the specified instance.
        /// </returns>
        public static Result<T, TError> CastUp<TDerived, TDerivedError>(Result<TDerived, TDerivedError> result)
            where TDerived : class, T
            where TDerivedError : class, TError
        {
            return result.state switch
            {
                ResultStatus.IsOk => new(result.resultValue),
                ResultStatus.IsError => new(result.errorValue),
                _ => default,
            };
        }

        /// <summary>Creates a result based on the specified intance, allowing covariant static cast.</summary>
        /// <param name="result">The result to cast value.</param>
        /// <typeparam name="TDerived">The type of derived value to cast.</typeparam>
        /// <returns>
        /// A new instance of the <see cref="Result{T,TError}"/> struct based on the contents of the specified instance.
        /// </returns>
        public static Result<T, TError> CastUpValue<TDerived>(Result<TDerived, TError> result)
            where TDerived : class, T
        {
            return result.state switch
            {
                ResultStatus.IsOk => new(result.resultValue),
                ResultStatus.IsError => new(result.errorValue),
                _ => default,
            };
        }

        /// <summary>Creates a result based on the specified intance, allowing covariant static cast.</summary>
        /// <param name="result">The result to cast error.</param>
        /// <typeparam name="TDerivedError">The type of derived error to cast.</typeparam>
        /// <returns>
        /// A new instance of the <see cref="Result{T,TError}"/> struct based on the contents of the specified instance.
        /// </returns>
        public static Result<T, TError> CastUpError<TDerivedError>(Result<T, TDerivedError> result)
            where TDerivedError : class, TError
        {
            return result.state switch
            {
                ResultStatus.IsOk => new(result.resultValue),
                ResultStatus.IsError => new(result.errorValue),
                _ => default,
            };
        }

        /// <summary>Create a result with the specified value.</summary>
        public static Result<T, TError> Ok(T resultValue)
        {
            return new Result<T, TError>(resultValue);
        }

        /// <summary>Create a result with the specified error.</summary>
        public static Result<T, TError> Error(TError errorValue)
        {
            return new Result<T, TError>(errorValue);
        }

        /// <summary>
        /// Creates a result by casting current value to <typeparamref name="TOther"/>
        /// or error to <typeparamref name="TOtherError"/>.
        /// </summary>
        /// <typeparam name="TOther">The type to cast value to.</typeparam>
        /// <typeparam name="TOtherError">The type to cast error to.</typeparam>
        /// <returns>A new instance of the <see cref="Result{T,TError}"/> struct.</returns>
        /// <exception cref="InvalidCastException">The result value or error cannot be cast to the specified type.</exception>
        public Result<TOther, TOtherError> Cast<TOther, TOtherError>()
            where TOther : class
            where TOtherError : class
        {
            return state switch
            {
                ResultStatus.IsOk => new((TOther)(object)resultValue!),
                ResultStatus.IsError => new((TOtherError)(object)errorValue!),
                _ => default,
            };
        }

        /// <summary>
        /// Creates a result by casting current value to <typeparamref name="TOther"/>.
        /// </summary>
        /// <typeparam name="TOther">The type to cast value to.</typeparam>
        /// <returns>A new instance of the <see cref="Result{T,TError}"/> struct.</returns>
        /// <exception cref="InvalidCastException">The result value cannot be cast to the specified type.</exception>
        public Result<TOther, TError> CastValue<TOther>()
            where TOther : class
        {
            return state switch
            {
                ResultStatus.IsOk => new((TOther)(object)resultValue!),
                ResultStatus.IsError => new(errorValue),
                _ => default,
            };
        }

        /// <summary>
        /// Creates a result by casting current error to <typeparamref name="TOtherError"/>.
        /// </summary>
        /// <typeparam name="TOtherError">The type to cast error to.</typeparam>
        /// <returns>A new instance of the <see cref="Result{T,TError}"/> struct.</returns>
        /// <exception cref="InvalidCastException">The result error cannot be cast to the specified type.</exception>
        public Result<T, TOtherError> CastError<TOtherError>()
            where TOtherError : class
        {
            return state switch
            {
                ResultStatus.IsOk => new(resultValue),
                ResultStatus.IsError => new((TOtherError)(object)errorValue!),
                _ => default,
            };
        }

        public Result<TOther, TError> Bind<TOther>(Func<T, Result<TOther, TError>> bind)
        {
            return state switch
            {
                ResultStatus.IsOk => bind(resultValue),
                ResultStatus.IsError => Result<TOther, TError>.Error(errorValue),
                _ => Result<TOther, TError>.Empty,
            };
        }

        public Result<TOther, TError> BiBind<TOther>(
            Func<T, Result<TOther, TError>> ok,
            Func<TError, Result<TOther, TError>> error)
        {
            return state switch
            {
                ResultStatus.IsOk => ok(resultValue),
                ResultStatus.IsError => error(errorValue),
                _ => Result<TOther, TError>.Empty,
            };
        }

        public override bool Equals(object? obj)
        {
            return obj is Result<T, TError> other && Equals(other);
        }

        public bool Equals(Result<T, TError> other)
        {
            if (state != other.state)
                return false;

            return state switch
            {
                ResultStatus.IsOk => EqualityComparer<T>.Default.Equals(resultValue, other.resultValue),
                ResultStatus.IsError => EqualityComparer<TError>.Default.Equals(errorValue, other.errorValue),
                ResultStatus.Empty => true,
                _ => false,
            };
        }

        /// <summary>Gets the returned error if any; otherwise, throws.</summary>
        /// <exception cref="InvalidOperationException">The result returned a value or is empty.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TError GetError()
        {
            return errorValue;
        }

        /// <summary>Gets the returned error if any; otherwise, throws.</summary>
        /// <param name="errorWhenEmpty">The error to retrieve when the result is empty.</param>
        /// <exception cref="InvalidOperationException">The result returned a value.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TError GetError(TError errorWhenEmpty)
        {
            return state switch
            {
                ResultStatus.IsError => errorValue,
                ResultStatus.Empty => errorWhenEmpty,
                _ => throw new Exception(),
            };
        }

        /// <summary>Gets the returned error if any; otherwise, throws.</summary>
        /// <param name="whenEmpty">The function to return an error to retrieve when the result is empty.</param>
        /// <exception cref="InvalidOperationException">The result returned a value.</exception>
        public TError GetError(Func<TError> whenEmpty)
        {
            return state switch
            {
                ResultStatus.IsError => errorValue,
                ResultStatus.Empty => whenEmpty(),
                _ => throw new Exception(),
            };
        }

        /// <summary>Gets the returned value if any; otherwise, throws.</summary>
        /// <exception cref="InvalidOperationException">The result returned an error or is empty.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue()
        {
            return resultValue;
        }

        /// <summary>Returns the specified value if result is an error; otherwise the current returned value.</summary>
        /// <exception cref="InvalidOperationException">The result is empty.</exception>
        public T IfError(T valueWhenError)
        {
            return state switch
            {
                ResultStatus.IsOk => resultValue,
                ResultStatus.IsError => valueWhenError,
                _ => throw new Exception(),
            };
        }

        /// <summary>Returns the specified error if result has a value; otherwise the current error.</summary>
        /// <exception cref="InvalidOperationException">The result is empty.</exception>
        public TError IfOk(TError valueWhenOk)
        {
            return state switch
            {
                ResultStatus.IsOk => valueWhenOk,
                ResultStatus.IsError => errorValue,
                _ => throw new Exception(),
            };
        }

        public override int GetHashCode()
        {
            return state switch
            {
                ResultStatus.IsOk => HashCode.Combine(state, resultValue),
                ResultStatus.IsError => HashCode.Combine(state, errorValue),
                _ => 0,
            };
        }

        /// <summary>Matches the states of the result.</summary>
        public void Match(Action<T> ok, Action<TError> error, Action? empty = null)
        {
            switch (state)
            {
                case ResultStatus.IsOk:
                    ok(resultValue);
                    break;
                case ResultStatus.IsError:
                    error(errorValue);
                    break;
                case var _ when empty is not null:
                    empty();
                    break;
                default:
                    throw new Exception();
                    break;
            }
        }

        /// <summary>Matches the states of the result and return a value.</summary>
        /// <typeparam name="TResult">The return type.</typeparam>
        public TResult Match<TResult>(Func<T, TResult> ok, Func<TError, TResult> error, Func<TResult>? empty = null)
        {
            return state switch
            {
                ResultStatus.IsOk => ok(resultValue),
                ResultStatus.IsError => error(errorValue),
                _ when empty is not null => empty(),
                _ => throw new Exception(),
            };
        }

        /// <summary>Matches the states of the result and return a value.</summary>
        /// <typeparam name="TResult">The return type.</typeparam>
        public TResult Match<TResult>(Func<T, TResult> ok, Func<TError, TResult> error, TResult empty)
        {
            return state switch
            {
                ResultStatus.IsOk => ok(resultValue),
                ResultStatus.IsError => error(errorValue),
                _ => empty,
            };
        }

        public Result<TOther, TError> Select<TOther>(Func<T, TOther> project)
        {
            return state switch
            {
                ResultStatus.IsOk => project(resultValue),
                ResultStatus.IsError => errorValue,
                _ => Result<TOther, TError>.Empty,
            };
        }

        public override string ToString()
        {
            return state switch
            {
                ResultStatus.IsOk => $"Ok({resultValue?.ToString() ?? "null"})",
                ResultStatus.IsError => $"Error({errorValue?.ToString() ?? "null"})",
                _ => "Empty",
            };
        }

        /// <summary>Attempts to retrieve the returned value of this result.</summary>
        /// <param name="value">When this method returns, contains the returned value if any.</param>
        /// <param name="error">When this method returns, contains the error if any.</param>
        /// <returns>True whether this result returned a value; otherwise, false.</returns>
        /// <exception cref="InvalidOperationException">The result is empty.</exception>
        public bool TryGetValue([MaybeNullWhen(false)] out T value, [MaybeNullWhen(true)] out TError error)
        {
            switch (state)
            {
                case ResultStatus.IsOk:
                    value = resultValue;
                    error = default;
                    return true;
                case ResultStatus.IsError:
                    value = default;
                    error = errorValue;
                    return false;
                default:
                    value = default;
                    error = default;
                    return false;
            }
        }
    }
}
