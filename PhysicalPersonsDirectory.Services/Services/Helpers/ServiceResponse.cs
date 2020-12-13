using PhysicalPersonsDirectory.Services.Models.Base;
using System.Net;

namespace PhysicalPersonsDirectory.Services.Services.Helpers
{
    public static class ServiceResponse
    {
        public static T Success<T>(T model) where T : ResponseBaseModel
        {
            model.StatusCode = (int)HttpStatusCode.OK;
            model.Success = true;
            model.RawResponse = Common.Resources.RsStrings.OperationCompleted;
            model.UserMessage = Common.Resources.RsStrings.OperationCompleted;

            return model;
        }

        public static T Success<T>(T model, string message) where T : ResponseBaseModel
        {
            model.StatusCode = (int)HttpStatusCode.OK;
            model.Success = true;
            model.RawResponse = $"{message}";
            model.UserMessage = $"{message}";

            return model;
        }

        public static T Fail<T>(T model, string message) where T : ResponseBaseModel
        {
            model.StatusCode = (int)HttpStatusCode.OK;
            model.RawResponse = $"{message}";
            model.UserMessage = $"{message}";

            return model;
        }

        public static T NotFound<T>(T model) where T : ResponseBaseModel
        {
            model.StatusCode = (int)HttpStatusCode.NotFound;
            model.RawResponse = $"{nameof(model)} with such ID not found";
            model.UserMessage = $"{nameof(model)} not found.";

            return model;
        }

        public static T NotFound<T>(T model, string message) where T : ResponseBaseModel
        {
            model.StatusCode = (int)HttpStatusCode.NotFound;
            model.RawResponse = $"{message}";
            model.UserMessage = $"{message}";

            return model;
        }

        public static T Error<T>(T model, string message) where T : ResponseBaseModel
        {
            model.StatusCode = (int)HttpStatusCode.InternalServerError;
            model.RawResponse = $"{message}";
            model.UserMessage = $"{message}";

            return model;
        }
    }
}
