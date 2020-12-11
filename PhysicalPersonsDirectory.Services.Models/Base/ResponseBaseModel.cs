using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace PhysicalPersonsDirectory.Services.Models.Base
{
    public class ResponseBaseModel
    {
        public int StatusCode { get; set; } = StatusCodes.Status400BadRequest;
        public bool Success { get; set; } = false;
        public string RawResponse { get; set; }
        public string UserMessage { get; set; }
        public List<ErrorModel> ErrorMessage { get; set; }
    }
}
