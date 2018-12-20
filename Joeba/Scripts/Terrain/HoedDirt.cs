using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joeba.Scripts.Terrain
{
    class HoedDirt : Component
    {

        Seed plantedSeed;

        public HoedDirt(Seed seed)
        {
            plantedSeed = seed;
        }

        public override void onAddedToEntity()
        {
            //add hoeddirt sprite, whenever hoeddirt.plant?
        }

        public void PlantSeed(int x, int y)
        {

        }

    }
}
