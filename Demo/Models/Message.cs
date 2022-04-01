using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Message
    {
        public string From { get; set; }
        public string Content { get; set; }
        public string ChannelId { get; set; }
        public string ConversitionId { get; set; }
        public string Author { get; set; }
    }
}
