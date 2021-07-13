using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using SuperSocket.Command;
using SuperSocket.WebSocket.Server;
using System;
using System.Threading.Tasks;

namespace MDM.WebSocket
{
    class Connect : IAsyncCommand<WebSocketSession, MDMPackageInfo>
    {
        private readonly IServiceProvider _appServices;
        private IClusterClient clusterClient;
        public Connect(IHost host)
        {
            _appServices = host.Services;
        }

        IClusterClient _clusterClient
        {
            get
            {
                if (clusterClient == null)
                    clusterClient = _appServices.GetRequiredService<IClusterClient>();
                return clusterClient;
            }
        }

        public async ValueTask ExecuteAsync(WebSocketSession session, MDMPackageInfo package)
        {
            //var ss = (CmppServer)session.Server;
            //var ssd = ss.GetSessionContainer()?.GetSessions<WebSocketSession>(w => w.State == SessionState.Connected);
            var connectStatus = 0;
            if (package.MessageData.DeviceId != null)
            {
                connectStatus = 1;
            }
            var resp = new MDMMessageResp
            {
                MessageId = package.MessageData.MessageId,
                IsSuccess = connectStatus == 1 ? true : false
            };
            await session.SendAsync(resp.ToString());
        }
    }
}
