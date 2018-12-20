using Microsoft.Xna.Framework;
using Nez;

namespace Joeba.Scripts.Components
{
    public class CameraBounds : Component, IUpdatable
    {
        public Vector2 min, max;


        public CameraBounds()
        {
            // make sure we run last so the camera is already moved before we evaluate its position
            setUpdateOrder(int.MaxValue);
        }


        public CameraBounds(Vector2 min, Vector2 max) : this()
        {
            this.min = min;
            this.max = max;
        }

        public CameraBounds(Vector2 max) : this(Vector2.Zero,max)
        {
            this.max = max;
        }


        public override void onAddedToEntity()
        {
            entity.updateOrder = int.MaxValue;
        }

        public void UpdateBounds()
        {
            max = new Vector2(Game1.TileSize * (Game1.CurrentMap.width - 1), Game1.TileSize * (Game1.CurrentMap.width - 1));
        }


        void IUpdatable.update()
        {
            var cameraBounds = entity.scene.camera.bounds;

            if (cameraBounds.top < min.Y)
                entity.scene.camera.position += new Vector2(0, min.Y - cameraBounds.top);

            if (cameraBounds.left < min.X)
                entity.scene.camera.position += new Vector2(min.X - cameraBounds.left, 0);

            if (cameraBounds.bottom > max.Y)
                entity.scene.camera.position += new Vector2(0, max.Y - cameraBounds.bottom);

            if (cameraBounds.right > max.X)
                entity.scene.camera.position += new Vector2(max.X - cameraBounds.right, 0);
        }
    }
}
