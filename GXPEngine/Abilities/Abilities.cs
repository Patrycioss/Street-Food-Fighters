using System;
using GXPEngine.Core;
using GXPEngine.StageManagement;

namespace GXPEngine.Abilities
{
    public class BurgerPunch : BasicMelee
    {
        public BurgerPunch() : base("hitboxes/burger_punch.png", 1, 1)
        {
            xCoordinates = new Vector2(-width, width * 1.5f);
            damage = 1;
            y = -1.5f * height;
            alpha = 50;
            attackDuration = 300;
            coolDown = 500;
        }
    }

    public class PastaWhip : BasicMelee
    {
        public PastaWhip() : base("hitboxes/pasta_whip.png", 1, 1)
        {
            xCoordinates = new Vector2(-width, 0.3f*width);
            damage = 1;
            y = -2.5f * height;
            alpha = 50;
            attackDuration = 300;
            coolDown = 1000;
        }
    }

    public class PizzaBite : BasicMelee
    {
        public PizzaBite() : base("hitboxes/pizza_bite.png", 1, 1)
        {
            xCoordinates = new Vector2(-width, width * 1.5f);
            damage = 1;
            y = -1.5f * height;
            alpha = 50;
            
            //random time, so that the enemies aren't attacking all at once (only set once)
            attackDuration = Utils.Random (300, 1000);   
            coolDown = Utils.Random(1000, 2500);       
        }
    }

    public class BurgerExplosion : BasicMelee
    {
        public BurgerExplosion() : base("hitboxes/burger_explosion.png", 1, 1)
        {
            xCoordinates = new Vector2(-width * 20, -width/2.25f);
            damage = 2;
            y =  -0.75f*height;
            alpha = 50;

            attackDuration = 500;
            coolDown = 500;
        }
    }
    
    public class SeedShooter : ProjectileShooter
    {
        public SeedShooter()
        {
         
        }
    }

    public class MeatballShooter : ProjectileShooter
    {
        public MeatballShooter()
        {
         
        }
    }
}