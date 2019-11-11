using Blacksmith.Validations;
using Blacksmith.Validations.Exceptions;
using System;

namespace Blacksmith.Automap.Tests.Models.Domain
{
    public abstract class AbstractAppDomain : AbstractDomain
    {
        protected static DateTime minimalDate = new DateTime(1990, 1, 1);

        protected void dateIsValid(DateTime date)
        {
            isTrue<DomainException>(minimalDate <= date);
        }


    }
}