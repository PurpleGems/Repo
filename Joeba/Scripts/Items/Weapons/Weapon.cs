using Joeba.Scripts.Components;
using Nez;
using Nez.Sprites;


namespace Joeba.Scripts.Items.Weapons
{
    abstract class Weapon : Item
    {
        public Weapon(string name, Sprite sprite) : base(name, sprite) { }

        public override void OnLeftClick()
        {
            Debug.log("Left clicked with " + Name);
            Entity swipe = Core.scene.createEntity("Swipe", Core.scene.findEntity("Player").position);
            swipe.addComponent(new Swipe(Sprite));
        }

        public void update()
        {
        }
    }
}
