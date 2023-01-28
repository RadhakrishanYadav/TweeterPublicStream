using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Models;
using Moq;
using System.Net;
using System.Reflection;
using TweetSample.Api.Controllers.V2;
using twitter.CommonExtensions;
using TwitterCoreApp;
using TwitterCoreApp.Abstraction;
using Xunit;

namespace TwitterApiTest
{
    public class TweetsSampleControllerTest
    {
        private readonly Mock<ITweetsSampleProcessor> mockTweetSampleProcessor;
        private readonly Mock<ILogger<TweetsSampleController>> mockLogger;
        private readonly TweetsSampleController tweetsSampleController;

        public TweetsSampleControllerTest()
        {
            this.mockLogger = new Mock<ILogger<TweetsSampleController>>();
            this.mockTweetSampleProcessor = new Mock<ITweetsSampleProcessor>();
            this.tweetsSampleController = new TweetsSampleController(this.mockLogger.Object, this.mockTweetSampleProcessor.Object);
        }

        [Fact]
        public void GetTweeterSampleStream_Success()
        {
            // Arrange
            var mockResponse = new Models.TweetResponse()
            {
                TotalTweetsCount = 200,
                AverageTweetsPerMinute = 2500
            };
            this.mockTweetSampleProcessor
                .Setup(_ => _.GetSampleTweetsCount())
                .Returns(mockResponse);

            // Act
            var actualResponse = this.tweetsSampleController.GetTweetSampleCount();
            var actionResult = Assert.IsType<OkObjectResult>(actualResponse);
            Assert.Equal((int)HttpStatusCode.OK, actionResult.StatusCode);
        }

        [Fact]
        public void GetTweeterSampleStream_NoTweetFound()
        {
            //Arrange
            var mockResponse = new Models.TweetResponse()
            {
                TotalTweetsCount = 0,
                AverageTweetsPerMinute = 0
            };
            this.mockTweetSampleProcessor
                .Setup(_ => _.GetSampleTweetsCount())
                .Returns(mockResponse);

            // Act
            var actualResponse = this.tweetsSampleController.GetTweetSampleCount();

            var actualActionResult = Assert.IsType<NotFoundObjectResult>(actualResponse);
            Assert.Equal((int)HttpStatusCode.NotFound, actualActionResult.StatusCode);
        }


        [Fact]
        public void GetTweeterSampleStream_ThrowException()
        {
            //Arrange
           
            this.mockTweetSampleProcessor
                .Setup(_ => _.GetSampleTweetsCount())
                .Throws(new TweetSampleException(ValidationErrorCode.BadRequestError.ToErrorCode(), ValidationErrorCode.BadRequestError));
           
            // Act
            var actualResponse = this.tweetsSampleController.GetTweetSampleCount();

            var actualActionResult = Assert.IsType<ObjectResult>(actualResponse);
            Assert.Equal((int)HttpStatusCode.BadRequest, actualActionResult.StatusCode);
        }
    }
}
