using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Concurrent;
using TwitterCoreApp;

namespace CoreAppTest
{
    public class TweetsSampleProcessorTest
    {
        private readonly Mock<ILogger<TweetsSampleProcessor>> mockLogger;
        private readonly Mock<TweetSampleStreamService> mockStreamResponse;
        private readonly TweetsSampleProcessor tweetsSampleProcessor;
        private readonly TweetSampleStreamService tweetSampleStreamResponse = new TweetSampleStreamService();
        public TweetsSampleProcessorTest()
        {
            this.mockLogger = new Mock<ILogger<TweetsSampleProcessor>>();
            this.mockStreamResponse = new Mock<TweetSampleStreamService>();
            this.tweetsSampleProcessor = new TweetsSampleProcessor(this.mockLogger.Object, tweetSampleStreamResponse);
        }

        [Fact]
        public void GetTweeterSampleStreamCount_NoTweet()
        {
            // Arrange
            var mockResponse = new ConcurrentDictionary<DateTime, string>();
            mockResponse.TryAdd(new DateTime() , "streamId");

            // Act
            var result = tweetsSampleProcessor.GetSampleTweetsCount();
            Assert.Equal(0,result.AverageTweetsPerMinute);
            Assert.Equal(0, result.TotalTweetsCount);
        }

        [Fact]
        public void GetTweeterSampleStreamCount_Success()
        {
            // Arrange
            var mockResponse = new ConcurrentDictionary<DateTime, string>();
            mockResponse.TryAdd(DateTime.Now, "streamId");
            mockResponse.TryAdd(DateTime.Now, "streamId1");

            tweetSampleStreamResponse.AddStreamToList(DateTime.Now,"TweetId1");
            tweetSampleStreamResponse.AddStreamToList(DateTime.Now, "TweetId2");

            // Act
            var result = tweetsSampleProcessor.GetSampleTweetsCount();
           
            Assert.Equal(2, result.TotalTweetsCount);
        }
    }
}