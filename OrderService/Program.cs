using Confluent.Kafka;
using Kafka.Test;
using Microsoft.EntityFrameworkCore;
using OrderService.Repositories;
using OrderService.Services;
using Postgres;

var builder = WebApplication.CreateBuilder(args);

//Início dos registros de Injeção de dependência

//DBContext
builder.Services.AddDbContext<OrderContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Default"))
);

//Kafka Producer
builder.Services.AddSingleton<IProducer<Null, string>>(sp =>
{
    var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
    return new ProducerBuilder<Null, string>(config).Build();
});

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrdersService, OrdersService>();

//Fim dos registros de Injeção de dependência

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Teste do Kafka
KafkaTest.Execute();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();