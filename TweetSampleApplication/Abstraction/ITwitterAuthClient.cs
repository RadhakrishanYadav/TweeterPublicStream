using Tweetinvi;

namespace TweetSample.Api.Abstraction
{
    public interface ITwitterAuthClient
    {
        Task<TwitterClient> GetAuthTwitterClientAsync();
    }
}
