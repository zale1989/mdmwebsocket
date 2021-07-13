using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.WebSocket
{
    public class MDMMessageResp
    {
        public string MessageId { get; set; }

        public bool IsSuccess { get; set; }

        public string RespContent { get; set; }

        public override string ToString()
        {
            var message = JsonConvert.SerializeObject(this);
            return message;
        }
    }
}
