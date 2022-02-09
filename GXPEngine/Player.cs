using GXPEngine.Core;

namespace GXPEngine
{
    public class Player : Entity
    {
        public Player() : base("player_feet.png", "barry.png", 7,1)
        {
            SetAnimationDelay(170);
            speed = 0.5f;
            SetScaleXY(3);
        }

        
        protected override void Update()
        {
            velocity.Add(GetKeyInputs());
            base.Update();
        }

        /// <returns>A directional vector with information from arrow keys pressed by the player.</returns>
        private Vector2 GetKeyInputs()
        {
            Vector2 vector2 = new Vector2(0, 0);
            
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