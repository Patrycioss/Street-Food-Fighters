namespace GXPEngine
{
    public class Barrier : Sprite
    {
        private EasyDraw canvas;

        /// <summary>
        /// Object that functions as an invisible barrier for entities
        /// </summary>
        public Barrier() : base("hitboxes/barrier.png", addCollider: true)
        {
            visible = false;
        }

        void Update()
        {
        }

        void Debug()
        {
            visible = !visible;
        }
    }
}