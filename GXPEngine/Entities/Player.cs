using System;
using GXPEngine.Core;
using GXPEngine.StageManagement;

namespace GXPEngine.Entities
{
    public class Player : Entity
    {
        public int chargedAmount { get; private set; }
        private int necessaryCharge;
        private bool canUseSpecial;
        
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
            
            SetCurrentCharacter(currentCharacter);

            // AddChild(specialAbility);
            
            health = 3.0f;
            speed = currentCharacter.speed;
            
            swapDelay = 2000;
            
            entityType = "player";
            invincibilityDuration = 1000;
            triggerDelay = 2000;

            canUseSpecial = true;
            necessaryCharge = 5;
            chargedAmount = 0;
        }

        protected override void Update()
        {
            if (Time.now - timeAtTrigger > triggerDelay)
            {
                TriggerEnemies();
                timeAtTrigger = Time.now;
            }

            
            if (chargedAmount >= necessaryCharge)
            {
                chargedAmount -= necessaryCharge;
                canUseSpecial = true;
            }

            if (Time.now - timeAtSwap > swapDelay && Input.GetKey(Key.S))
            {
                SwapCharacters();
                timeAtSwap = Time.now;            
            }
            
               
            if (Input.GetKey(Key.F) && abilityCanBeUsed)
            {
                Console.WriteLine(abilityCanBeUsed);
                UseMainAbility();
                abilityCanBeUsed = false;
            }

            else if (Input.GetKey(Key.G) && canUseSpecial && abilityCanBeUsed)
            {
                UseSpecialAbility();
                canUseSpecial = false;
                abilityCanBeUsed = false;
            }
            else velocity.Add(GetMovementInputs());


            
            base.Update();
            //System.Console.WriteLine("AnimationSprite x: {0}, y: {1} \n Feet x: {2}, y: {3}", model.x, model.y, canvas.x, canvas.y);
            
            
         
        }

        /// <summary>
        /// Adds a certain amount of charge to the player's special
        /// </summary>
        public void AddCharge(int amount)
        {
            chargedAmount += amount;
        }

        public override void Damage(float amount)
        {
            base.Damage(amount);
            myGame.hud.RemoveHearts((int)amount);
        }
     
        protected override void ChangeMirrorStatus()
        {
            if (Input.GetKey(Key.LEFT))
            {
                mirrored = true;
            }
            else if (Input.GetKey(Key.RIGHT))
            {
                mirrored = false;
            }
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
                    if (DistanceTo(enemy) <= enemy.attackRadius)
                    {
                        enemy.mainAbility.Use();
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

        /// <summary>
        /// Swaps between the two playable characters
        /// </summary>
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

        /// <summary>
        /// Sets the current character
        /// </summary>
        private void SetCurrentCharacter(Entity newCharacter)
        {
            currentCharacter = null;
            currentCharacter = newCharacter;
            
            if (model != null) model.Remove();
            
            unusedPixels = newCharacter.unusedPixels;
            
            SetBodyHitbox(newCharacter.bodyHitbox.name,newCharacter.bodyHitbox.x,newCharacter.bodyHitbox.y);
            SetModel(newCharacter.model.name, newCharacter.modelColumns, newCharacter.modelRows, currentCharacter.model.x, currentCharacter.model.y);
            SetMainAbility(newCharacter.mainAbility);
            SetSpecialAbility(newCharacter.specialAbility);


            walkAnimationDelay = newCharacter.walkAnimationDelay;
            idleAnimationDelay = newCharacter.idleAnimationDelay;
            specialAnimationDelay = newCharacter.specialAnimationDelay;
            basicAnimationDelay = newCharacter.basicAnimationDelay;

            walkingCycle = newCharacter.walkingCycle;
            idleCycle = newCharacter.idleCycle;
            mainCycle = newCharacter.mainCycle;
            specialCycle = newCharacter.specialCycle;

            currentState = newCharacter.currentState;
            unusedPixels = newCharacter.unusedPixels;

        }
    }
}