using Confluent.Kafka;

const string KAFKA_TOPIC = "MYSTRINGMESSAGE-KAFKA-TOPIC";

Console.WriteLine("Hello, kafka-producer!");

var producerConfig = new ProducerConfig
{
    BootstrapServers = "localhost:9092"
};

var producer = new ProducerBuilder<string, string>(producerConfig)
    .Build();

while (true)
{
    try
    {
        var msg = Faker.Lorem.Sentence();
        await producer.ProduceAsync(KAFKA_TOPIC, new Message<string, string>() { Key = "k1", Value = msg });
        Console.WriteLine($"Sent message: {msg}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Something went wrong: {ex.Message}");
    }
    Task.Delay(3000).Wait();
}

