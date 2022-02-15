using System;
using GXPEngine.Core;
using GXPEngine.Entities;
using GXPEngine.StageManagement;

namespace GXPEngine.Abilities
{
    public class Seed : AnimationSprite
    {
        private float speed;
        private Vector2 direction;
        private float damage;
        private Entity actualParent;
        
        public Seed(Vector2 setDirection, float setSpeed, float setDamage, GameObject parent) : base("hitboxes/seed.png", 1, 1)
        {
            direction = setDirection;
            collider.isTrigger = false;
            speed = setSpeed;
            damage = setDamage;

            Console.WriteLine(speed);

            actualParent = (Entity) parent;
        }

        void Update()
        {
            Move(speed * Time.deltaTime * direction.x, speed * Time.deltaTime * direction.y);

            foreach (Entity entity in StageLoader.GetEntities())
            {
                if (entity.entityType != actualParent.entityType)
                {
                    if (HitTest(entity.bodyHitbox))
                    {
                        entity.Damage(damage);
                    }
                }
            }

            if (DistanceTo(myGame.player) > 1000)
            {
                Destroy();
            }
        }
    }
}