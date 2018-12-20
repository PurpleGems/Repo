using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joeba.Scripts.Components;
using Joeba.Scripts.Graphics;
using Nez;
using Nez.Sprites;

namespace Joeba.Scripts
{
    class HoedDirt : Component
    {
        
        public override void onAddedToEntity()
        {
            var spr = entity.addComponent(new Sprite(GlobalSpritesheets.HoedDirtSplit[0]));
            spr.setRenderLayer(88);
            entity.addComponent(new GridSnap(false));
        }
    }
}
