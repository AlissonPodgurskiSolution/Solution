﻿using Core.Messages.Integration;
using EasyNetQ;
using Polly;
using RabbitMQ.Client.Exceptions;

namespace MessageBus;

public class MessageBus : IMessageBus
{
    private readonly string _connectionString;
    private IAdvancedBus _advancedBus;
    private IBus _bus;

    public MessageBus(string connectionString)
    {
        _connectionString = connectionString;
        TryConnect();
    }

    public bool IsConnected => _bus?.IsConnected ?? false;
    public IAdvancedBus AdvancedBus => _bus?.Advanced;

    public void Publish<T>(T message) where T : IntegrationEvent
    {
        TryConnect();
        _bus.Publish(message);
    }

    public async Task PublishAsync<T>(T message) where T : IntegrationEvent
    {
        TryConnect();
        await _bus.PublishAsync(message);
    }

    public void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class
    {
        TryConnect();
        _bus.Subscribe(subscriptionId, onMessage);
    }

    public void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage, bool autoAck) where T : class
    {
        TryConnect();
        _bus.SubscribeAsync(subscriptionId, onMessage);
    }

    public TResponse Request<TRequest, TResponse>(TRequest request) where TRequest : IntegrationEvent
        where TResponse : ResponseMessage
    {
        TryConnect();
        return _bus.Request<TRequest, TResponse>(request);
    }

    public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
        where TRequest : IntegrationEvent where TResponse : ResponseMessage
    {
        TryConnect();
        return await _bus.RequestAsync<TRequest, TResponse>(request);
    }

    public IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder)
        where TRequest : IntegrationEvent where TResponse : ResponseMessage
    {
        TryConnect();
        return _bus.Respond(responder);
    }

    public IDisposable RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder)
        where TRequest : IntegrationEvent where TResponse : ResponseMessage
    {
        TryConnect();
        return _bus.RespondAsync(responder);
    }

    public void Dispose()
    {
        _bus.Dispose();
    }

    private void TryConnect()
    {
        if (IsConnected) return;

        var policy = Policy.Handle<EasyNetQException>()
            .Or<BrokerUnreachableException>()
            .WaitAndRetry(3, retryAttempt =>
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        policy.Execute(() =>
        {
            _bus = RabbitHutch.CreateBus(_connectionString);
            _advancedBus = _bus.Advanced;
            _advancedBus.Disconnected += OnDisconnect;
        });
    }

    private void OnDisconnect(object s, EventArgs e)
    {
        var policy = Policy.Handle<EasyNetQException>()
            .Or<BrokerUnreachableException>()
            .RetryForever();

        policy.Execute(TryConnect);
    }
}