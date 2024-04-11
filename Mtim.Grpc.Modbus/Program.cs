using Mtim.Grpc.Modbus.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

//custom
builder.Services.AddKeyedSingleton<IModbusService, ModbusTcpService>("tcp");
builder.Services.AddKeyedSingleton<IModbusService, ModbusUdpService>("udp");
builder.Services.AddSingleton<IModbusServiceFactory, ModbusServiceFactory>();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<ModbusGrpcService>();

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. " +
        "To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();