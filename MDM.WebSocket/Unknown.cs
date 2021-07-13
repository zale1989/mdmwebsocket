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
                MessageId = package.MessageData.MessageId,
                IsSuccess = false,
                RespContent = "unknown message:" + package.MessageData.Content
            };
            await session.SendAsync(resp.ToString());
        }
    }
}
