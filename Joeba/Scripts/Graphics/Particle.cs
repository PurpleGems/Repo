using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace Joeba.Scripts.Graphics
{
    class Particle : Component, IUpdatable
    {
        Texture2D texture;
        Vector2 position;
        Vector2 velocity;
        Vector2 gravity;
        Vector2 origin;
        Sprite sprite;
        public float Lifespan { get; set; }
        bool bounce;
        bool grounded;
        bool randomRotation;
        Vector2 originalPosition;
        float rotationSpeed = 0.05f;

        float originalLifespan;

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, Vector2 gravity, float rotationSpeed, float Lifespan, bool bounce, bool grounded, bool randomRotation)
        {
            this.Lifespan = Lifespan;
            this.texture = texture;
            this.position = position;
            this.velocity = velocity;
            this.gravity = gravity;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            originalLifespan = Lifespan;
            this.bounce = bounce;
            this.grounded = grounded;
            this.rotationSpeed = rotationSpeed;
            this.randomRotation = randomRotation;
        }

        public override void onAddedToEntity()
        {
            sprite = entity.addComponent(new Sprite(texture));
            originalPosition = entity.position;

            if (randomRotation)
                entity.rotation = Nez.Random.nextInt(360);

        }

        public void update()
        {
            entity.rotation += rotationSpeed;
            sprite.setColor(Color.White * (Lifespan / originalLifespan));
            Lifespan -= Time.deltaTime;
            velocity += gravity;
            entity.position += velocity;



            if (grounded)
            {
                if (entity.position.Y > originalPosition.Y + 10)
                {
                    velocity = Vector2.Zero;
                    gravity = Vector2.Zero;
                }
            }
            if (bounce)
            {
                if (entity.position.Y >= originalPosition.Y + 8)
                {
                    velocity.X *= 0.4f;
                    velocity.Y *= 0.7f;
                    velocity.Y *= -1;
                }
            }


        }
    }
}
