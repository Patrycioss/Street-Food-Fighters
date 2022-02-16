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
         
         SetModel("models/burger_woman_idle.png",1,1, -40,-110);
         SetBodyHitbox("hitboxes/burger_woman.png", -25,-model.height*0.8f);
         health = 3.0f;
         speed = 0.5f;

         SetMainAbility(new BurgerPunch());
         SetSpecialAbility(new BurgerExplosion());

         idleAnimationDelay = 200;
         walkAnimationDelay = 200;

         specialAnimationDelay = 200;
         basicAnimationDelay = 200;

         unusedPixels = 0;

      }
   }

   public class PastaMan : Entity
   {
      public PastaMan() : base("hitboxes/pasta_man.png")
      {
         SetModel("models/pasta_man.png",53,1,-40,-110, 256);
         SetBodyHitbox("hitboxes/pasta_man.png", -25, -model.height*0.8f);
         health = 3.0f;
         speed = 0.5f;

         SetMainAbility(new PastaWhip());
         SetSpecialAbility(new MeatballShooter());

         unusedPixels = 256;

         idleAnimationDelay = 100;
         walkAnimationDelay = 40;

         basicAnimationDelay = 100;
         specialAnimationDelay = 100;

         idleCycle = new Vector2(25, 5);
         walkingCycle = new Vector2(0, 25);
         mainCycle = new Vector2(30, 11);
         specialCycle = new Vector2(41, 12);
      }  
   }

   public class PizzaZombie : Enemy
   {
      public PizzaZombie() : base("hitboxes/pizza_zombie_feet.png")
      {
         SetModel("models/pizza_zombie.png",1,1,-40,-110);
         SetBodyHitbox("hitboxes/pizza_zombie.png", -20,-110);
            
         SetTarget(myGame.player);  
         
         SetMainAbility(new PizzaBite());
         
         health = 3.0f;
         speed = 0.2f;
            
         attackRadius = 100;
      }
   }

   public class TomatoZombie : Enemy
   {
      public TomatoZombie() : base("hitboxes/tomato_zombie_feet.png")
      {
         SetModel("models/tomato_zombie.png",1,1,-40,-110);
         SetBodyHitbox("hitboxes/tomato_zombie.png",-40,-110);
         
         SetTarget(myGame.player);
         
         SetMainAbility(new SeedShooter());
         
         health = 2.0f;
         speed = 0.2f;

         attackRadius = 500;
      }
   }

  
   
   
}