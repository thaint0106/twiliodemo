using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class UserChannel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserResourceId { get; set; }
        public string Email { get; set; }
        public string ChannelId { get; set; }
        public string ConversitionId { get; set; }
    }
}
