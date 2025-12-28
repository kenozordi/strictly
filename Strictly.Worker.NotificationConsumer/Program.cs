using StackExchange.Redis;
using Strictly.Infrastructure.Configuration;
using Strictly.Worker.NotificationConsumer.Configuration.AppSettings;
using Strictly.Worker.NotificationProducer.Workers;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<ReminderConsumer>();

var connectionStringPlaceholder = new ConnectionStrings();
builder.Services.AddSingleton<IConnectionMultiplexer>(x =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString(nameof(connectionStringPlaceholder.Redis))
));
builder.AddStrictlyToWorker();

var host = builder.Build();
host.Run();
