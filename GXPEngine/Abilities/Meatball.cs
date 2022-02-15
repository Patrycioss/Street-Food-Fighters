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
            collider.isTrigger = false;
            speed = setSpeed;
            damage = setDamage;

            actualParent = (Entity) parent;
        }

        void Update()
        {
            Move(speed * Time.deltaTime,0);

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