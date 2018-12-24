using System;
using Joeba.Scripts.Items;
using Joeba.Scripts.Items.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using System.Collections.Generic;
using Joeba.Scripts.Components;
using Joeba.Scripts.Graphics;
using Joeba.Scripts.Graphics.GUI;
using Joeba.Scripts.Items.Crops;
using Joeba.Scripts.Items.Tools;


namespace Joeba.Scripts.Characters
{
    class PlayerFarmer : Component, ITriggerListener, IUpdatable
    {
        int move_speed = 50; // Used to be 50
        private int anim_fps = 11;
        private int originOffSetY = 28;
        

        public enum FacingDirection
        {
            North,
            South,
            West,
            East
        }

        public static FacingDirection Facing;

        enum AnimationsBase
        {
            WalkNorth,
            WalkSouth,
            WalkWest,
            WalkEast,
        }

        enum AnimationsLegs
        {
            WalkNorth,
            WalkSouth,
            WalkWest,
            WalkEast,
        }

        enum AnimationsArms
        {
            WalkNorth,
            WalkSouth,
            WalkWest,
            WalkEast,
        }

        //Components declaration for easy use in other parts of the class
        Item heldItem = new Hoe("Iridium Hoe", new Sprite(GlobalSpritesheets.WeaponsSplit[1]));
        Sprite<AnimationsBase> animationsBase;
        Sprite<AnimationsLegs> animationsLegs;
        Sprite<AnimationsArms> animationsArms;
        Mover mover;
        private InventoryBar inventoryBar;

        //Input Handling for this Player Component//

        //Movement Input
        VirtualIntegerAxis xAxisInput;
        VirtualIntegerAxis yAxisInput;

        //Buttons Input/Actions
        //VirtualButton actionInput;

        public override void onAddedToEntity()
        {
            var BaseTexture = GlobalSpritesheets.BaseCharacterSplit;
            
            
            //size 10,8
            var col = entity.addComponent(new BoxCollider(10,8));
            
            var camera = entity.addComponent(new FollowCamera(entity, FollowCamera.CameraStyle.LockOn));
            camera.addComponent(new CameraBounds(new Vector2(0, 0), new Vector2(Game1.TileSize * (Game1.CurrentMap.width - 1), Game1.TileSize * (Game1.CurrentMap.height - 1))));

            Flags.setFlagExclusive(ref col.physicsLayer, 1);
            Flags.setFlagExclusive(ref col.collidesWithLayers, 0);

            //Initalize all this components other components
            //boxCollider = entity.addComponent(new BoxCollider());
            mover = entity.addComponent(new Mover());
            //we add onto our player the component of "Sprite" with animationsBase and we set its first texture
            animationsBase = entity.addComponent(new Sprite<AnimationsBase>(BaseTexture[0])); // all the other animationsBase collision are based off this also the starting sprite
            animationsLegs = entity.addComponent(new Sprite<AnimationsLegs>(BaseTexture[6])); // all the other animationsBase collision are based off this also the starting sprite
            animationsArms = entity.addComponent(new Sprite<AnimationsArms>(BaseTexture[3])); // all the other animationsBase collision are based off this also the starting sprite
            //heldItem = new WoodenSword();

            //---BASE ANIMATIONS FOR WALKING---\\
            animationsBase.addAnimation(AnimationsBase.WalkSouth, new SpriteAnimation(new List<Subtexture>()
            {
               BaseTexture[16],
               BaseTexture[17],
               BaseTexture[18]
            }).setOrigin(new Vector2(8, originOffSetY)).setFps(anim_fps));

            animationsBase.addAnimation(AnimationsBase.WalkNorth, new SpriteAnimation(new List<Subtexture>()
            {
                BaseTexture[32],
                BaseTexture[33],
                BaseTexture[34]
            }).setOrigin(new Vector2(8, originOffSetY)).setFps(anim_fps));

            animationsBase.addAnimation(AnimationsBase.WalkEast, new SpriteAnimation(new List<Subtexture>()
            {
                BaseTexture[0],
                BaseTexture[2],
                BaseTexture[1],
                BaseTexture[2],
                BaseTexture[0],
                BaseTexture[4],
                BaseTexture[3],
                BaseTexture[4]
            }).setOrigin(new Vector2(8, originOffSetY)).setFps(anim_fps)).setRenderLayer((int)Game1.Layers.PlayerLayer);

            //---LEGS ANIMATIONS FOR WALKING---\\
            animationsLegs.addAnimation(AnimationsLegs.WalkEast, new SpriteAnimation(new List<Subtexture>()
            {
                BaseTexture[5],
                BaseTexture[7],
                BaseTexture[6],
                BaseTexture[7],
                BaseTexture[5],
                BaseTexture[9],
                BaseTexture[8],
                BaseTexture[9]
            }).setOrigin(new Vector2(8, originOffSetY)).setFps(anim_fps));



            animationsLegs.addAnimation(AnimationsLegs.WalkNorth, new SpriteAnimation(new List<Subtexture>()
            {
                BaseTexture[37],
                BaseTexture[38],
                BaseTexture[39]
            }).setOrigin(new Vector2(8, originOffSetY)).setFps(anim_fps));

            animationsLegs.addAnimation(AnimationsLegs.WalkSouth, new SpriteAnimation(new List<Subtexture>()
            {
                BaseTexture[21],
                BaseTexture[22],
                BaseTexture[23]
            }).setOrigin(new Vector2(8, originOffSetY)).setFps(anim_fps)).setRenderLayer((int)Game1.Layers.PlayerLayer);

            //---ARMS ANIMATIONS FOR WALKING---\\
            animationsArms.addAnimation(AnimationsArms.WalkSouth, new SpriteAnimation(new List<Subtexture>()
            {
                BaseTexture[24]
            }).setOrigin(new Vector2(8, originOffSetY)).setFps(anim_fps));

            animationsArms.addAnimation(AnimationsArms.WalkNorth, new SpriteAnimation(new List<Subtexture>()
            {
                BaseTexture[40]
            }).setOrigin(new Vector2(8, originOffSetY)).setFps(anim_fps));

            animationsArms.addAnimation(AnimationsArms.WalkEast, new SpriteAnimation(new List<Subtexture>()
            {
                BaseTexture[11],
                BaseTexture[11],
                BaseTexture[12],
                BaseTexture[11],
                BaseTexture[11],
                BaseTexture[12],
                BaseTexture[12],
                BaseTexture[12]
            }).setOrigin(new Vector2(8, originOffSetY)).setFps(anim_fps)).setRenderLayer((int)Game1.Layers.PlayerLayer);



            SetupInput();

            //Inventory Stuff
            inventoryBar = entity.addComponent(new InventoryBar());
            

        }

