using Nez;

namespace Joeba.Scripts.Components
{
    class MouseFollow : Component, IUpdatable
    {
        public void update()
        {
            //entity.setPosition(Input.scaledMousePosition);
            entity.setPosition(entity.scene.camera.mouseToWorldPoint());
        }
    }
}
