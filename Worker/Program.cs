using Worker;
using Worker.Handlers;
using Worker.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IJobHandler, CheckImageExists>();
builder.Services.AddSingleton<JobExecutor>();

builder.Services.AddHostedService<Worker.Worker>();

builder.Services.AddHttpClient<JobApiClient>(client =>
{
    client.BaseAddress = new Uri("http://127.0.0.1:5213/");
});

var host = builder.Build();

host.Run();