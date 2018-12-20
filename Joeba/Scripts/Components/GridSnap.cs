using System;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;

namespace Joeba.Scripts.Components
{
    /// <summary>
    /// When added to an entity it will snap its position to the grid. (example: When creating a chest it would snap it onto the grid)
    /// </summary>
    class GridSnap : Component
    {
        private new Vector2 originOffset;
        private new Vector2 positionInGrid;
        private Sprite sprite;
        private bool changeRenderLayer = false;

        public GridSnap(int xPosition, int yPosition)
        {
            positionInGrid = new Vector2(xPosition, yPosition);
            originOffset = Vector2.Zero;
            changeRenderLayer = true;
        }

        public GridSnap(int xPosition, int yPosition, Vector2 OriginOffset) : this(xPosition,yPosition)
        {          
            this.originOffset = OriginOffset;
        }


        public GridSnap(int xPosition, int yPosition,bool ChangeRenderLayer) : this(xPosition,yPosition)
        {
            changeRenderLayer = ChangeRenderLayer;
        }

        



        public override void onAddedToEntity()
        {
            sprite = entity.getComponent<Sprite>();
           
            sprite.setOrigin(originOffset);

            if (changeRenderLayer)
                sprite.setRenderLayer((int)Game1.Layers.PlayerLayer);

            
                entity.setPosition(new Vector2((int)positionInGrid.X * 16, (int)positionInGrid.Y * 16));
                Console.WriteLine(entity.position);
            

            float inversedDepth = 1 - (entity.position.Y / 100000);
            sprite.setLayerDepth(inversedDepth);
        }

    }
}
