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
        int move_speed = 75; // Used to be 50
       
        

        public enum FacingDirection
        {
            North,
            South,
            West,
            East
        }

        public static FacingDirection Facing;

        enum Animations
        {
            WalkNorth,
            WalkSouth,
            WalkWest,
            WalkEast,
            Attack,
            Water,
            Hoe,
            Forage
        }

        //Components declaration for easy use in other parts of the class
        Item heldItem = new Hoe("Iridium Hoe", new Sprite(GlobalSpritesheets.WeaponsSplit[1]));
        Sprite<Animations> animations;
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
            //init animation stuff
            var texture = entity.scene.content.Load<Texture2D>("Characters/Player/BaseMale");  // This is the spritesheet of our 
            var textureSplit = Subtexture.subtexturesFromAtlas(texture, 16, 32); // This is the texture split up
            
            //size 10,8
            var col = entity.addComponent(new BoxCollider(10,8));
            col.setLocalOffset(new Vector2(-3, 0));
            var camera = entity.addComponent(new FollowCamera(entity, FollowCamera.CameraStyle.LockOn));
            camera.addComponent(new CameraBounds(new Vector2(0, 0), new Vector2(Game1.TileSize * (Game1.CurrentMap.width - 1), Game1.TileSize * (Game1.CurrentMap.height - 1))));

            Flags.setFlagExclusive(ref col.physicsLayer, 1);
            Flags.setFlagExclusive(ref col.collidesWithLayers, 0);

            //Initalize all this components other components
            //boxCollider = entity.addComponent(new BoxCollider());
            mover = entity.addComponent(new Mover());
            //we add onto our player the component of "Sprite" with animations and we set its first texture
            animations = entity.addComponent(new Sprite<Animations>(textureSplit[0])); // all the other animations collision are based off this also the starting sprite
            //heldItem = new WoodenSword();


            animations.addAnimation(Animations.WalkNorth, new SpriteAnimation(new List<Subtexture>()
            {
               textureSplit[16],
               textureSplit[17],
               textureSplit[18]
            }).setOrigin(new Vector2(8, 28)));

            animations.addAnimation(Animations.WalkSouth, new SpriteAnimation(new List<Subtexture>()
            {
               textureSplit[0],
               textureSplit[1],
               textureSplit[2]

            }).setOrigin(new Vector2(8, 28)));

            animations.addAnimation(Animations.WalkEast, new SpriteAnimation(new List<Subtexture>()
            {
               textureSplit[32],
               textureSplit[33],
               textureSplit[34],
               textureSplit[35],
               textureSplit[36],
               textureSplit[37]
            }).setOrigin(new Vector2(8, 28)));

            animations.addAnimation(Animations.WalkWest, new SpriteAnimation(new List<Subtexture>()
            {
               textureSplit[48],
               textureSplit[49],
               textureSplit[50],
               textureSplit[51],
               textureSplit[52],
               textureSplit[53]
            }).setOrigin(new Vector2(8,28))).setRenderLayer((int)Game1.Layers.PlayerLayer);


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



        Animations anim = Animations.WalkSouth;
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
           

            if (moveDirection.X < 0) { anim = Animations.WalkWest; Facing = FacingDirection.West; }
            else if (moveDirection.X > 0) { anim = Animations.WalkEast; Facing = FacingDirection.East; }

            if (moveDirection.Y < 0) { anim = Animations.WalkNorth; Facing = FacingDirection.North; }
            else if (moveDirection.Y > 0) { anim = Animations.WalkSouth; Facing = FacingDirection.South; }



            if (moveDirection != Vector2.Zero)
            {
                animations.setLayerDepth(1 - (entity.position.Y / 100000));

                if (!animations.isAnimationPlaying(anim))
                    animations.play(anim);

                CollisionResult res;
                mover.move(moveDirection * move_speed * Time.deltaTime, out res);
            }
            else { animations.play(anim); animations.stop(); }

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
