using Shared.Common.Enums.Responses;
using System.Net;

namespace Shared.Common.Models.Validators
{
    public class ErrorValidator
    {
        public ResultTypes Type { get; set; }
        public string PropertyName { get; set; }
        public string Message { get; set; }
        public ResultSeverities Severity { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}