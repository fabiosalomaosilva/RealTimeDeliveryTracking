using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Postgres;
using Postgres.Repositories;
using TrackingService.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DBContext
builder.Services.AddDbContext<OrderContext>(opt =>
        opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")),
    ServiceLifetime.Singleton
);

builder.Services.AddSingleton<IConsumer<Null, string>>(sp =>
{
    var config = new ConsumerConfig
    {
        GroupId = "tracking-service",
        BootstrapServers = "localhost:9092",
        AutoOffsetReset = AutoOffsetReset.Earliest
    };
    return new ConsumerBuilder<Null, string>(config).Build();
});

builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
builder.Services.AddSingleton<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddHostedService<OrderConsumerService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();