using System;
using Joeba.Scripts.Graphics;
using Joeba.Scripts.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using Nez.Tiled;

namespace Joeba.Scripts.Components
{
    
    /// <summary>
    /// Entity will follow the mouse cursor in a snap fashion (Example: Red border around hovered tile)
    /// </summary>

    class MouseGridSnap : Component, IUpdatable
    {
        private int currentSeed = 0;
        private Sprite sprite;

        public override void onAddedToEntity()
        {
            sprite = entity.getComponent<Sprite>();

            sprite.setOrigin(Vector2.Zero);
        }



        public void update()
        {
            entity.setPosition(new Vector2(Input.MousePositionInGrid().X, Input.MousePositionInGrid().Y));

            //CreatePlantSeed();
            //HoeDirt();

            //TODO REMOVE TESTING CODE
            if (Input.isKeyPressed(Keys.D1))
                currentSeed = 0;
            else if (Input.isKeyPressed(Keys.D2))
                currentSeed = 1;
            else if (Input.isKeyPressed(Keys.D3))
                currentSeed = 2;
        }



//        private void CreatePlantSeed()
//        {
//            if (Input.leftMouseButtonPressed)
//            {
//                string value = "";
//
//                if (Helper.DoesTileHaveProperty((int)Input.MousePositionInGridTile().X,(int)Input.MousePositionInGridTile().Y,"Diggable","Back"))
//                {
//                    Entity tempPlant = entity.scene.createEntity("Seedling");
//                    switch (currentSeed)
//                    {
//                    case 0:
//                        tempPlant.addComponent(new TestSeed(56, 6));
//                        break;
//                    case 1:
//                        tempPlant.addComponent(new TestSeed(0, 5));
//                        break;
//                    case 2:
//                        tempPlant.addComponent(new TestSeed(72, 6));
//                        break;
//                    }
//
//                   tempPlant.addComponent(new GridSnap((int)Input.MousePositionInGridTile().X,(int) Input.MousePositionInGridTile().Y, new Vector2(0,16)));
//                   tempPlant.addComponent(new MovementShake());
//                }
//            }
//        }


    }
 }
        

