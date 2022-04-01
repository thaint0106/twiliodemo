using System;
namespace Demo.Models
{
    public class Channel
    {
        public Channel()
        {
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string ServiceId { get; set; }
        public ChannelAtribute Atribute { get; set; }
    }
}
