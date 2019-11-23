using System;

namespace digitalsign.common.Settings
{
    public class JwtSettings
    {
        public string Secret { get; set; }

        public TimeSpan TokenLifeTime { get; set; }
    }
}
