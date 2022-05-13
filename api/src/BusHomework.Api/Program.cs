using BusHomework.Api.Endpoints;
using BusHomework.Api.Infra;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
IocSetup.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();
app.MapGet("/stop/{stopId}", Stop.StopWithStopId);
app.MapGet("/stop/{stopId}/time/{timestamp}", Stop.StopWithStopIdAndCallTime);

app.Run();
