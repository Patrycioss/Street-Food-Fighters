using System;
using System.Runtime.InteropServices;
using GXPEngine.Abilities;
using GXPEngine.Core;
using GXPEngine.StageManagement;

namespace GXPEngine.Entities
{
    /// <summary>
    /// Class that covers all types of entities (players and enemies)
    /// </summary>
    public class Entity : Sprite
    {
        
        public State currentState;
        public string entityType { get; protected set;}
        
        protected Vector2 velocity;
        public int unusedPixels { get; protected set; }

        
        //Abilities
        public Ability mainAbility { get; private set; }
        public Ability specialAbility { get; private set; }

        protected bool abilityCanBeUsed;
        private bool usingAbility;

        private int abilityCooldown;
        private int abilityDuration;
        private int abilityUseTime;

        public byte specialAnimationDelay { get; protected set; }
        public byte basicAnimationDelay { get; protected set; }

        //STATS
        public float health { get; protected set; }
        public float speed { get; protected set; }

        //Invincibility duration after being hit
        private int damageTime;
        protected int invincibilityDuration;
        private bool damageable;

        //Model Information
        public AnimationSprite model { get; private set; }

        public int modelColumns { get; private set; }
        public int modelRows { get; private set; }

        public bool mirrored { get; protected set; }

        public byte walkAnimationDelay { get; protected set; }
        public byte idleAnimationDelay { get; protected set; }
        
        //x = startFrame, y = numFrames
        public Vector2 walkingCycle { get; protected set; }
        public Vector2 idleCycle { get; protected set; }
        public Vector2 mainCycle { get; protected set; }
        public Vector2 specialCycle { get; protected set; }
        

        private Vector2 xVectorModel;

        //Feet
        private Vector2 xVectorFeet;
        private EasyDraw canvas;

        //Body
        public Sprite bodyHitbox { get; private set; }
        
        //Sounds
        private Sound hitSound;
        private float hitSoundVolume;
        private Sound deathSound;
        private float deathSoundVolume;
        
        //Death
        private AnimatedDecoration deathAnimation;


        protected bool alreadyMirrored;


        /// <summary>
        /// All enemies and players are entities, all entities can move, are animated and have hitboxes.
        /// The sprite of this object functions as the hitbox for the feet of the entity, this hitbox is thus
        /// mostly used for walking.
        /// </summary>
        /// <param name="hitboxPath">Filepath of the image used for the hitbox of the feet of the entity</param>
        protected Entity(string hitboxPath) : base(hitboxPath)  //feet HitBox are the base for animationSprite, that means that the base needs to be in the correct size so that the animationSprite fits
        {
            damageable = true;

            //Canvas for debug purposes
            canvas = new EasyDraw(width, height, false);      
            canvas.Fill(255,0,0);
            canvas.visible = false;
            AddChild(canvas);

            usingAbility = false;
            abilityCanBeUsed = true;


            abilityDuration = 700;
            abilityCooldown = 1000;
        }
        
        public void UseMainAbility()
        {
            if (abilityCanBeUsed)
            {
                abilityCanBeUsed = false;

                currentState = State.MainAttack;
                usingAbility = true;

                if (mainAbility == null) throw new Exception(this + " doesn't have a main ability! Please set one using SetMainAbility()");
                
                mainAbility.Use();
                abilityUseTime = Time.now;
            }
        }

        protected void UseSpecialAbility()
        {
            if (abilityCanBeUsed)
            {
                abilityCanBeUsed = false;
                
                currentState = State.SpecialAttack;
                usingAbility = true;


                
                if (specialAbility == null) throw new Exception(this + " doesn't have a special ability! Please set one using SetMainAbility()");

                specialAbility.Use();
                abilityUseTime = Time.now;
            }
        }
        
        protected void SetMainAbility(Ability newAbility)
        {
            mainAbility = newAbility;
            AddChild(mainAbility);
        }
        protected void SetSpecialAbility(Ability newAbility)
        {
            specialAbility = newAbility;
            AddChild(specialAbility);
        }

        protected void SetDeathAnimation(string path, int cols, int rows, byte delay, float scale = 1.0f)
        {
            deathAnimation = new AnimatedDecoration(path, cols, rows, delay, true, true, true);
            deathAnimation.SetScaleXY(scale);
            deathAnimation.visible = false;

        }

        

        protected void SetModel(string modelPath, int columns, int rows, float x = 0, float y = 0, bool alreadyMirrored = false)
        {
            model = new AnimationSprite(modelPath, columns, rows, addCollider: false);

            model.SetXY(x,y);
            
            xVectorModel = new Vector2(model.x - unusedPixels, model.x);

            modelColumns = columns;
            modelRows = rows;
            
            
            AddChild(model);
            model.AddChild(canvas);

            xVectorFeet = InverseTransformPoint(xVectorModel.x, xVectorModel.y);
            Vector2 canvasPos = model.InverseTransformPoint(this.x, this.y);
            canvas.SetXY(canvasPos.x,canvasPos.y);

            this.alreadyMirrored = alreadyMirrored;
        }
        
        
        
        /// <summary>
        /// Sets the hitbox of the body
        /// </summary>
        /// <param name="path">The filepath for the image used</param>
        /// <param name="newX">The x coordinate of the location of the hitbox</param>
        /// <param name="newY">The y coordinate of the location of the hitbox</param>
        protected void SetBodyHitbox(string path, float newX, float newY)
        {
            bodyHitbox = new Sprite(path)
            {
                visible = false,
                alpha = 170,
                collider = {isTrigger = true},
                x = newX,
                y = newY,
                parent = this
            };
        }
        
