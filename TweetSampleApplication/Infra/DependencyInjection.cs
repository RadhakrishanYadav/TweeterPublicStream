using Autofac;
using MediatR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TweetSample.Api;
using TweetSample.Api.Abstraction;
using TwitterCoreApp;
using TwitterCoreApp.Abstraction;

namespace TweetSampleApplication.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<ITwitterAuthClient, TwitterAuthClient>();
            services.AddSingleton<TweetSampleStreamService>();
            services.AddMediatR(typeof(TweetStreamSampleCommandHandler).GetTypeInfo().Assembly);
            services.AddScoped<ITweetsSampleProcessor, TweetsSampleProcessor>();
            return services;
        }
    }
}
