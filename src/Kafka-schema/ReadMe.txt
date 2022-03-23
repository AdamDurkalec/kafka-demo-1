How to delete a schema in registry:
curl -X DELETE http://localhost:9081/subjects/MYLOGEVENT-KAFKA-TOPIC-value 
LogEventTopic-value is a schema name

How to generate the model classes from the schema:
avrogen -s .\AvroSchema\MyLogEvent.avsc .