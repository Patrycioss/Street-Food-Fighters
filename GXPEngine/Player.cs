using GXPEngine.Core;

namespace GXPEngine
{
    public class Player : Entity
    {
        public Player() : base("player_feet.png", "barry.png", 7,1)
        {
            SetAnimationDelay(170);
            speed = 0.5f;
            
            EasyDraw canvas = new EasyDraw(width,height, false);
            canvas.Fill(255,0,0);
            canvas.ShapeAlign(CenterMode.Min,CenterMode.Min);
            canvas.Rect(0,0,width,height);
            AddChildAt(canvas,0);
            
            
            SetScaleXY(3);
        }



        void Update()
        {
            model.Animate(Time.deltaTime);
            

            velocity.Add(GetInputs());

            if (velocity.Magnitude() == 0)
            {
                
            }
            
            UpdateMovement();


            UpdateState();
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