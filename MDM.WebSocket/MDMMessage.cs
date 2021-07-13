namespace MDM.WebSocket
{
    public class MDMMessage
    {
        public string MessageId { get; set; }

        public string DeviceId { get; set; }

        public string MessageType { get; set; }

        public string OperationType { get; set; }

        public string Content { get; set; }
    }
}
