using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameEngine
{
    public class GameSounds
    {
        public SoundPlayer ImpactSound = null;
        public SoundPlayer StretchSound = null;
        public SoundPlayer GlassTing = null;

        public GameSounds()
        {
            System.IO.Stream str = Properties.Resources.ImpactDrum;
            ImpactSound = new System.Media.SoundPlayer(str);
            str = Properties.Resources.Stretch;
            StretchSound = new System.Media.SoundPlayer(str);
            str = Properties.Resources.GlassTing;
            GlassTing = new System.Media.SoundPlayer(str);
        }





    }
}
