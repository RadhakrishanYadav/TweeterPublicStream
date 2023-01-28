using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Code = twitter.CommonExtensions.ValidationErrorCode;

namespace TwitterCoreApp
{
    public class TweetSampleException : Exception
    {
        public TweetSampleException(string message, Code code, HttpStatusCode httpStatusCode)
            : base(message)
        {
            this.Code = code;
            this.HttpStatusCode = httpStatusCode;
        }

        public TweetSampleException(string message, Code code)
            : base(message)
        {
            this.Code = code;
        }

        public Code Code { get; set; }

        public HttpStatusCode HttpStatusCode { get; private set; }

        public MessageStatusModel MessageStatusModel { get; set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Code), this.Code);

            base.GetObjectData(info, context);
        }
    }
}
