using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joeba.Scripts.Graphics
{
    class ParticleEmitter : Component, IUpdatable
    {
        Vector2 velocity;
        public Vector2 EmitterPosition;
        List<Entity> particles;
        Texture2D texture;
        Vector2 gravity;

        public ParticleEmitter(Vector2 position, Texture2D texture, Vector2 velocity, Vector2 gravity)
        {
            particles = new List<Entity>();
            EmitterPosition = position;
            this.texture = texture;
            this.velocity = velocity;
            this.gravity = gravity;
        }


        public void CreateLeafBurst(int nParticle)
        {
            float velocity = 0.45f;
            for (int i = 0; i < nParticle; i++)
            {
                var tempEnt = Core.scene.createEntity("Particle");
                tempEnt.addComponent(new Particle(texture, EmitterPosition, new Vector2(Nez.Random.nextFloat() * (velocity - -velocity) + -velocity, Nez.Random.nextFloat() * (velocity - -velocity) + -velocity), new Vector2(0, Nez.Random.nextFloat() * (0.005f - 0.002f) + 0.002f), 0.005f, 3f, false, false, true));
                particles.Add(tempEnt);
            }

        }

        public void CreateWoodHit(int nParticle)
        {
            for (int i = 0; i < nParticle; i++)
            {
                var tempEnt = Core.scene.createEntity("Particle");
                tempEnt.addComponent(new Particle(texture, EmitterPosition, new Vector2(Nez.Random.nextFloat() * (0.60f - -0.60f) + -0.60f, -4), new Vector2(0, Nez.Random.nextFloat() * (0.60f - 0.50f) + 0.50f), 0f, 2f, true, true, false));
                particles.Add(tempEnt);
            }

        }





        public void update()
        {
            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i].getComponent<Particle>().Lifespan <= 0)
                {
                    particles[i].destroy();
                    particles.RemoveAt(i);

                    i--;
                }
            }


            if (Input.leftMouseButtonPressed)
            {
                CreateWoodHit(10);
            }
        }
    }
}
