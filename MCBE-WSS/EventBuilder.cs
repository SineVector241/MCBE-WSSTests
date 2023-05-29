using System;
using System.ComponentModel;
using System.Text.Json;

namespace MCBE_WSS
{
    public enum EventType
    {
        //Player Events
        [Description("PlayerJoin")]
        PlayerJoin,
        [Description("PlayerMessage")]
        PlayerMessage,
        [Description("PlayerTravelled")]
        PlayerTravelled,
        [Description("PlayerTransform")]
        PlayerTransform,


        [Description("ItemDropped")]
        ItemDropped,
        [Description("EntitySpawned")]
        EntitySpawned,
        [Description("StartWorld")]
        StartWorld,
        [Description("PlayerDied")]
        PlayerDied

    }

    public class EventBuilder
    {
        private EventStructure eventStructure = new();

        public EventBuilder SetMessagePurpose(string messagePurpose)
        {
            eventStructure.header.messagePurpose = messagePurpose;
            return this;
        }

        public EventBuilder SetEventType (EventType eventType)
        {
            eventStructure.body.eventName = eventType.ToDescriptionString();
            return this;
        }

        public string Build()
        {
            if (string.IsNullOrWhiteSpace(eventStructure.body.eventName))
                throw new Exception("Error. EventType must be set!");

            string convert = JsonSerializer.Serialize(eventStructure);
            return convert;
        }

        private class EventStructure
        {
            public EventHeaders header { get; set; } = new();
            public EventBody body { get; set; } = new();
        }

        private class EventHeaders
        {
            public string requestId { get; set; } = Guid.NewGuid().ToString();
            public string messagePurpose { get; set; } = "subscribe";
            public int version { get; set; } = 1;
        }

        private class EventBody
        {
            public string eventName { get; set; } = "";
        }
    }
}
