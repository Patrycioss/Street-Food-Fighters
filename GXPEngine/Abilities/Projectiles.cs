using System;
using System.Collections.Generic;
using GXPEngine.Core;
using GXPEngine.Entities;
using GXPEngine.StageManagement;

namespace GXPEngine.Abilities
{
    public class Projectile : AnimationSprite
    {
        protected float speed;
        protected float damage;
        protected Entity actualParent;
        protected Vector2 direction;

        private List<Entity> entitiesHit;


        protected Projectile(Vector2 setDirection, float setSpeed, float setDamage, Entity parent, string path, int cols, int rows) : base(path,cols,rows)
        {
            direction = setDirection;
            speed = setSpeed;
            damage = setDamage;
            actualParent = parent;
            entitiesHit = new List<Entity>();
        }

        protected void Update()
        {
            Move(speed * Time.deltaTime * direction.x, speed * Time.deltaTime * direction.y);
            
            //Destroys the projectile if it gets too far away from the player to prevent unseen projectiles from lagging the game
            if (DistanceTo(myGame.player) > 1200)
            {
                Destroy();
            }
            
            DamageOtherEntities();
        }
        
        /// <summary>
        /// Checks if it hits an entity and damages it if it hits
        /// </summary>
        private void DamageOtherEntities()
        {
            foreach (Entity entity in StageLoader.GetEntities())
            {
                if (entity.entityType != actualParent.entityType)
                {
                    if (!entitiesHit.Contains(entity))
                    {
                        if (HitTest(entity.bodyHitbox))
                        {
                            entity.Damage(damage);
                            entitiesHit.Add(entity);
                        }
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// Seed for the seed shooter
    /// </summary>
    public class Seed : Projectile  
    {
        public Seed(Vector2 setDirection, float setSpeed, float setDamage, Entity parent) : base(setDirection, setSpeed,
            setDamage, parent,
            "models/seeds.png", 1, 1) {}
    }
    
    /// <summary>
    /// Meatball projectile for the meatball shooter
    /// </summary>
    public class Meatball : Projectile
    {
        private int duration;
        private int doTime;

        public Meatball(Vector2 setDirection, float setSpeed, float setDamage, Entity parent) : base(setDirection,
            setSpeed, setDamage, parent,
            "models/meatball.png", 1, 1)
        {
            duration = 300;
            visible = false;
            doTime = Time.now;

        }

        new void Update()
        {
            if (Time.now - doTime > duration)
            {
                visible = true;
                base.Update();
            } 
        }
    }
}