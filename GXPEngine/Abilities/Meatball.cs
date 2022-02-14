using System.Threading;
using GXPEngine.Core;
using GXPEngine.Entities;
using GXPEngine.StageManagement;

namespace GXPEngine.Abilities
{
    public class Meatball : AnimationSprite
    {
        private float speed;
        private float damage;
        private Entity actualParent;
        
        public Meatball(float setSpeed, float setDamage, GameObject parent) : base("hitboxes/meat_ball.png", 1, 1)
        {
            collider.isTrigger = true;
            speed = setSpeed;
            damage = setDamage;

            actualParent = (Entity) parent;
        }

        void Update()
        {
            Move(speed * Time.deltaTime,0);

            foreach (Entity entity in StageLoader.GetEntities())
            {
                if (DistanceTo(entity) < width * 1.1f)
                {
                  
                    if (entity.entityType != actualParent.entityType)
                    {
                        if (HitTest(entity))
                        {
                            entity.Damage(damage);
                        }
                    }
                }
            }

            if (DistanceTo(myGame.player) > 1000)
            {
                Destroy();
            }
            
            // if (collision != null)
            // {
            //     if (collision.other is Entity)
            //     {
            //         Entity parentInfo = (Entity) parent.parent;
            //         Entity collidedEntity = (Entity) collision.other;
            //         
            //         if (parentInfo.entityType != collidedEntity.entityType)
            //         {
            //             collidedEntity.Damage(damage);
            //         }
            //     }
            // }
        }
    }
}