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
        private Sprite sprite;
        private bool changeRenderLayer = false;
        private bool bMouseSnap = false;

        public GridSnap()
        {
            originOffset = Vector2.Zero;
            changeRenderLayer = true;
            bMouseSnap = true;
        }

        public GridSnap(Vector2 OriginOffset) : this()
        {          
            this.originOffset = OriginOffset;
        }

        public GridSnap(bool ChangeRenderLayer) : this (ChangeRenderLayer, true)
        {

        }

        public GridSnap(bool ChangeRenderLayer, bool mouseSnap)
        {
            changeRenderLayer = ChangeRenderLayer;
            bMouseSnap = mouseSnap;
        }

        public override void onAddedToEntity()
        {
            sprite = entity.getComponent<Sprite>();
           
            sprite.setOrigin(originOffset);

            if (changeRenderLayer)
                sprite.setRenderLayer((int)Game1.Layers.PlayerLayer);

            if(bMouseSnap)
                entity.setPosition(Input.MousePositionInGrid());
            else
            {
                entity.setPosition(Helper.GetGridPosition(new Vector2(entity.position.X, entity.position.Y)));
                Console.WriteLine(entity.position);

            }

            float inversedDepth = 1 - (entity.position.Y / 100000);
            sprite.setLayerDepth(inversedDepth);
        }

    }
}
