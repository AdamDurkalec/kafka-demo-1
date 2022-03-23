using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using demo.kafka;

const string KAFKA_TOPIC = "MYLOGEVENT-AVRO-KAFKA-TOPIC";

Console.WriteLine("Hello, Consumer!");

var consumerConfig = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "log-events",
    AutoOffsetReset = AutoOffsetReset.Latest,
};

var schemaRegistryConfig = new SchemaRegistryConfig
{
    Url = "localhost:9081" // By default the port is 8081, but in my case, there is McAffee which uses this 8081 port
};

using var cachedSchemaRegistryClient = new CachedSchemaRegistryClient(schemaRegistryConfig);
using var consumer = new ConsumerBuilder<string, MyLogEvent>(consumerConfig)
    .SetValueDeserializer(new AvroDeserializer<MyLogEvent>(cachedSchemaRegistryClient).AsSyncOverAsync())
    .Build();
consumer.Subscribe(KAFKA_TOPIC);

while (true)
{
    try
    {
        var result = consumer.Consume(100);
        if (result != null)
        {
            var level = result.Message.Value.LogLevel.ToString();
            DateTime eventDate = DateTimeOffset.FromUnixTimeMilliseconds(result.Message.Value.OccurenceTimeStamp).UtcDateTime;
            Console.WriteLine($"Received message: {eventDate} {level} {result.Message.Value.LogMessage}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Something went wrong while trying to consume: {ex.Message}");
    }

}