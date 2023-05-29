using Fleck;

namespace MCBE_WSS
{
    class Program
    {
        public static void Main(string[] args)
        {
            WebSocketServer server = new WebSocketServer("ws://0.0.0.0:8080");
            var allSockets = new List<IWebSocketConnection>();

            server.Start(socket => {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("Connection Established!");
                    allSockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    Console.WriteLine("Connection Closed!");
                    allSockets.Remove(socket);
                };
                socket.OnMessage = message => Console.WriteLine(message);
                socket.OnBinary = binary => Console.WriteLine("Received Binary");
                socket.OnError = error => Console.WriteLine("Error!");
            });

            while (true)
            {
                Console.ReadLine();
                var packet = new EventBuilder().SetEventType(EventType.PlayerTransform).Build();
                Console.WriteLine(packet);
                foreach (var socket in allSockets.ToList())
                {
                    socket.Send(packet).Wait();
                }
            }
        }
    }
}