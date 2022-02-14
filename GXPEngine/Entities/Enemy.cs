using System;
using GXPEngine.Core;

namespace GXPEngine.Entities
{
    public class Enemy : Entity
    {
        public int detectionRadius;
        protected int attackRadius;
    
        private GameObject target;              //setting to player in game (later change it to level)
        private Sprite vision;                  //show rotation from enemy to player
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
            
            
            vision = new Sprite("placeholders/colors.png", addCollider: false);

            // without this the vision is set from the upper left corner of the feet Hit box (better for making the enemy move to the player's y axis?)
            // vision.SetXY(x + width / 2, -model.height / 2 + canvas.height); //canvas.height :feet hit box, height: entire entity including model height + canvas height
            // vision.SetScaleXY(1, 0.1f);
            // AddChild(vision);

            //SetScaleXY(1, 2);        
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
            if (target != null)
            {
                if (DistanceTo(target) >= attackRadius)
                {
                    //Calculates the the difference between the target and the current position and then normalizes it to get the direction
                    Vector2 targetPosition = new Vector2(target.x, target.y);
                    direction = targetPosition - new Vector2(x, y);
                    direction.Normalize();
                    velocity = direction;
            
                    //Calculates the angle at which the enemy is tracking the player and visually updates it
                    float angle = Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI;
                    vision.rotation = angle;
                }
            
            }
            else Console.WriteLine("Assign a target to enemy: " + this);
        
            base.Update();
        }

    }
}