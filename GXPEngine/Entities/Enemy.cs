using System;
using GXPEngine.Core;

namespace GXPEngine.Entities
{
    public class Enemy : Entity
    {
        public int attackRadius { get; protected set; }
    
        private GameObject target;              //setting to player in game (later change it to level)
        private Vector2 direction;              //moving direction

        /// <summary>
        /// A temporary class for an enemy that tracks the player, will later most likely function as a base class for enemies.
        /// </summary>
        protected Enemy(string feetPath) : base(feetPath)
        {
            //Invincibility duration for all enemies (can be overriden individually)
            invincibilityDuration = 0;

            entityType = "enemy";
        
            collider.isTrigger = false;
            speed = 0.1f;
        }

        /// <summary>
        /// Sets the target of this enemy
        /// </summary>
        /// <param name="newTarget">Target GameObject that this enemy tracks</param>
        public void SetTarget(GameObject newTarget)
        {
            target = newTarget;
            Console.WriteLine(("Set target to: " + target));
        }
    
        protected override void Update()
        {
            ChangeMirrorStatus();
            FixMirroring();
            
            if (target != null)
            {
                if (DistanceTo(target) >= attackRadius)
                {
                    //Calculates the the difference between the target and the current position and then normalizes it to get the direction
                    Vector2 targetPosition = new Vector2(target.x, target.y);
                    direction = targetPosition - new Vector2(x, y);
                    direction.Normalize();
                    velocity = direction;
                } 
            }
            else Console.WriteLine("Assign a target to enemy: " + this);
        
            base.Update();
        }

        protected override void ChangeMirrorStatus()
        {
            mirrored = (target.x < x);
        }

        protected override void Kill()
        {
            myGame.player.AddCharge(1);
            base.Kill();
        }

    }
}