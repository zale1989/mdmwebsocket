using Newtonsoft.Json;
using SuperSocket;
using SuperSocket.Command;
using SuperSocket.ProtoBase;
using SuperSocket.WebSocket.Server;
using System.Linq;
using System.Threading.Tasks;

namespace MDM.WebSocket
{
    class Unknown : IAsyncCommand<WebSocketSession, MDMPackageInfo>
    {
        public async ValueTask ExecuteAsync(WebSocketSession session, MDMPackageInfo package)
        {
            var resp = new MDMMessageResp
            {
                MessageId = package.MessageResp.MessageId,
                IsSuccess = false,
                RespContent = "unknown message:" + package.MessageResp.RespContent
            };
            await session.SendAsync(resp.ToString());
        }
    }
}
