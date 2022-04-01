using System;
namespace Demo.Models
{
    public class ChannelAtribute
    {
        public ChannelAtribute()
        {
        }

        public ChannelAtribute(string carteId)
        {
            CarteId = carteId;
        }

        public string CarteId { get; set; }
    }
}
