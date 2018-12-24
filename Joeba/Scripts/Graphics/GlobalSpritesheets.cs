using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Textures;
using System.Collections.Generic;


namespace Joeba.Scripts.Graphics
{
    class GlobalSpritesheets
    {
        //Stand alone Sheets
        private static Texture2D Weapons = Core.content.Load<Texture2D>("Items/weapons");
        private static Texture2D Crops = Core.content.Load<Texture2D>("Terrain/Crops/crops");
        private static Texture2D HoedDirt = Core.content.Load<Texture2D>("Terrain/Crops/hoeDirt");
        private static Texture2D BaseCharacter = Core.content.Load<Texture2D>("Characters/Player/BaseCharacter");

        //SplitUp Sheets
        public static List<Subtexture> WeaponsSplit = Subtexture.subtexturesFromAtlas(Weapons, 16, 16);
        public static List<Subtexture> CropsSplit = Subtexture.subtexturesFromAtlas(Crops, 16, 32);
        public static List<Subtexture> HoedDirtSplit = Subtexture.subtexturesFromAtlas(HoedDirt, 16, 16);
        public static List<Subtexture> BaseCharacterSplit = Subtexture.subtexturesFromAtlas(BaseCharacter, 16, 32);
        
    }
}
