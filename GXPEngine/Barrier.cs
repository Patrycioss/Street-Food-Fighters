namespace GXPEngine
{
    public class Barrier : Sprite
    {
        private EasyDraw canvas;

        Barrier() : base("barrier.png", addCollider: true)
        {
            canvas = new EasyDraw(width, height);
        }

        void Update()
        {
            if (debugMode)
            {
                canvas.Clear(255,0,0);
            }
        }
        
        
    }
}