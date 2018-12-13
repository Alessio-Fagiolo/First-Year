using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Fast2Da
{
    class NrgPowerUp : PowerUp
    {
        public NrgPowerUp(Vector2 spritePosition, string textureName="powerUp_Nrg") : base(spritePosition, textureName)
        {
        }

        protected override void OnAttach(Player player)
        {
            attachedPlayer = player;
            attachedPlayer.Nrg = attachedPlayer.MaxNrg;

            OnDetach();
        }
    }
}
