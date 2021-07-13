using Newtonsoft.Json;
using SuperSocket.Command;
using SuperSocket.ProtoBase;
using SuperSocket.WebSocket;
using System;
using System.Linq;

namespace MDM.WebSocket
{
    class MDMPackageConverter : IPackageMapper<WebSocketPackage, MDMPackageInfo>
    {
        public MDMPackageInfo Map(WebSocketPackage package)
        {
            try
            {
                Console.WriteLine("receive message:" + package.Message);
                var mdmMessgae = JsonConvert.DeserializeObject<MDMMessageResp>(package.Message);
                var pack = new MDMPackageInfo();
                pack.Key = "Resp";
                pack.MessageResp = mdmMessgae;
                return pack;
            }
            catch (Exception ex)
            {
                return new MDMPackageInfo { Key = "Unknown", MessageResp = new MDMMessageResp {  RespContent = package.Message } };
            }

        }
    }
}
