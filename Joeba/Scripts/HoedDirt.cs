using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joeba.Scripts.Components;
using Joeba.Scripts.Graphics;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;

namespace Joeba.Scripts
{
    class HoedDirt : Component
    {
        private Vector2 dirtPosition;
        public HoedDirt(Vector2 position)
        {
            dirtPosition = position;
        }

        public override void onAddedToEntity()
        {
            var spr = entity.addComponent(new Sprite(GlobalSpritesheets.HoedDirtSplit[0]));
            spr.setRenderLayer(88);
            entity.addComponent(new GridSnap((int) dirtPosition.X,(int)dirtPosition.Y,false));
        }
    }
}
