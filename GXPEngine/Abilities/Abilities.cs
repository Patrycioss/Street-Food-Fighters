using System;
using GXPEngine.Core;
using GXPEngine.Entities;
using GXPEngine.StageManagement;

namespace GXPEngine.Abilities
{
    public class BurgerPunch : BasicMelee
    {
        public BurgerPunch() : base("hitboxes/burger_punch.png", 1, 1)
        {
            xCoordinates = new Vector2(-width, width * 1.5f);
            damage = 2;
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
            xCoordinates = new Vector2(-width*0.5f, -width/2.25f);
            damage = 2;
            y =  -0.75f*height;
            alpha = 50;

            attackDuration = 500;
            coolDown = 500;
        }
    }

    public class SeedShooter : Ability
    {
        private float speed;
        public SeedShooter() : base("hitboxes/seed.png",1,1)
        {
            damage = 1;
            speed = 1;
            xCoordinates = new Vector2(0, width);
            y = -0.75f * height;
            coolDown = 500;
        }

        protected override void Action()
        {
            Vector2 playerPos = InverseTransformPoint(myGame.player.x, myGame.player.y);

            Vector2 direction = playerPos - new Vector2(parent.parent.x, parent.parent.y);
            
            direction.Normalize();
            Console.WriteLine(direction);

            Seed seed = new Seed(direction,speed,damage, (Entity) parent);
     
            Vector2 vector2 = TransformPoint(parent.parent.x + xCoordinates.x, parent.parent.y + y);
            seed.SetXY(vector2.x,vector2.y);
            StageLoader.AddObject(seed);
        }
        
        
    }   

    public class MeatballShooter : Ability
    {
        private float speed;
        public MeatballShooter() : base("hitboxes/meat_ball.png",1,1)
        {
            damage = 3;
            speed = 0.5f;
            xCoordinates = new Vector2(0, width);
            y = -0.75f * height;
            coolDown = 500;
         
        }
        
        protected override void Action()
        {
            Entity parent = (Entity) this.parent;
            Vector2 direction = new Vector2(parent.mirrored ? -1 : 1, 0);

            Meatball meatball = new Meatball(direction,speed,damage, parent);                      
                                                                                                 
            Vector2 vector2 = TransformPoint(parent.parent.x + xCoordinates.x, parent.parent.y + y);    
            meatball.SetXY(vector2.x,vector2.y);                                                        
            StageLoader.AddObject(meatball);                                                            
        }                                                                                               

    }
}