using System;
using System.Collections;
using System.Collections.Generic;
using Joeba.Scripts;
using Joeba.Scripts.Characters;
using Joeba.Scripts.Components;
using Joeba.Scripts.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Tiled;

namespace Joeba
{
    public class Game1 : Core
    {
        public enum Layers
        {
            BackLayer = 100,
            BuildingsLayer = 90,
            PlayerLayer = 85,
            FrontLayer = 80,
            AlwaysFrontLayer = 0
        }

        public const int WIDTH = 1280;
        public const int HEIGHT = 720;
        public static int TileSize { get; } = 16;
        private static Entity PlayerEntity;
        public static TiledMap CurrentMap;
        public static Dictionary<Vector2,Entity> HoedSpots = new Dictionary<Vector2,Entity>();


        public Game1() : base(WIDTH, HEIGHT, false, true, "Joel's Farming Game", "Content")
        {
            Scene.setDefaultDesignResolution(WIDTH, HEIGHT, Scene.SceneResolutionPolicy.ShowAllPixelPerfect);
            defaultSamplerState = SamplerState.PointClamp;

        }

        protected override void Initialize()
        {
            base.Initialize();

            


            //Texture2D catfish = Content.Load<Texture2D>("Particles/treeleaf");
            Texture2D mousegrdtxr = Content.Load<Texture2D>("mousegridtxr");
            var testScene = Scene.createWithDefaultRenderer(Color.Purple);
            testScene.camera.setZoom(1);
//            var defRenderer = testScene.addRenderer(new DeferredLightingRenderer(1000, LIGHT_LAYER, new int[] { RENDERABLES_LAYER, -100, 100 }));
            

            //Tilemap!
            CurrentMap = content.Load<TiledMap>("Maps/untitled");


            Entity tilemapEnt = testScene.createEntity("tilemap_Town");
            var tilemaplayerCollision = tilemapEnt.addComponent(new TiledMapComponent(CurrentMap, "Buildings"));
          

            //Layer rendering
            var BackLayer = tilemapEnt.addComponent(new TiledMapComponent(CurrentMap));
            BackLayer.setLayerToRender("Back");
            BackLayer.setRenderLayer((int)Layers.BackLayer);

            tilemaplayerCollision.setLayerToRender("Buildings");
            tilemaplayerCollision.setRenderLayer((int)Layers.BuildingsLayer);

            var FrontLayer = tilemapEnt.addComponent(new TiledMapComponent(CurrentMap));
            FrontLayer.setLayerToRender("Front");
            FrontLayer.setRenderLayer((int)Layers.FrontLayer);


            var AlwaysFrontLayer = tilemapEnt.addComponent(new TiledMapComponent(CurrentMap));
            AlwaysFrontLayer.setLayerToRender("AlwaysFront");
            AlwaysFrontLayer.setRenderLayer((int)Layers.AlwaysFrontLayer);



            //PARTICLE STUFF NULL RN
//            Entity particleEmitter = testScene.createEntity("ParticleEmitter");
//            particleEmitter.addComponent(new ParticleEmitter(new Vector2(250, 250), catfish, new Vector2(Nez.Random.nextFloat() * (0.60f - -0.60f) + -0.60f, -6), new Vector2(0, Nez.Random.nextFloat() * (0.20f - 0.10f) + 0.10f)));

            Entity mouseFollowEntity = testScene.createEntity("MouseCursor");
            mouseFollowEntity.addComponent(new MouseFollow());
            var sprC = mouseFollowEntity.addComponent(new Sprite(mousegrdtxr).setLayerDepth(0));
            sprC.addComponent(new MouseGridSnap());




            PlayerEntity = testScene.createEntity("Player", new Vector2(15*16, 15*16));
            PlayerEntity.addComponent(new PlayerFarmer());
            
            
//            PlayerEntity.addComponent(new PointLight(new Color(0.8f, 0.8f, 0.9f))).setRadius(100).setIntensity(1f)
//            .setRenderLayer(LIGHT_LAYER);
            

            //HOED DIRT ENTITY
            Entity dirt = testScene.createEntity("HoedDirt");
            dirt.addComponent(new HoedDirt());
            dirt.setPosition(new Vector2(40 * 23, 38 * 13));

          


            Core.scene = testScene;
        }

    }
}
