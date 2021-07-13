using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SuperSocket;
using SuperSocket.Channel;
using SuperSocket.ProtoBase;
using SuperSocket.Server;
using SuperSocket.WebSocket;
using SuperSocket.WebSocket.Server;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace MDM.WebSocket
{
    public class CmppServer : SuperSocketService<WebSocketPackage>
    {
        private Timer requestTimer = null;
        private ILogger _logger;
        public CmppServer(IServiceProvider serviceProvider, IOptions<ServerOptions> serverOptions, ILogger<CmppServer> logger)
         : base(serviceProvider, serverOptions)
        {
            _logger = logger;
            double sendInterval = 6000;
            requestTimer = new Timer(sendInterval);
            requestTimer.Elapsed += RequestTimer_Elapsed;
            requestTimer.Enabled = true;
            requestTimer.Start();
        }

        private void RequestTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //发送请求报文
            var sessionList = this.GetSessionContainer()?.GetSessions<WebSocketSession>(w => w.State == SessionState.Connected);
            if (sessionList == null)
                return;
            foreach (var session in sessionList)
            {
                var ts = DateTime.Now - session.LastActiveTime;
                if (ts.TotalSeconds < 60)
                    continue;
                
            }
        }

        protected override async ValueTask OnSessionConnectedAsync(IAppSession session)
        {
            // do something right after the sesssion is connected
            await base.OnSessionConnectedAsync(session);
        }

        protected override async ValueTask OnSessionClosedAsync(IAppSession session, CloseEventArgs e)
        {
            // do something right after the sesssion is closed
            await base.OnSessionClosedAsync(session, e);
        }

        protected override async ValueTask OnStartedAsync()
        {
            // do something right after the service is started
            await base.OnStartedAsync();
        }

        protected override async ValueTask OnStopAsync()
        {
            await Task.Delay(0);
            if (requestTimer != null)
            {
                requestTimer.Stop();
                requestTimer.Close();
            }
            // do something right after the service is stopped
        }

        protected override async ValueTask<bool> OnSessionErrorAsync(IAppSession session, PackageHandlingException<WebSocketPackage> exception)
        {
            await Task.Delay(0);
            if (_logger != null)
                _logger.LogError(exception, $"[{DateTime.UtcNow.ToString("G")}]  Session[{session.SessionID}]: session exception.");
            return true;
        }
    }
}
