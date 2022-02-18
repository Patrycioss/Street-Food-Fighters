//This file hosts all specific character classes so it's easier to edit stats without having to look through bulks of code

using System;
using GXPEngine.Abilities;
using GXPEngine.Core;

namespace GXPEngine.Entities
{
   public class BurgerWoman : Entity
   {
      public BurgerWoman() : base("hitboxes/burger_woman_feet.png")
      {
         unusedPixels = 256;
         
         idleAnimationDelay = 100;
         walkAnimationDelay = 35;

         basicAnimationDelay = 60;
         specialAnimationDelay = 70;



         idleCycle = new Vector2(25, 5);
         walkingCycle = new Vector2(0, 25);
         mainCycle = new Vector2(30, 11);
         specialCycle = new Vector2(41, 13);

         
         
         SetModel("models/burger_woman_grid.png",9,6, -40,-110);
         SetBodyHitbox("hitboxes/burger_woman.png", -25,-model.height*0.8f);
         health = 3.0f;
         speed = 0.7f;

         SetMainAbility(new BurgerPunch());
         SetSpecialAbility(new BurgerExplosion());
      }
   }

   public class PastaMan : Entity
   {
      public PastaMan() : base("hitboxes/pasta_man.png")
      {
         SetModel("models/pasta_man_grid.png",9,5,-40,-110);
         SetBodyHitbox("hitboxes/pasta_man.png", -25, -model.height*0.8f);
         health = 3.0f;
         speed = 0.7f;

         SetMainAbility(new PastaWhip());
         SetSpecialAbility(new MeatballShooter());

         unusedPixels = 256;

         idleAnimationDelay = 100;
         walkAnimationDelay = 40;

         basicAnimationDelay = 100;
         specialAnimationDelay = 100;

         idleCycle = new Vector2(25, 5);
         walkingCycle = new Vector2(0, 25);
         mainCycle = new Vector2(31, 11);
         specialCycle = new Vector2(41, 4);
      }  
   }

   public class PizzaZombie : Enemy
   {
      public PizzaZombie() : base("hitboxes/pizza_zombie_feet.png")
      {
         SetModel("models/pizza_zombie.png",35,1,-40,-110, true);
         SetBodyHitbox("hitboxes/pizza_zombie.png", -20,-110);
            
         SetTarget(myGame.player);  
         
         SetMainAbility(new PizzaBite());
         
         health = 3.0f;
         speed = 0.2f;
            
         attackRadius = 100;
         
         idleAnimationDelay = 100;
         walkAnimationDelay = 70;

         basicAnimationDelay = 100;

         idleCycle = new Vector2(7,5);
         walkingCycle = new Vector2(12,24);
         mainCycle = new Vector2(1,6);
         
         model.SetScaleXY(2.0f);
         
         SetDeathAnimation("models/pizza_zombie_death.png", 6, 1, 50,2.0f);
      }
   }

   public class TomatoZombie : Enemy
   {
      public TomatoZombie() : base("hitboxes/tomato_zombie_feet.png")
      {
         SetModel("models/tomato_zombie.png",35,1,-40,-110, true);
         SetBodyHitbox("hitboxes/tomato_zombie.png",-40,-110);
         
         SetTarget(myGame.player);
         
         SetMainAbility(new SeedShooter());
         
         health = 2.0f;
         speed = 0.2f;

         attackRadius = 400;

         idleAnimationDelay = 100;
         walkAnimationDelay = 70;

         basicAnimationDelay = 100;

         idleCycle = new Vector2(7,5);
         walkingCycle = new Vector2(12,24);
         mainCycle = new Vector2(1,6);
         
         SetHitSound("sounds/tomato_damage.wav");
         
         model.SetScaleXY(2.0f);
         
         SetDeathAnimation("models/tomato_zombie_death.png",6,1,100, 2.0f);
      }
   }

  
   
   
}