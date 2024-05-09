﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace RecruitmentTask.Domain.Abstractions;

public class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public static Result Success() => new Result(true, Error.None);

    public static Result Failure(Error error) => new Result(false, error);

    public static Result<TValue> Success<TValue>(TValue value) => new Result<TValue>(value, true, Error.None);

    public static Result<TValue> Failure<TValue>(Error error) => new Result<TValue>(default, false, error);

    public static Result<TValue> Create<TValue>(TValue value) => value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
}

public class Result<TValue> : Result
{
    private readonly TValue _value;

    protected internal Result(TValue value, bool isSuccess, Error error) 
        : base(isSuccess, error)
    {
        _value = value;
    }

    [NotNull]
    public TValue Value => IsSuccess ? _value : throw new InvalidOperationException("The value of a failure result cannot be accessed.");

    public static implicit operator Result<TValue>(TValue value) => Create(value);
}
