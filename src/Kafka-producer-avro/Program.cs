using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using demo.kafka;

const string KAFKA_TOPIC = "MYLOGEVENT-AVRO-KAFKA-TOPIC";

Console.WriteLine("Hello, kafka-producer!");

var producerConfig = new ProducerConfig
{
    BootstrapServers = "localhost:9092"
};

var schemaRegistryConfig = new SchemaRegistryConfig
{
    Url = "localhost:9081" // By default the port is 8081, but in my case, there is McAffee which uses this 8081 port
};

var cachedSchemaRegistryClient = new CachedSchemaRegistryClient(schemaRegistryConfig);
var producer = new ProducerBuilder<string, MyLogEvent>(producerConfig)
    .SetValueSerializer(new AvroSerializer<MyLogEvent>(cachedSchemaRegistryClient))
    .Build();

Console.WriteLine("Choose log level: I - Info, D - Debug, W - Warning, E - Error");
var logLevelChar = Console.ReadLine().ToUpper();
LogLevel logLevel;
switch (logLevelChar)
{
    case "I": logLevel = LogLevel.Info; break;
    case "D": logLevel = LogLevel.Debug; break;
    case "W": logLevel = LogLevel.Warning; break;
    case "E": logLevel = LogLevel.Error; break;
    default: logLevel = LogLevel.Debug; break;
}

while (true)
{
    var date = DateTime.UtcNow;
    DateTimeOffset dateTimeOffset = date;
    long milisecondsTimeStamp = dateTimeOffset.ToUnixTimeMilliseconds();

    var logEvent = new MyLogEvent
    {
        LogMessage = Faker.Lorem.Sentence(),
        LogLevel = logLevel,
        OccurenceTimeStamp = milisecondsTimeStamp
    };

    try
    {
        await producer.ProduceAsync(KAFKA_TOPIC, new Message<string, MyLogEvent>() { Key = "k1", Value = logEvent });
        DateTime eventDate = DateTimeOffset.FromUnixTimeMilliseconds(logEvent.OccurenceTimeStamp).UtcDateTime;
        Console.WriteLine($"Sent message: {logLevel} {eventDate} {logEvent.LogMessage}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Something went wrong: {ex.Message}");
    }
    Task.Delay(3000).Wait();
}

