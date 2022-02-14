using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GXPEngine.Core;
using GXPEngine.Entities;

namespace GXPEngine.Abilities
{
    public class Ability : AnimationSprite
    {
        protected float damage;

        //Use timer
        protected int coolDown;
        private int timeAtUse;
        private bool usable;
        
        //x-coordinates that are used to ensure the position being correct with mirroring
        protected Vector2 xCoordinates;


        protected List<Entity> entitiesHit;


        /// <summary>
        /// Every entity can have and use an ability, an ability has a hitbox and can have a model
        /// </summary>
        /// <param name="path">FilePath for the hitbox image</param>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
        protected Ability(string path, int cols, int rows) : base(path, cols, rows)
        {
            collider.isTrigger = true;
            entitiesHit = new List<Entity>();
        }

        protected void Update()
        {
            if (Time.now - timeAtUse > coolDown) usable = true;
            x = mirrorX ? xCoordinates.x : xCoordinates.y;
        }

        /// <summary>
        /// Attempt to use the ability
        /// </summary>
        public void Use()
        {
            
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
}




