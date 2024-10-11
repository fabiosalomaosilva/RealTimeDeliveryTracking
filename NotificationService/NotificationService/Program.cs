using Confluent.Kafka;
using NotificationService.Workers;
using SendGrid;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConsumer<Null, string>>(sp =>
{
    var config = new ConsumerConfig
    {
        GroupId = "notification-service",
        BootstrapServers = "localhost:9092",
        AutoOffsetReset = AutoOffsetReset.Earliest
    };
    return new ConsumerBuilder<Null, string>(config).Build();
});

builder.Services.AddSingleton<ISendGridClient>(_ =>
    new SendGridClient(builder.Configuration["SendGridApiKey"])
);

//builder.Services.AddSingleton<FirebaseMessaging>(_ =>
//{
//    FirebaseApp.Create(new AppOptions()
//    {
//        Credential = GoogleCredential.FromFile("path/to/serviceAccountKey.json")
//    });
//    return FirebaseMessaging.DefaultInstance;
//});

builder.Services.AddHostedService<NotificationConsumerService>();

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
