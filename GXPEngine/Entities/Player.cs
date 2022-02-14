using System;
using GXPEngine.Core;
using GXPEngine.StageManagement;

namespace GXPEngine.Entities
{
    public class Player : Entity
    {
        private int timeAtTrigger;
        private int triggerDelay;

        private BurgerWoman burgerWoman;
        private PastaMan pastaMan;
        private Entity currentCharacter;


        private int timeAtSwap;
        private int swapDelay;


        public Player() : base("hitboxes/burger_woman_feet.png")
        {
            burgerWoman = new BurgerWoman();
            pastaMan = new PastaMan();

            currentCharacter = burgerWoman;
            
            SetBodyHitbox(currentCharacter.bodyHitbox.name, currentCharacter.bodyHitbox.x,currentCharacter.bodyHitbox.y);
            SetModel(currentCharacter.model.name,1,1,(int)currentCharacter.model.x,(int)currentCharacter.model.y);

            SetMainAbility(currentCharacter.mainAbility); 
            SetSpecialAbility(currentCharacter.specialAbility);

            AddChild(mainAbility);
            // AddChild(specialAbility);
            
            health = 3.0f;
            speed = currentCharacter.speed;
            
            swapDelay = 2000;
            
            entityType = "player";
            invincibilityDuration = 5000;
            triggerDelay = 2000;
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
                UseMainAbility();
            }

            if (Input.GetKey(Key.G))
            {
                UseSpecialAbility();
            }

            if (Time.now - timeAtSwap > triggerDelay && Input.GetKey(Key.S))
            {
                SwapCharacters();
                timeAtSwap = Time.now;            
            }
            

            velocity.Add(GetMovementInputs());
            
            base.Update();
            //System.Console.WriteLine("AnimationSprite x: {0}, y: {1} \n Feet x: {2}, y: {3}", model.x, model.y, canvas.x, canvas.y);
        }

        /// <summary>
        /// Triggers enemies to attack when they are in range of the player
        /// </summary>
        private void TriggerEnemies()
        {
            foreach (Enemy enemy in StageLoader.GetEnemies())
            {
                if (enemy.mainAbility != null)
                {
                    if (enemy.detectionRadius != null)
                    {
                        if (DistanceTo(enemy) <= enemy.detectionRadius)
                        {
                            enemy.mainAbility.Use();
                        }
                    }
                }
            }
        }

        /// <returns>A directional vector with information from arrow keys pressed by the player.</returns>
        private Vector2 GetMovementInputs()
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

        private void SwapCharacters()
        {
            if (currentCharacter == burgerWoman)
            {
                SetCurrentCharacter(pastaMan);
            }
            else
            {
                SetCurrentCharacter(burgerWoman);
            }
        }

        private void SetCurrentCharacter(Entity newCharacter)
        {
            if (currentCharacter != newCharacter)
            {
                currentCharacter = newCharacter;
                model.Destroy();
                SetModel(newCharacter.model.name, newCharacter.modelColumns,newCharacter.modelRows, currentCharacter.model.x,currentCharacter.model.y);
                SetMainAbility(newCharacter.mainAbility);
                SetSpecialAbility(newCharacter.specialAbility);
                
            }
            else Console.WriteLine("The new character is the same as the old one!");
        }
    }
}