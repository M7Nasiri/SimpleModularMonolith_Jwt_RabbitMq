using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain.Exceptions;

public class BaseDomainException : Exception
{
    public BaseDomainException()
    {
    }

    public BaseDomainException(string message) : base(message)
    {
    }
}