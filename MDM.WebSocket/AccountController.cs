using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperSocket;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MDM.WebSocket
{
    public class AccountController : ControllerBase
    {
        private readonly IHostedService _mdmServer;
        public AccountController(IHost host)
        {
            _mdmServer = host.Services.GetRequiredService<IHostedService>();
        }

        [Route("test")]
        public string Index()
        {
            return "hello api";
        }

        [HttpPost("/android/devices/{deviceId}/notice")]
        public async Task<IActionResult> NoticeCommandAsync(string deviceId, [FromBody] DeviceNoticeRequest request)
        {
            var session = ((MDMServer)_mdmServer)
                .GetSessionContainer()?
                .GetSessions<MDMSession>(w => w.DeviceId == deviceId && w.State == SessionState.Connected)
                .FirstOrDefault();
            if (session != null)
            {
                var notice = new NoticeDto
                {
                    Title = request.Title,
                    Content = request.Content
                };
                var message = new MDMMessage<NoticeDto>
                {
                    MessageId = request.MessageId,
                    MessageType = EnumMessageType.Command,
                    MessageContent = notice
                };
                await session.SendAsync(message.ToString());
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost("/android/devices/{deviceId}/command")]
        public async Task<IActionResult> DeviceCommandAsync(string deviceId, [FromBody] DeviceCommandRequest request)
        {
            var session = ((MDMServer)_mdmServer)
                .GetSessionContainer()?
                .GetSessions<MDMSession>(w => w.DeviceId == deviceId && w.State == SessionState.Connected)
                .FirstOrDefault();
            if (session != null)
            {
                var message = new MDMMessage<string>
                {
                    MessageId = request.MessageId,
                    MessageType = EnumMessageType.Command,
                    MessageContent = request.CommandType
                };
                await session.SendAsync(message.ToString());
                return Ok(true);
            }
            return Ok(false);
        }
    }

    public class DeviceCommandRequest
    {
        public string MessageId { get; set; }
        public string CommandType { get; set; }
    }

    public class DeviceNoticeRequest
    {
        public string MessageId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
