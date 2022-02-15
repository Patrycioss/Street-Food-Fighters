namespace GXPEngine
{
    public class Barrier : Sprite
    {
        /// <summary>
        /// Object that functions as an invisible barrier for entities
        /// </summary>
        public Barrier() : base("hitboxes/barrier.png", addCollider: true)
        {
            visible = false;
        }
 
        void Update()
        {
            visible = debugMode;
        }
    }
}