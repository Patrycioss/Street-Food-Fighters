using System;
using GXPEngine.Core;

namespace GXPEngine
{
    public class Player : Entity
    {
        private int timeAtTrigger;
        private int triggerDelay;
        
        
        public Player() : base("player_feet_blue.png", "burger_woman_idle.png", 1,1)
        {
            triggerDelay = 2000;

            tag = "player";

            health = 3.0f;
            
            SetAnimationDelay(170);
            speed = 0.5f;
            
            SetBodyHitbox("pizza_body.png", 0,-model.height*0.8f);

            invincibilityDuration = 5000;
        }

        
        protected override void Update()
        {
            if (Time.now - timeAtTrigger > triggerDelay)
            {
                TriggerEnemies();
                timeAtTrigger = Time.now;
            }
            
            
            if (Input.GetKey(Key.F))
            {
                if (weapon != null)
                {
                    weapon.Use();
                }
            }

            
            velocity.Add(GetKeyInputs());
            base.Update();
            //System.Console.WriteLine("AnimationSprite x: {0}, y: {1} \n Feet x: {2}, y: {3}", model.x, model.y, canvas.x, canvas.y);
        }

        /// <returns>A directional vector with information from arrow keys pressed by the player.</returns>
        private Vector2 GetKeyInputs()
        {
            Vector2 vector2 = new Vector2(0, 0);
            
            if (Input.GetKey(Key.UP))
            {
                vector2.y -= 1;
            }
            else if (Input.GetKey(Key.DOWN))
            {
                vector2.y += 1;
            }

            if (Input.GetKey(Key.LEFT))
            {
                vector2.x -= 1;
            }
            else if (Input.GetKey(Key.RIGHT))
            {
                vector2.x += 1;
            }

            return vector2;

        }

        private void TriggerEnemies()
        {
            foreach (Enemy enemy in StageLoader.GetEnemies())
            {
                if (enemy.weapon != null)
                {
                    if (enemy.detectionRadius != null)
                    {
                        if (DistanceTo(enemy) <= enemy.detectionRadius)
                        {
                            enemy.weapon.Use();
                        }
                    }
                }
            }
        }

                
    }

    

}