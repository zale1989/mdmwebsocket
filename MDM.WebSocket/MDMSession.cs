using SuperSocket.WebSocket.Server;

namespace MDM.WebSocket
{
    public class MDMSession : WebSocketSession
    {
        public string DeviceId { get; set; }
    }
}
