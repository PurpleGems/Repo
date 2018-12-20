using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joeba.Scripts.Items
{
    abstract class Item
    {
        private string name;
        private Sprite sprite;

        public Item(string name, Sprite sprite)
        {
            Name = name;
            Sprite = sprite;
        }

        public virtual void OnLeftClick()
        {

        }

        public string Name { get { return name; } set { if (value.Length >= 3) name = value; else throw new Exception($"Name is too short {name}"); } }
        public Sprite Sprite { get { return sprite; } set { if (value != null) sprite = value; else throw new Exception("Sprite --" + Name + "-- Was Null"); } }

    }
}
