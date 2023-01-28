# TweeterPublicStream
TweeterPublicStream is built in Dot Net 6.0 and C#. Tweetinvi library is used to get a random sample of approximately 1% of the full tweet stream.
Tweetinvi is an intuitive .NET C# library to access the Twitter REST API.
There is a background service to consume the incoming tweets where authentication is done by using the consumer credentials, stored in appconfig file.
Decoupling is achieved by using Mediator design pattern while consuming and processing the tweets.
There is a get endpoing exposed to keep track of the following: 
•	Total number of tweets received  
•	Average tweets per minute 

This solution contains 6 projects:

1. TweetSampleApplication : This project contains below main files :
appSettings - to hold the application configuration like credentials /token
TweetSampleBackgroundService- fetch the tweets continuously from the sample stream tweeter api
TweetsSampleController- exposing a get endpoint to display the tweets statistics


2. TwitterCoreApp : Contains 4 class file:
TweetStreamSampleCommandHandler: MediatR command handler to consume the tweets received.
TweetsSampleProcessor : To process the received tweets and generate response.
TweetSampleStreamService : Used to add and get the tweets from the concurrent dictionary
TweetSampleException : To handle the exceptions

3. Models : This contains the models used throughout the application like requestCommand, get api response, credentials and exceptions

4. TwitterCommonExtensions : This project contains common functionality like static class, constants and error codes.

These are 2 Unit test projects:
5. ApiTest : Code cover for background service and controller class 
6. CoreAppTest : Mainly cover the tweet processing logic.