        void SetupInput()
        {
            xAxisInput = new VirtualIntegerAxis();
            xAxisInput.nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.CancelOut, Keys.A, Keys.D));
            xAxisInput.nodes.Add(new VirtualAxis.GamePadDpadLeftRight());

            yAxisInput = new VirtualIntegerAxis();
            yAxisInput.nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.CancelOut, Keys.W, Keys.S));
            yAxisInput.nodes.Add(new VirtualAxis.GamePadDpadUpDown());
        }



        AnimationsBase animBase = AnimationsBase.WalkSouth;
        AnimationsLegs animLegs = AnimationsLegs.WalkSouth;
        AnimationsArms animArms = AnimationsArms.WalkSouth;

        public void update()
        {
            
            ChangingInventorySlots();

            if (Input.isKeyPressed(Keys.P))
            {
                Vector2 mousePosition = Input.MousePositionInGridTile();
                if (Game1.HoedSpots.ContainsKey(mousePosition) && TileWithinRange())
                {
                    if (Game1.HoedSpots[mousePosition].getComponent<HoedDirt>().CanPlantSeed())
                    {
                        Game1.HoedSpots[mousePosition].getComponent<HoedDirt>().PlantSeed((int)mousePosition.X, (int)mousePosition.Y, new Crop(0, 6));
                    }
                    else {
                        Debug.log( "SEED ALREADY PLANTED HERE - Player Class");
                    }
                }
                else { Debug.log("cant plant a seed here, tile hasnt been dug up or too far away. - Player Class");}
            }

            var moveDirection = new Vector2(xAxisInput.value, yAxisInput.value);

            //Settings the proper animationsBase for movement
            if (moveDirection.X < 0)
            {
                animationsBase.flipX = true;
                animationsLegs.flipX = true;
                animationsArms.flipX = true;

                animBase = AnimationsBase.WalkEast; Facing = FacingDirection.West;
                animLegs = AnimationsLegs.WalkEast;
                animArms = AnimationsArms.WalkEast;



            }
            else if (moveDirection.X > 0)
            {
                animationsBase.flipX = false;
                animationsLegs.flipX = false;
                animationsArms.flipX = false;

                animBase = AnimationsBase.WalkEast; Facing = FacingDirection.East;
                animLegs = AnimationsLegs.WalkEast;
                animArms = AnimationsArms.WalkEast;
            }

            if (moveDirection.Y < 0)
            {
                animBase = AnimationsBase.WalkNorth; Facing = FacingDirection.North;
                animArms = AnimationsArms.WalkNorth;
                animLegs = AnimationsLegs.WalkNorth;
            }
            else if (moveDirection.Y > 0)
            {
                animBase = AnimationsBase.WalkSouth; Facing = FacingDirection.South;
                animLegs = AnimationsLegs.WalkSouth;
                animArms = AnimationsArms.WalkSouth;

            }



            if (moveDirection != Vector2.Zero)
            {
                animationsBase.setLayerDepth(1 - (entity.position.Y / 100000));
                animationsArms.setLayerDepth(1 - (entity.position.Y / 100000));
                animationsLegs.setLayerDepth(1 - (entity.position.Y / 100000));

                if (!animationsBase.isAnimationPlaying(animBase))
                {
                    animationsBase.play(animBase);
                    animationsLegs.play(animLegs);
                    animationsArms.play(animArms);

                }

                CollisionResult res;
                mover.move(moveDirection * move_speed * Time.deltaTime, out res);
            }
            else
            {
                animationsBase.play(animBase); animationsBase.stop();
                animationsLegs.play(animLegs); animationsLegs.stop();
                animationsArms.play(animArms); animationsArms.stop();
            }

            if (Input.leftMouseButtonPressed)
            {
                //Clickable tiles around the player
                if (TileWithinRange())
                heldItem.OnLeftClick();
                else
                {
                    Debug.log("TILE IS TOO FAR AWAY - PlayerFarmerClass");
                }
            }

            if (Input.isKeyPressed(Keys.B))
            {
                if (Game1.HoedSpots.ContainsKey(Input.MousePositionInGridTile()))
                {
                    Game1.HoedSpots[Input.MousePositionInGridTile()].destroy();
                    Game1.HoedSpots.Remove(Input.MousePositionInGridTile());
                }

            }

        }

        private bool TileWithinRange()
        {
            Vector2 clickedPosition = Input.MousePositionInGridTile();
            Vector2 playerPosition = Helper.GetTilePosition(entity.position);
            List<Vector2> clickabletiles = new List<Vector2>();

            clickabletiles.Add(new Vector2(playerPosition.X - 1, playerPosition.Y - 1));
            clickabletiles.Add(new Vector2(playerPosition.X, playerPosition.Y - 1));
            clickabletiles.Add(new Vector2(playerPosition.X + 1, playerPosition.Y - 1));

            clickabletiles.Add(new Vector2(playerPosition.X - 1, playerPosition.Y));
            clickabletiles.Add(new Vector2(playerPosition.X, playerPosition.Y));
            clickabletiles.Add(new Vector2(playerPosition.X + 1, playerPosition.Y));

            clickabletiles.Add(new Vector2(playerPosition.X - 1, playerPosition.Y + 1));
            clickabletiles.Add(new Vector2(playerPosition.X, playerPosition.Y + 1));
            clickabletiles.Add(new Vector2(playerPosition.X + 1, playerPosition.Y + 1));

            

            foreach (var tiles in clickabletiles)
            {
                if (clickedPosition == tiles)
                {
                    return true;
                }
            }

            return false;
        }

        private void ChangingInventorySlots()
        {
            if (Input.isKeyPressed(Keys.D1)) inventoryBar.CurrentlySelected = 0;
            if (Input.isKeyPressed(Keys.D2)) inventoryBar.CurrentlySelected = 1;
            if (Input.isKeyPressed(Keys.D3)) inventoryBar.CurrentlySelected = 2;
            if (Input.isKeyPressed(Keys.D4)) inventoryBar.CurrentlySelected = 3;
            if (Input.isKeyPressed(Keys.D5)) inventoryBar.CurrentlySelected = 4;
            if (Input.isKeyPressed(Keys.D6)) inventoryBar.CurrentlySelected = 5;
            if (Input.isKeyPressed(Keys.D7)) inventoryBar.CurrentlySelected = 6;
            if (Input.isKeyPressed(Keys.D8)) inventoryBar.CurrentlySelected = 7;
            if (Input.isKeyPressed(Keys.D9)) inventoryBar.CurrentlySelected = 8;
            if (Input.isKeyPressed(Keys.D0)) inventoryBar.CurrentlySelected = 9;

            if (Input.mouseWheelDelta < 0) inventoryBar.CurrentlySelected--;
            if (Input.mouseWheelDelta > 0) inventoryBar.CurrentlySelected++;

        }

        public void onTriggerEnter(Collider other, Collider local)
        {
            if (other.entity.getComponent<MovementShake>() != null)
            {
                other.entity.getComponent<MovementShake>().Shake();
            }

        }

        public void onTriggerExit(Collider other, Collider local)
        {

        }
    }
}
