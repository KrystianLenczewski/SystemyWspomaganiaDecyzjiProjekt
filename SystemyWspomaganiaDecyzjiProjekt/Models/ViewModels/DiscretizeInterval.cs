using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemyWspomaganiaDecyzjiProjekt.Models.ViewModels
{
    public class DiscretizeInterval
    {
        public string Label { get; set; } = string.Empty;
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
    }
}
