using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace Dotel2.SignalR
{
    public class MessageHub:Hub
    {
        private static ConcurrentDictionary<int, string> _connections = new();

        public override Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var userIdStr = httpContext?.Request.Query["userId"];

            if (int.TryParse(userIdStr, out int userId))
            {
                _connections[userId] = Context.ConnectionId;
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var kv = _connections.FirstOrDefault(x => x.Value == Context.ConnectionId);
            if (!kv.Equals(default(KeyValuePair<int, string>)))
            {
                _connections.TryRemove(kv.Key, out _);
            }

            return base.OnDisconnectedAsync(exception);
        }

        public static string? GetConnectionId(int userId)
        {
            _connections.TryGetValue(userId, out string? connId);
            return connId;
        }
    }
}
