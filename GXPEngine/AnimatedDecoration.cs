namespace GXPEngine
{
    /// <summary>
    /// Class for animated decorations
    /// </summary>
    public class AnimatedDecoration : AnimationSprite
    {
        public AnimatedDecoration(string path, int cols, int rows, byte delay) : base(path, cols, rows, addCollider:false)
        {
            _animationDelay = delay;
        }

        void Update()
        {
            Animate(Time.deltaTime);
        }
    }
}