        /// <summary>
        /// Damages the entity for a certain amount of damage. 
        /// </summary>
        public virtual void Damage(float amount)
        {
            if (damageable)
            {
                if (this is Player)
                {
                    Console.WriteLine("REMOVING HEART");
                    myGame.hud.RemoveHeart();
                }
                health -= amount;
                if (health <= 0) Kill();
                damageTime = Time.now;
                damageable = false;
                Console.WriteLine(model.name + ", " + "Health: " + health);

                if (hitSound != null)
                {
                    Console.WriteLine("ha");
                    hitSound.Play(volume: hitSoundVolume);
                }
            }
        }

        /// <summary>
        /// Adds a given amount of health to the health pool of the entity.
        /// </summary>
        public void AddHealth(float amount)
        {
            health += amount;
        }

        /// <summary>
        /// Sets the sound that is played when damaged
        /// </summary>
        protected void SetHitSound(string path, float volume = 1f)
        {
            hitSound = new Sound(path);
            hitSoundVolume = volume;
        }
        
        /// <summary>
        /// Sets the sound that is played when killed
        /// </summary>
        protected void SetDeathSound(string path, float volume = 1f)
        {
            deathSound = new Sound(path);
            deathSoundVolume = volume;
        }
        
        
        /// <summary>
        /// Kills the entity
        /// </summary>
        protected virtual void Kill()
        {
            if (deathAnimation != null)
            {
                deathAnimation.SetXY(x - 32,y - 110);
                deathAnimation.visible = true;
                deathAnimation.UnPause();
                deathAnimation.Mirror(mirrored,false);
                StageLoader.AddObject(deathAnimation);
            }
            LateDestroy();
        }

        /// <summary>
        /// Every frame the entity's movement, animation and state are updated.
        /// When overriding this always call this base at the end by using base.Update();
        /// </summary>
        protected virtual void Update()
        {
            if (usingAbility)
            {
                if (Time.now - abilityUseTime > abilityDuration) usingAbility = false;
            }

            if (!abilityCanBeUsed)
            {
                if (Time.now - abilityUseTime > abilityCooldown)
                {
                    Console.WriteLine("ha");
                    abilityCanBeUsed = true;
                } 
            }
            
            
            if (debugMode)
            {
                bodyHitbox.visible = true;
                canvas.visible = true;
            }
            else
            {
                bodyHitbox.visible = false;
                canvas.visible = false;
            }
            
            if (model == null) throw new Exception(this.name + " is lacking a model! Assign one using SetModel()");
            
            
            
            model.Animate(Time.deltaTime);
            UpdateState();

            //Updates movement and fixes mirror
            if (velocity != new Vector2(0, 0)) UpdateMovement();
            
            
            //Debugging
            if (debugMode)
            {
                canvas.Fill(255,0,0);
                canvas.ShapeAlign(CenterMode.Min,CenterMode.Min);
                canvas.Rect(0,0,width,height);
            }
            else canvas.ClearTransparent();
            

            //Invincibility frames
            if (Time.now - damageTime > invincibilityDuration) damageable = true;

            model.alpha = damageable ? 1 : Utils.Random(60, 100);
            
            UpdateAnimation();
            
        }
        

        /// <summary>
        /// Updates the entities movement based on its speed and Time.deltaTime
        /// </summary>
        protected virtual void UpdateMovement()
        {
            //Separate MoveUntilCollision into x and y so that the player doesn't get stuck in a wall when moving diagonally (Game Programming Recording 2: 1:16:56)
            MoveUntilCollision(0, velocity.y * Time.deltaTime * speed);
            MoveUntilCollision(velocity.x * Time.deltaTime * speed, 0);
            
            //Fixes mirroring based on the velocity
            ChangeMirrorStatus();
            FixMirroring();
            
            //Reset the velocity
            velocity.Set(0,0);
        }

        /// <summary>
        /// Changes the status of the mirrored bool, can be overridden to ensure proper mirroring
        /// </summary>
        protected virtual void ChangeMirrorStatus()
        {
            if (alreadyMirrored)
            {
                mirrored = !(velocity.x < 0);
            }
            else mirrored = velocity.x < 0;



        }

        /// <summary>
        /// Fixes the mirroring
        /// </summary>
        protected void FixMirroring()
        {
            model.Mirror(mirrored,false);

            model.x = mirrored ? xVectorModel.x : xVectorModel.y;
            canvas.x = mirrored ? xVectorFeet.x : xVectorFeet.y;
            
            canvas.Mirror(mirrored,false);
            
            Mirror(mirrored,false);

            if (mainAbility != null) mainAbility.Mirror(mirrored,false);

            if (specialAbility != null) specialAbility.Mirror(mirrored,false);

        }

        /// <summary>
        /// Enum that determines in what state the entity is
        /// </summary>
        public enum State
        {
            Walk,
            Stand,
            MainAttack,
            SpecialAttack
        }
        
        /// <summary>
        /// Updates the currentState
        /// </summary>
        private void UpdateState()
        {
            if (!usingAbility){currentState = velocity.Magnitude() == 0 ? State.Stand : State.Walk;}
        }
        
        /// <summary>
        /// Updates the AnimationCycle based on the currentState
        /// </summary>
        private void UpdateAnimation()
        {
            // if (idleCycle != null && walk)
            switch (currentState)
            {
                case State.Stand:
                    model.SetCycle((int)idleCycle.x,(int)idleCycle.y, idleAnimationDelay);
                    break;
                case State.Walk:
                    model.SetCycle((int)walkingCycle.x,(int)walkingCycle.y, walkAnimationDelay);
                    break;
                case State.MainAttack:
                    model.SetCycle((int)mainCycle.x,(int)mainCycle.y,basicAnimationDelay);
                    break;
                case State.SpecialAttack:
                    model.SetCycle((int)specialCycle.x,(int)specialCycle.y,specialAnimationDelay);
                    break;
            }
        }
    }
}