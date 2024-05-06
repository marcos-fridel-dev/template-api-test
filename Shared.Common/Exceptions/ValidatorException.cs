using Shared.Common.Enums.Responses;
using Shared.Common.Models.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Common.Exceptions
{
    public class ValidatorException : Exception
    {
        public ValidatorException(string message, List<ErrorValidator> validations, ResultSeverities severity = ResultSeverities.Normal) :
            base(message)
        {
            Severity = severity;
            Validations = validations;
            Type = Validations.Max(x => x.Type);
        }

        public ValidatorException(string message, ResultSeverities severity = ResultSeverities.Normal) : base(message)
        {
            Type = ResultTypes.Error;
            Severity = severity;
        }

        public ResultSeverities Severity { get; set; }
        public ResultTypes Type { get; set; }
        public List<ErrorValidator> Validations { get; set; } = new();
    }
}