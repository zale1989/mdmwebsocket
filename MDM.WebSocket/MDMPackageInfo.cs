using SuperSocket.ProtoBase;

namespace MDM.WebSocket
{
    public class MDMPackageInfo : IKeyedPackageInfo<string>
    {
        public string Key { get; set; }

        public MDMMessageResp MessageResp { get; set; }
    }
}
