using Confluent.Kafka;

namespace OrderService.Test;
public class KafkaTest
{
    public static void Execute()
    {
        var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            producer.Produce("test-topic", new Message<Null, string> { Value = "Hello Kafka!" });
            producer.Flush(TimeSpan.FromSeconds(10));
        }

        // Exemplo de consumo de mensagem do Kafka
        var consumerConfig = new ConsumerConfig
        {
            GroupId = "test-consumer-group",
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using (var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build())
        {
            consumer.Subscribe("test-topic");

            var consumeResult = consumer.Consume();
            Console.WriteLine($"Mensagem recebida: {consumeResult.Message.Value}");
        }
    }
}

