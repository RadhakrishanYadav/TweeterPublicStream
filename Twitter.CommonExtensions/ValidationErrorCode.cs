using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twitter.CommonExtensions
{
    public enum ValidationErrorCode
    {
        // client errors
        InvalidStateError = 100000,
        ArgumentNullError = 100001,
        BadRequestError = 100002,
        TweetNotFound = 100003,
    }

    public static class ValidationErrorCodesMap
    {
        private static Dictionary<ValidationErrorCode, string> errorCodesMap = new Dictionary<ValidationErrorCode, string>();

        static ValidationErrorCodesMap()
        {
            errorCodesMap.Add(ValidationErrorCode.InvalidStateError, "body is required");
            errorCodesMap.Add(ValidationErrorCode.ArgumentNullError, "missing required argument");
            errorCodesMap.Add(ValidationErrorCode.BadRequestError, "Bad request");
            errorCodesMap.Add(ValidationErrorCode.TweetNotFound, "Tweet not found");
        }

        public static string ToErrorMessage(this ValidationErrorCode code)
        {
            return errorCodesMap[code];
        }

        public static string ToErrorCode(this ValidationErrorCode code)
        {
            return Constants.ServiceIdentifier + (int)code;
        }
    }
}
