using Strictly.Infrastructure.Configuration;
using Strictly.Worker.NotificationProducer.Workers;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<ReminderProducer>();

builder.AddStrictlyToWorker();

var host = builder.Build();
host.Run();
