﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joeba.Scripts.Components;
using Joeba.Scripts.Graphics;
using Joeba.Scripts.Items.Crops;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;

namespace Joeba.Scripts
{
    /// <summary>
    /// When added onto an entity it allows for a crop/seed to be planted in
    /// </summary>
    class HoedDirt : Component
    {
        private Vector2 dirtPosition;
        private Entity crop;
        private Sprite spr;
        private bool cropInitalized = false;
        private bool isWatered = false;

        public HoedDirt(Vector2 position)
        {
            dirtPosition = position;
        }

        public override void onAddedToEntity()
        {
            crop = Core.scene.createEntity("Crop");
            spr = entity.addComponent(new Sprite(GlobalSpritesheets.HoedDirtSplit[0]));
            spr.setRenderLayer(88);
            entity.addComponent(new GridSnap((int) dirtPosition.X,(int)dirtPosition.Y,false));
        }

        public void WaterPlant()
        {
            if (!isWatered)
            {
                isWatered = true;
                spr.setSubtexture(GlobalSpritesheets.HoedDirtSplit[4]);
            }
        }

        public bool CanPlantSeed()
        {
            return !cropInitalized;
        }

        public void PlantSeed(int xTile, int yTile,Crop crops)
        {
            cropInitalized = true;
            this.crop.addComponent(crops);
            crop.addComponent(new GridSnap(xTile, yTile,new Vector2(0,16)));
        }


    }
}
