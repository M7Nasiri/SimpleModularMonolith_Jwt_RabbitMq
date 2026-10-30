using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain.Exceptions;

public class InvalidDomainDataException : BaseDomainException
{
    public InvalidDomainDataException()
    {

    }
    public InvalidDomainDataException(string message) : base(message)
    {

    }
}