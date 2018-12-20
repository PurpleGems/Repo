using Joeba.Scripts.Characters;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joeba.Scripts.Components
{
    class Swipe : Component, IUpdatable
    {
        Sprite sprite;
        float attack_speed = 0.35f;
        int arcAmount = 160;

        float startingArc = 0; // 

        float swordRPS = 0; //Sword rotation per seconds (How many degrees we move each frames);
        float nDegreesCounter = 0;

        public Swipe(Sprite in_sprite)
        {
            sprite = in_sprite;

        }

        Entity childEntity;
        public override void onAddedToEntity()
        {
            childEntity = entity.scene.createEntity("parentSwipe", Core.scene.findEntity("Player").position);

            sprite = childEntity.addComponent(new Sprite(sprite.subtexture));
            sprite.transform.setRotationDegrees(-45);
            childEntity.addComponent(new BoxCollider(8, 8));
            childEntity.setParent(entity);
            childEntity.setPosition(new Microsoft.Xna.Framework.Vector2(-16, -16));




            switch (PlayerFarmer.Facing)
            {
                case PlayerFarmer.FacingDirection.North:
                    startingArc = -90;
                    break;
                case PlayerFarmer.FacingDirection.South:
                    startingArc = 90;
                    break;
                case PlayerFarmer.FacingDirection.West:
                    startingArc = 180;
                    break;
                case PlayerFarmer.FacingDirection.East:
                    startingArc = 0;
                    break;
                default:
                    break;
            }

            swordRPS = arcAmount / (attack_speed * 60);
            startingArc += 45; //  there is a 45 degree offset in the swing
        }



        public void update()
        {
            entity.setLocalRotationDegrees(startingArc += swordRPS);
            sprite.transform.setLocalRotationDegrees(-90);

            nDegreesCounter += swordRPS;

            if (nDegreesCounter >= arcAmount)
                entity.destroy();

        }
    }
}
