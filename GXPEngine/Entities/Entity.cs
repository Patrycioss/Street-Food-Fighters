using System;
using System.Linq.Expressions;
using GXPEngine.Abilities;
using GXPEngine.Core;

namespace GXPEngine.Entities
{
    /// <summary>
    /// Class that covers all types of entities (players and enemies)
    /// </summary>
    public class Entity : Sprite
    {
        //Abilities
        public Ability mainAbility { get; protected set; }
        public Ability specialAbility { get; protected set; }

        //STATS
        public float health { get; protected set; }
        public float speed { get; protected set; }
        
        //Invincibility duration after being hit
        private int damageTime;
        protected int invincibilityDuration;
        private bool damageable;
        
        //Model Information
        public int modelColumns { get; private set; }
        public int modelRows { get; private set; }

        protected bool mirrored;
        
        protected Vector2 velocity;
        protected State currentState;
        
        public AnimationSprite model { get; private set; } 
        protected EasyDraw canvas;      

        public Sprite bodyHitbox;

        public string entityType { get; protected set;}
        


        /// <summary>
        /// All enemies and players are entities, all entities can move, are animated and have hitboxes.
        /// The sprite of this object functions as the hitbox for the feet of the entity, this hitbox is thus
        /// mostly used for walking.
        /// </summary>
        /// <param name="hitboxPath">Filepath of the image used for the hitbox of the feet of the entity</param>
        /// <param name="modelPath">Filepath of the image used for the entity's model</param>
        /// <param name="columns">The amount of columns the spritesheet has</param>
        /// <param name="rows">The amount of rows the spritesheet has</param>
        protected Entity(string hitboxPath) : base(hitboxPath)  //feet HitBox are the base for animationSprite, that means that the base needs to be in the correct size so that the animationSprite fits
        {
            damageable = true;

            //Canvas for debug purposes
            canvas = new EasyDraw(width, height, false);      
            canvas.Fill(255,0,0);
            canvas.visible = false;
        }
        
        public void UseMainAbility()
        {
            if (mainAbility == null)
            {
                throw new Exception(this + " doesn't have a main ability! Please set one using SetMainAbility()");
            }
            mainAbility.Use();
        }

        public void UseSpecialAbility()
        {
            if (specialAbility == null)
            {
                throw new Exception(this + " doesn't have a special ability! Please set one using SetMainAbility()");
            }

            specialAbility.Use();
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

        

        protected void SetModel(string modelPath, int columns, int rows, float x = 0, float y = 0)
        {
            model = new AnimationSprite(modelPath, columns, rows, addCollider: false);
            model.SetXY(x,y);

            modelColumns = columns;
            modelRows = rows;
            
            
            AddChild(model);
            model.AddChild(canvas);

            Vector2 canvasPos = model.InverseTransformPoint(this.x, this.y);
            canvas.SetXY(canvasPos.x,canvasPos.y);
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
        public void Damage(float amount)
        {
            if (damageable)
            {
                health -= amount;
            
                if (health <= 0)
                {
                    Kill();  
                }

                damageTime = Time.now;
                damageable = false;
                
                model.SetColor(0,255,0);
                Console.WriteLine(model.name + ", " + "Health: " + health);
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
        /// Kills the entity
        /// </summary>
        protected virtual void Kill()
        {
            this.LateDestroy();
        }

        /// <summary>
        /// Every frame the entity's movement, animation and state are updated.
        /// When overriding this always call this base at the end by using base.Update();
        /// </summary>
        protected virtual void Update()
        {
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
            
            if (model == null)
            {
                throw new Exception(this.name + " is lacking a model! Assign one using SetModel()");
            }
            
            
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
            else
            {
                canvas.ClearTransparent();
            }

            //Invincibility frames
            if (Time.now - damageTime > invincibilityDuration)
            {
                damageable = true;
                model.SetColor(255, 0, 0);
            }

            if (!damageable)
            {
                model.alpha = Utils.Random(60, 100);
            }
        }



        /// <summary>
        /// Sets the delay between animation frames, can be set individually for entities to ensure nice animations
        /// </summary>
        /// <param name="delay">The amount of delay between animation frames, can range from 0-255</param>
        protected void SetAnimationDelay(byte delay)
        {
            model.SetCycle(1,model.frameCount,delay);
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

        protected virtual void ChangeMirrorStatus()
        {
            mirrored = (velocity.x < 0);
        }

        protected void FixMirroring()
        {
            
            Mirror(mirrored,false);
            model.Mirror(mirrored,false);

            if (mainAbility != null)
            {
                mainAbility.Mirror(mirrored,false);
            }

            if (specialAbility != null)
            {
                specialAbility.Mirror(mirrored,false);
            }

        }

        /// <summary>
        /// Enum that determines in what state the entity is
        /// </summary>
        protected enum State
        {
            Walk,
            Stand
        }
        
        /// <summary>
        /// Updates the currentState
        /// </summary>
        private void UpdateState()
        {
            if (velocity.Magnitude() == 0)
            {
                currentState = State.Stand;
                UpdateAnimation();
            }
            else if (currentState != State.Walk)
            {
                currentState = State.Walk;
                UpdateAnimation();
            }
        }
        
        /// <summary>
        /// Updates the AnimationCycle based on the currentState
        /// </summary>
        private void UpdateAnimation()
        {
            switch (currentState)
            {
                case State.Stand:
                    model.SetCycle(5,3);
                    break;
                case State.Walk:
                    model.SetCycle(1,3);
                    break;
            }
        }
    }
}