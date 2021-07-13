using Newtonsoft.Json;

namespace MDM.WebSocket
{
    public class MDMMessage<T>
    {
        public string MessageId { get; set; }

        public EnumMessageType MessageType { get; set; }

        public T MessageContent { get; set; }

        public override string ToString()
        {
            var message = JsonConvert.SerializeObject(this);
            return message;
        }
    }

    public class NoticeDto
    {
        public string Title { get; set; }

        public string Content { get; set; }
    }

    public enum EnumMessageType
    {
        Notice = 1,
        Command = 2
    }

}
