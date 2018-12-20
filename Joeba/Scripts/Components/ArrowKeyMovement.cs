using Microsoft.Xna.Framework;
using Nez;

namespace Joeba.Scripts.Components
{

        class ArrowKeyMovement : Component, IUpdatable
        {
            int speed = 30;
            VirtualIntegerAxis xAxis;
            VirtualIntegerAxis yAxis;
            Mover _mover;
            public override void onAddedToEntity()
            {
                _mover = entity.addComponent(new Mover());
                xAxis = new VirtualIntegerAxis();
                yAxis = new VirtualIntegerAxis();

                xAxis.nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.CancelOut, Microsoft.Xna.Framework.Input.Keys.Left, Microsoft.Xna.Framework.Input.Keys.Right));
                yAxis.nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.CancelOut, Microsoft.Xna.Framework.Input.Keys.Up, Microsoft.Xna.Framework.Input.Keys.Down));
            }

            public void update()
            {
                Vector2 movementVel = new Vector2(xAxis, yAxis);

                CollisionResult res;
                _mover.move(movementVel * speed * Time.deltaTime, out res);
            }
        }
}
