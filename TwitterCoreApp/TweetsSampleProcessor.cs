using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using twitter.CommonExtensions;
using TwitterCoreApp.Abstraction;

namespace TwitterCoreApp
{
    public class TweetsSampleProcessor : ITweetsSampleProcessor
    {
        private readonly ILogger<TweetsSampleProcessor> logger;
        private readonly TweetSampleStreamService tweetSampleStreamService;
        public TweetsSampleProcessor(ILogger<TweetsSampleProcessor> logger, TweetSampleStreamService tweetSampleStreamService)
        {
            this.logger = logger;
            this.tweetSampleStreamService = tweetSampleStreamService;
        }

        public TweetResponse GetSampleTweetsCount()
        {
            try
            {
                this.logger.LogDebug($"Begin: GetSampleTweetsCount");
                TweetResponse tweetResponse = new TweetResponse();
                var tweets = this.tweetSampleStreamService.GetStreamList();
                if (tweets.Any())
                {
                    var totaltweetCount = tweets.Count();
                    var startDate = tweets.Keys.OrderBy(x => x.Date).ThenBy(x => x.Hour).ThenBy(x => x.Minute).ThenBy(x => x.Second).First();
                    var endDate = tweets.Keys.OrderBy(x => x.Date).ThenBy(x => x.Hour).ThenBy(x => x.Minute).ThenBy(x => x.Second).Last();
                    TimeSpan timeSpan = endDate - startDate;
                    tweetResponse.TotalTweetsCount = totaltweetCount;
                    tweetResponse.AverageTweetsPerMinute = Math.Round((totaltweetCount / (timeSpan.TotalMinutes)), MidpointRounding.AwayFromZero);
                }
                else
                {
                    tweetResponse.ErrorModel = new MessageStatusModel
                    {
                        Description = ValidationErrorCode.TweetNotFound.ToErrorMessage(),
                        ResponseCode = ValidationErrorCode.TweetNotFound.ToErrorCode(),
                    };
                }
                return tweetResponse;
            }
            catch (TweetSampleException ex)
            {
                this.logger.LogError(ex, $"Error: GetSampleTweetsCount: {ex.Message}");
                throw;
            }
        }
    }
}
