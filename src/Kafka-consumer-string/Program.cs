using Confluent.Kafka;

const string KAFKA_TOPIC = "MYSTRINGMESSAGE-KAFKA-TOPIC";

Console.WriteLine("Hello, Consumer!");

var consumerConfig = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "log-events",
    AutoOffsetReset = AutoOffsetReset.Latest,
};

using var consumer = new ConsumerBuilder<string, string>(consumerConfig)
    .Build();
consumer.Subscribe(KAFKA_TOPIC);

while (true)
{
    try
    {
        var result = consumer.Consume(100);
        if (result != null)
        {
            Console.WriteLine($"Received message: {result.Message.Value}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Something went wrong while trying to consume: {ex.Message}");
    }

}