using Joeba.Scripts.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joeba.Scripts.Components;
using Microsoft.Xna.Framework;

namespace Joeba.Scripts.Items.Crops
{
    class Crop : Component, IUpdatable
    {
        private Sprite seedSprite;
        private int nGrowthStages;
        private int currentGrowthStage = 0;
        public bool bIsFullyGrown { get; set; }

        public Crop()
        {

        }

        public Crop(int seedIndex, int nGrowthStages)
        {
            
            currentGrowthStage = seedIndex;
            this.nGrowthStages = nGrowthStages + seedIndex;
            
        }

        public override void onAddedToEntity()
        {
            seedSprite = entity.addComponent(new Sprite(GlobalSpritesheets.CropsSplit[currentGrowthStage]));
            seedSprite.setRenderLayer((int)Game1.Layers.PlayerLayer);

            entity.addComponent(new MovementShake());

        }

        public void update()
        {
            if (Input.isKeyPressed(Keys.T) && !bIsFullyGrown)
            {
                currentGrowthStage++;
                seedSprite.setSubtextureOriginalOrigin(GlobalSpritesheets.CropsSplit[currentGrowthStage]);

                if (currentGrowthStage == nGrowthStages)
                    bIsFullyGrown = true;
            }
        }
    }
}
