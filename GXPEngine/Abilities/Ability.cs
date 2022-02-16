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

        //List of entities that were already hit by the ability
        protected List<Entity> entitiesHit;
        
        //Stuff that determines whether and for how long the attack is happening
        protected int attackDuration;
        protected int timeAtAttack;
        protected bool attacking;
        
        //Sound
        protected Sound sound;
        protected float soundVolume;



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
            
            //Timer for the duration of the punch
            if (Time.now - timeAtAttack > attackDuration)
            {
                attacking = false;
                visible = false;
                entitiesHit.Clear();
            }
        }

        /// <summary>
        /// Attempt to use the ability
        /// </summary>
        public void Use()
        {
            if (usable)
            {
                if (!attacking)
                {
                    visible = false;
                    attacking = true;
                    timeAtAttack = Time.now;
                }
                
                Action();
                timeAtUse = Time.now;
                usable = false;

                if (sound != null)
                {
                    sound.Play(volume: soundVolume);
                }
            }
        }

        /// <summary>
        /// Action that can be overridden in subclasses to allow for one time happening actions
        /// </summary>
        protected virtual void Action() {}

        protected void SetSound(string path, float volume = 1.0f)
        {
            sound = new Sound(path);
            soundVolume = volume;
        }
    }
}




