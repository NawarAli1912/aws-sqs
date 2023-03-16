using Amazon.SQS;
using Customers.QueueConsumer;
using Customers.QueueConsumer.Messages;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.Configure<QueueSettings>(builder.Configuration.GetSection("QueueSettings"));
builder.Services.AddSingleton<IAmazonSQS, AmazonSQSClient>();
builder.Services.AddHostedService<SQSQueueConsumerService>();


var app = builder.Build();

app.Run();
