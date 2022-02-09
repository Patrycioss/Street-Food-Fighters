namespace GXPEngine
{
    public class Barrier : Sprite
    {
        private EasyDraw canvas;

        /// <summary>
        /// Object that functions as an invisible barrier for entities
        /// </summary>
        public Barrier() : base("barrier.png", addCollider: true)
        {
            canvas = new EasyDraw(width, height);
            AddChild(canvas);
        }

        void Update()
        {
            if (debugMode)
            {
                canvas.Clear(255,0,0, 50);
            }
            else canvas.ClearTransparent();
        }
    }
}