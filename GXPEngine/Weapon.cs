
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Security;
using GXPEngine.Core;
using TiledMapParser;



namespace GXPEngine
{
    public class Weapon : AnimationSprite
    {
        protected float damage;

        //Use timer
        protected int coolDown;
        private int timeAtUse;
        private bool usable;
        
        //x-coordinates that are used to ensure the position being correct with mirroring
        protected Vector2 mirrorXcoords;


        protected List<Entity> entitiesHit;
        
        
        /// <summary>
        /// Every entity can have and use a weapon, a weapon has a hitbox and can have a model
        /// </summary>
        /// <param name="hitboxPath">FilePath for the hitbox image</param>
        protected Weapon(string path, int cols, int rows) : base(path, cols, rows)
        {
            collider.isTrigger = true;
            entitiesHit = new List<Entity>();
        }

        protected void Update()
        {
            if (Time.now - timeAtUse > coolDown) usable = true;
            x = mirrorX ? mirrorXcoords.x : mirrorXcoords.y;
        }

        /// <summary>
        /// Attempt to use the weapon
        /// </summary>
        public void Use()
        {
            if (timeAtUse == null) timeAtUse = Time.now;
            
            if (usable)
            {
                Action();
                timeAtUse = Time.now;
                usable = false;
            }
        }
    

        /// <summary>
        /// Executes the appropriate action for the weapon
        /// </summary>
        protected virtual void Action(){}
    }


    public class Punch : Weapon
    {
        //Stuff that determines whether and for how long the punching is happening
        protected int timeAtPunch;
        protected int punchDuration;
        protected bool punching;
        
        /// <summary>
        /// Base class for all types of punches made using an animationSprite
        /// </summary>
        protected Punch(string path, int cols, int rows) : base(path,cols,rows)
        {
            visible = false;
        }

        public void Update()
        {
            //Usage cooldown
            base.Update();

            //Timer for the duration of the punch
            if (Time.now - timeAtPunch > punchDuration)
            {
                punching = false;
                visible = false;
                entitiesHit.Clear();
            }
            
            //The punching itself
            if (punching)
            {
                foreach (Entity entity in StageLoader.GetEntities())
                {
                    Entity parent = (Entity) this.parent;
                    
                    if (entity.tag != parent.tag)
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
            if (!punching)
            {
                visible = true;
                punching = true;
                timeAtPunch = Time.now;
            }
            
        }
    }

    public class BurgerPunch : Punch
    {
        public BurgerPunch() : base("burgerPunch_hitbox.png", 1, 1)
        {
            mirrorXcoords = new Vector2(-width, width * 1.5f);
            damage = 1;
            y = -1.5f * height;
            alpha = 50;
            punchDuration = 300;
            coolDown = 1000;
        }
    }
    
}




