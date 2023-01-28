using System.Net.Http;
using System.Net.Security;
using Tweetinvi;
using Tweetinvi.Client;
using Tweetinvi.Models;
using MediatR;
using Models;
using System.Threading;
using Microsoft.Extensions.Options;
using TweetSample.Api.Abstraction;

namespace TweetSampleApplication
{
    public class TweetSampleBackgroundService : BackgroundService
    {
        private readonly ILogger<TweetSampleBackgroundService> _logger;
        private readonly IMediator _mediator;
        private readonly ITwitterAuthClient _twitterAuthClient;

        public TweetSampleBackgroundService(ILogger<TweetSampleBackgroundService> logger, IMediator mediator, ITwitterAuthClient twitterAuthClient)
        {
            _logger = logger;
            _mediator = mediator;
            _twitterAuthClient = twitterAuthClient;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var appClient = await _twitterAuthClient.GetAuthTwitterClientAsync();
            var sampleStreamV2 = appClient.StreamsV2.CreateSampleStream();
            sampleStreamV2.TweetReceived += async (sender, args)  =>
            {
                var command = new TweetStreamSampleCommand
                {
                    Text = args.Tweet.Text,
                    Id = args.Tweet.Id
                };
               await _mediator.Send(command);
                System.Console.WriteLine(args.Tweet.Id);
            };

            _ = Task.Run(() => sampleStreamV2.StartAsync());
        }
    }
}