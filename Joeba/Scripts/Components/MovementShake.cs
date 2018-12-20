using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez;

namespace Joeba.Scripts.Components
{

    /// <summary>
    /// When added onto an entity if something with a collider walks over it, it will "Shake". (adds a BoxCollider Triger)
    /// </summary>
    class MovementShake : Component, IUpdatable
    {
        private bool bIsShaking = false;

        //how fast the shake is
        private int rotationVelocity = 40;

        private int rotationAmount = 12;
        //when this is added to an entity if something collides with it it will do a "shake" will move 20 degrees on direction and go back to 0


        public override void onAddedToEntity()
        {
            entity.addComponent(new BoxCollider( 0,0,16,16 )).isTrigger = true;
        }

        public void update()
        {
            if (bIsShaking == true)
            {
                entity.rotationDegrees += Time.deltaTime * rotationVelocity;
                if (entity.rotationDegrees > rotationAmount)
                {
                    rotationVelocity *= -1;
                    entity.rotationDegrees = rotationAmount;
                }

                if (entity.rotationDegrees < 0)
                {
                    entity.rotationDegrees = 0;
                    rotationVelocity *= -1;
                    bIsShaking = false;
                }
            }
        }

        public void Shake()
        {
            bIsShaking = true;
        }

       
    }
}
