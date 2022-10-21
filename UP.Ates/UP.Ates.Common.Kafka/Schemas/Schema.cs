using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
#pragma warning disable CS0618

namespace UP.Ates.Common.Kafka.Schemas;


public static class Schemas
{
    private static Dictionary<string, string> _schemaCollection = new()
    {
        {
            "UP.Ates.TaskTracker.Contracts.Outgoing.PopugTask.v1.PopugTask", 
            @"{
                'description': 'Popug task v1',
                'type': 'object',
                'properties':
                {
                    'Id': {'type':'string'},
                    'Title': {'type':'string'},
                    'UserId': {'type':'string'},
                    'Status': {'type':'integer'},
                },
                'minProperties': 4,
                'additionalProperties': false
            }"
        },
        {
            "UP.Ates.TaskTracker.Contracts.Outgoing.PopugTask.v2.PopugTask",
            @"{
                'description': 'Popug task v2',
                'type': 'object',
                'properties':
                {
                    'Id': {'type':'string'},
                    'Title': {'type':'string'},
                    'JiraId': {'type':'string'},
                    'UserId': {'type':'string'},
                    'Status': {'type':'integer'},
                },
                'minProperties': 5,
                'additionalProperties': false
            }"
        }
    };

    public static void Validate(object message)
    {
        var contractFullName = message.GetType().FullName;
        if (_schemaCollection.TryGetValue(contractFullName, out var schemaString))
        {
            var schema = JsonSchema.Parse(schemaString);
            var p = JObject.FromObject(message);
            var valid = p.IsValid(schema);
            if (!valid)
                throw new Exception($"Schema not valid for message: {JsonConvert.SerializeObject(message)}");
        }
    }
}