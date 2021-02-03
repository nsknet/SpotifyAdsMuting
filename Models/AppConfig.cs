using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAdsMuting.Models
{
    class AppConfig
    {
        public string SpotifyPath { get; set; }
        public bool AutoMute { get; set; }
        public bool StartWithWindows { get; set; }

    }
}
