using Worker;
using Worker.Handlers;
using Worker.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IJobHandler, CheckImageExists>();
builder.Services.AddSingleton<JobExecutor>();

builder.Services.AddHostedService<Worker.Worker>();

var host = builder.Build();

host.Run();