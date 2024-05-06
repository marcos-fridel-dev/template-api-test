using Shared.Common.Enums.Responses;
using Shared.Common.Models.Validators;
using System.Collections.Generic;

namespace Shared.Common.Models.Responses
{
    public class ResultResponse : ResultResponse<object>
    {
    }

    public class ResultResponse<T>
    {
        public ResultTypes Type { get; set; } = ResultTypes.Success;
        public string TypeLabel => Type.ToString().ToLower();
        public ResultSeverities Severity { get; set; } = ResultSeverities.Normal;
        public string SeverityLabel => Severity.ToString().ToLower();
        public string Message { get; set; } = string.Empty;
        public IEnumerable<ErrorValidator>? Validations { get; set; }
        public T? Data { get; set; }
    }
}