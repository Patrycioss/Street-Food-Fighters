
using GXPEngine.Core;

namespace GXPEngine
{
    /// <summary>
    /// Class that covers all types of entities (players and enemies)
    /// </summary>
    public class Entity : Sprite
    {
        public bool FeetHitBoxIsVisible = false;
        
        protected Vector2 velocity;
        protected float speed;
        protected State currentState;
        
        private AnimationSprite model;
        private EasyDraw canvas;


        /// <summary>
        /// All enemies and players are entities, all entities can move, are animated and have hitboxes.
        /// </summary>
        /// <param name="hitboxPath">Filepath of the image used for the hitbox of the feet of the entity</param>
        /// <param name="modelPath">Filepath of the image used for the entity's model</param>
        /// <param name="columns">The amount of columns the spritesheet has</param>
        /// <param name="rows">The amount of rows the spritesheet has</param>
        protected Entity(string hitboxPath, string modelPath, int columns, int rows) : base(hitboxPath)
        {
            model = new AnimationSprite(modelPath, columns, rows, addCollider:false);
            model.SetXY(x-10,y-model.height+height);
            AddChild(model);

            canvas = new EasyDraw(model.width, model.height, false);
            AddChildAt(canvas,0);
        }
        
        /// <summary>
        /// Every frame the entity's movement, animation and state are updated.
        /// When overriding this always call this base at the end by using base.Update();
        /// </summary>
        protected virtual void Update()
        {
            model.Animate(Time.deltaTime);
            UpdateState();

            if (velocity != new Vector2(0, 0))
            {
                UpdateMovement();
                
                model.Mirror((velocity.x<0),false);
                velocity.Set(0,0);
            }

            if (debugMode)
            {
                canvas.Fill(255,0,0);
                canvas.ShapeAlign(CenterMode.Min,CenterMode.Min);
                canvas.Rect(0,0,width*1/3,height*1/3);
            }
            else
            {
                canvas.ClearTransparent();
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
            MoveUntilCollision(velocity.x * Time.deltaTime * speed, velocity.y * Time.deltaTime * speed);
        }
        

        /// <summary>
        /// Enum that determines in what state the entity is
        /// </summary>
        protected enum State
        {
            Walk,
            Stand,
            Jump
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
        /// Updates the animationcycle based on the currentState
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
                case State.Jump:
                    model.SetCycle(4,1);
                    break;
            }
        }
    }
}