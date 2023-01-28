using MediatR;
using Microsoft.Extensions.Options;
using Models;
using Tweetinvi.Models;
using Tweetinvi;
using TweetSample.Api.Abstraction;

namespace TweetSample.Api

{
    public class TwitterAuthClient : ITwitterAuthClient
    {
        private readonly ILogger<TwitterAuthClient> _logger;
        private readonly TwitterApiCredential _twitterCredentials;

        public TwitterAuthClient(ILogger<TwitterAuthClient> logger, IMediator mediator, IOptions<TwitterApiCredential> credentialOptions)
        {
            _logger = logger;
            _twitterCredentials = credentialOptions.Value;
        }

        public async Task<TwitterClient> GetAuthTwitterClientAsync()
        {
            var consumerOnlyCredentials = new ConsumerOnlyCredentials(_twitterCredentials.ConsumerKey, _twitterCredentials.ConsumerSecret);
            var appClientWithoutBearer = new TwitterClient(consumerOnlyCredentials);

            var bearerToken = await appClientWithoutBearer.Auth.CreateBearerTokenAsync();

            var appCredentials = new ConsumerOnlyCredentials(_twitterCredentials.ConsumerKey, _twitterCredentials.ConsumerSecret)
            {
                BearerToken = bearerToken 
            };
           return new TwitterClient(appCredentials);
        }
    }
}
