using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joeba.Scripts.Items.Tools
{
    class Hoe : Item
    {
        public Hoe(string name, Sprite sprite) : base(name,sprite)
        {

        }


        public override void OnLeftClick()
        {
            string value = "";
            Vector2 clickedPosition = Input.MousePositionInGridTile();
            if (Helper.DoesTileHaveProperty((int)clickedPosition.X,(int)clickedPosition.Y, "Diggable", "Back") && !
                    Game1.HoedSpots.ContainsKey(clickedPosition))
            {

                Entity HoeDirt = Core.scene.createEntity("HoedDirt");
                HoeDirt.addComponent(new HoedDirt(new Vector2((int)clickedPosition.X,(int)clickedPosition.Y)));
                Game1.HoedSpots.Add(new Vector2((int)clickedPosition.X, (int)clickedPosition.Y), HoeDirt);
                Console.WriteLine(clickedPosition);
            }
            else
            {
                Console.WriteLine("THIS GROUND IS ALREADY DUG");
            }

        }


    }
}
