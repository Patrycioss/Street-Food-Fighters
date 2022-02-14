using System;
using GXPEngine.Core;
using GXPEngine.Entities;
using GXPEngine.StageManagement;

namespace GXPEngine.Abilities
{
    public class BasicMelee : Ability
    {
        //Stuff that determines whether and for how long the attack is happening
        protected int attackDuration;
        private int timeAtAttack;
        private bool attacking;
        
        /// <summary>
        /// Basic melee ability involving a hitbox in which other types of entities get damaged
        /// </summary>
        protected BasicMelee(string path, int cols, int rows) : base(path,cols,rows)
        {
            visible = false;
        }

        public void Update()
        {
            //Usage cooldown
            base.Update();


            //Timer for the duration of the punch
            if (Time.now - timeAtAttack > attackDuration)
            {
                attacking = false;
                visible = false;
                entitiesHit.Clear();
            }
            
            //The punching itself
            if (attacking)
            {
                foreach (Entity entity in StageLoader.GetEntities())
                {
                    Entity parentInfo = (Entity) this.parent;
                    
                    //Makes sure that enemies can't attack enemies and players can't attack players
                    if (entity.entityType != parentInfo.entityType)
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
        /// If the user is not punching this triggers a punch
        /// </summary>
        protected override void Action()
        {
            if (!attacking)
            {
                visible = true;
                attacking = true;
                timeAtAttack = Time.now;
            }
            
        }
    }
    
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
}   