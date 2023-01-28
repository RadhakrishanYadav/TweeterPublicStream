using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net;
using TwitterCoreApp;
using TwitterCoreApp.Abstraction;
using Code = twitter.CommonExtensions.ValidationErrorCode;

namespace TweetSample.Api.Controllers.V2
{
    [Route("api/v1/twitter/sample/stream")]
    public class TweetsSampleController : BaseController
    {
        private readonly ILogger<TweetsSampleController> logger;
        private readonly ITweetsSampleProcessor tweetsSampleProcessor;

        public TweetsSampleController(
            ILogger<TweetsSampleController> logger,
            ITweetsSampleProcessor tweetsSampleProcessor)
        {
            this.logger = logger;
            this.tweetsSampleProcessor = tweetsSampleProcessor;
        }

        [HttpGet("count")]
        [ProducesResponseType(typeof(TweetResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(MessageStatusModel), (int)HttpStatusCode.NotFound)]
        public IActionResult GetTweetSampleCount()
        {
            try
            {
                this.logger.LogDebug($"Begin: GetTweetSampleCount");
                var response = this.tweetsSampleProcessor.GetSampleTweetsCount();
                this.logger.LogDebug($"End: GetTweetSampleCount");
                if (response.TotalTweetsCount < 1)
                {
                    return this.NotFoundResponse("No tweet found", Code.TweetNotFound);

                }
                return this.Ok(response);
            }
            catch (TweetSampleException ex)
            {
                this.logger.LogError(ex, $"Error: GetTweetSampleCount");
                return this.ErrorResponse(ex.Message, ex.Code);
            }
        }

    }
}
