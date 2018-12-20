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

            if (Helper.DoesTileHaveProperty((int)Input.MousePositionInGridTile().X, (int)Input.MousePositionInGridTile().Y, "Diggable", "Back") && !
                    Game1.HoedSpots.ContainsKey(Input.MousePositionInGridTile()))
            {

                Entity HoeDirt = Core.scene.createEntity("HoedDirt");
                HoeDirt.addComponent(new HoedDirt());
                Game1.HoedSpots.Add(Input.MousePositionInGridTile(), HoeDirt);
                Console.WriteLine(Input.MousePositionInGridTile());
            }
            else
            {
                Console.WriteLine("THIS GROUND IS ALREADY DUG");
            }

        }


    }
}
