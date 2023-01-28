using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net;
using Code = twitter.CommonExtensions.ValidationErrorCode;


namespace TweetSample.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        [NonAction]
        public ActionResult ErrorResponse(string msg, Code code)
        {
            var httpStatus = this.DetermineHttpStatusCode(code);
            MessageStatusModel messageStatus = this.GetMessageStatusModel(msg, $"{(int)httpStatus}");
            return this.StatusCode((int)httpStatus, messageStatus);
        }

        [NonAction]
        public ActionResult NotFoundResponse(string msg, Code code)
        {
            MessageStatusModel messageStatus = this.GetMessageStatusModel(msg, $"{(int)HttpStatusCode.NotFound}");
            return this.NotFound(messageStatus);
        }

        private HttpStatusCode DetermineHttpStatusCode(Code code)
        {
            return code switch
            {
                var c when
                    c == Code.ArgumentNullError ||
                    c == Code.BadRequestError ||
                    c == Code.InvalidStateError => HttpStatusCode.BadRequest,

                var c when
                   c == Code.TweetNotFound
                    => HttpStatusCode.NotFound,

            };
        }
        private MessageStatusModel GetMessageStatusModel(string msg, string responseCode)
        {
            return new MessageStatusModel
            {
                ResponseCode = responseCode,
                Description = msg,
            };
        }
    }
}
