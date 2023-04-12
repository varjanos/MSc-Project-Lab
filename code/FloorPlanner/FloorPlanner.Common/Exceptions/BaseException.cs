﻿namespace FloorPlanner.Common.Exceptions;

public abstract class BaseException : Exception
{
    public BaseException(string message)
        : base(message)
    {
    }

    public BaseException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public abstract string TranslationKey { get; }
}