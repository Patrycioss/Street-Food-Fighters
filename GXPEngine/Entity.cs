using System.IO;
using GXPEngine.Core;

namespace GXPEngine
{
    /// <summary>
    /// Class that covers all types of entities (players and enemies)
    /// </summary>
    public class Entity : AnimationSprite
    {
        protected Vector2 velocity;
        protected float speed;


        protected Entity(string path, int cols, int rows, int frames =-1) : base(path, cols, rows, frames)
        {
            
        }

        protected virtual void UpdateMovement()
        {
            MoveUntilCollision(velocity.x * Time.deltaTime * speed, velocity.y * Time.deltaTime * speed);
        }
    }
}