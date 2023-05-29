using System.Text.Json;

namespace MCBE_WSS
{

    public class CommandBuilder
    {
        private CommandStructure commandStructure = new();

        public CommandBuilder SetCommand(string command)
        {
            commandStructure.body.commandLine = command;
            return this;
        }

        public string Build()
        {
            if (string.IsNullOrWhiteSpace(commandStructure.body.commandLine))
                throw new Exception("Error. Command must be set!");

            string convert = JsonSerializer.Serialize(commandStructure);
            return convert;
        }

        private class CommandStructure
        {
            public CommandHeaders header { get; set; } = new();
            public CommandBody body { get; set; } = new();
        }

        private class CommandHeaders
        {
            public string requestId { get; set; } = Guid.NewGuid().ToString();
            public string messagePurpose { get; set; } = "commandRequest";
            public int version { get; set; } = 1;
            public string messageType { get; set; } = "commandRequest";
        }

        private class CommandBody
        {
            public CommandOrigin origin = new();
            public string commandLine { get; set; } = "";
            public int version { get; set; } = 1;
        }

        private class CommandOrigin
        {
            public string type = "player";
        }
    }
}
