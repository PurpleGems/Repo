
using Joeba.Scripts.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;

namespace Joeba.Scripts.Items
{
    class TestSeed : Component, IUpdatable
    {
        private Sprite seedSprite;
        private int nGrowthStages;
        private int currentGrowthStage = 0;
        public bool bIsFullyGrown { get; set; }

        public TestSeed(int seedIndex,int nGrowthStages)
        {
            
            currentGrowthStage = seedIndex;
            this.nGrowthStages = nGrowthStages + seedIndex;
        }

        public override void onAddedToEntity()
        {
            seedSprite = entity.addComponent(new Sprite(GlobalSpritesheets.CropsSplit[currentGrowthStage]));
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
