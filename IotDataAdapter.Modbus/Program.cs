using IotDataAdapter.Modbus.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
//
// //custom register
// builder.Services.AddControllers();
//
// builder.Services.AddTransient<IModbusService, ModbusService>();

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.MapControllers();

// Configure the HTTP request pipeline.
app.MapGrpcService<ModbusGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. " +
                      "To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");


app.Run();