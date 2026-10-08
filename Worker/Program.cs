using Worker;
using Worker.Handlers;
using Worker.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IJobHandler, CheckImageExists>();
builder.Services.AddSingleton<JobExecutor>();

builder.Services.AddHostedService<Worker.Worker>();

builder.Services.AddHttpClient<JobApiClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7131/");
});

var host = builder.Build();

host.Run();