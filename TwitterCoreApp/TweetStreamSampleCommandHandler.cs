using MediatR;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace TwitterCoreApp
{
    public class TweetStreamSampleCommandHandler : IRequestHandler<TweetStreamSampleCommand, bool>
    {
        private readonly ILogger<TweetStreamSampleCommandHandler> logger;
        private readonly TweetSampleStreamService tweetSampleStreamResponse;
        public TweetStreamSampleCommandHandler(ILogger<TweetStreamSampleCommandHandler> logger,
            TweetSampleStreamService tweetSampleStreamResponse)
        {
            this.logger = logger;
            this.tweetSampleStreamResponse = tweetSampleStreamResponse;
        }

        public Task<bool> Handle(TweetStreamSampleCommand request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(request.Id))
            {
                this.tweetSampleStreamResponse.AddStreamToList(DateTime.Now, request.Id);
            }

            return Task.FromResult(true);
        }
    }
}
