using System;
using System.IO;
using GXPEngine.Core;

namespace GXPEngine
{
    /// <summary>
    /// Class that covers all types of entities (players and enemies)
    /// </summary>
    public class Entity : Sprite
    {
        protected Vector2 velocity;
        protected float speed;
        protected State currentState;
        protected AnimationSprite model;



        protected Entity(string hitboxPath, string modelPath, int columns, int rows) : base(hitboxPath)
        {
            model = new AnimationSprite(modelPath, columns, rows, addCollider:false);
            model.SetXY(x-10,y-model.height+height);
            AddChild(model);
            
            

        }

        protected void SetAnimationDelay(byte delay)
        {
            model.SetCycle(1,model.frameCount,delay);
        }

        protected virtual void UpdateMovement()
        {
            MoveUntilCollision(velocity.x * Time.deltaTime * speed, velocity.y * Time.deltaTime * speed);
        }

        protected void SetStateCycle(State state, int startFrame, int numFrames)
        {
        }
        
        protected void UpdateAnimation()
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

        protected void UpdateState()
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

        protected enum State : int
        {
            Walk,
            Stand,
            Jump
        }
    }
}