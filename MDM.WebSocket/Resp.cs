using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using SuperSocket.Command;
using SuperSocket.WebSocket.Server;
using System;
using System.Threading.Tasks;

namespace MDM.WebSocket
{
    class Resp : IAsyncCommand<MDMSession, MDMPackageInfo>
    {
        public async ValueTask ExecuteAsync(MDMSession session, MDMPackageInfo package)
        {
            //handler resp 
            Console.WriteLine($"resp is {package.MessageResp.IsSuccess},content:{package.MessageResp.RespContent}");
        }
    }
}
