using GXPEngine.Core;

namespace GXPEngine
{
    public class Player : Entity
    {
        public Player() : base("barry.png", 7, 1)
        {
            _animationDelay = 150;
            speed = 0.5f;

        }



        void Update()
        {
            Animate(Time.deltaTime);
            
            GetInputs();
            
            velocity.Add(GetInputs());
            UpdateMovement();
            velocity.Set(0,0);
            
            
        }

        private Vector2 GetInputs()
        {
            Vector2 vector2;
            vector2 = new Vector2(0, 0);
            
            if (Input.GetKey(Key.UP))
            {
                vector2.y -= 1;
            }
            else if (Input.GetKey(Key.DOWN))
            {
                vector2.y += 1;
            }

            if (Input.GetKey(Key.LEFT))
            {
                vector2.x -= 1;
            }
            else if (Input.GetKey(Key.RIGHT))
            {
                vector2.x += 1;
            }

            return vector2;

        }

                
    }

    

}