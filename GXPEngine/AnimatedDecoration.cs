namespace GXPEngine
{
    /// <summary>
    /// Class for animated decorations
    /// </summary>
    public class AnimatedDecoration : AnimationSprite
    {
        private int cycleDuration;
        private int cycleStart;
        
        public AnimatedDecoration(string path, int cols, int rows, byte delay, bool stopAfterOneCycle = false) : base(path, cols, rows, addCollider:false)
        {
            _animationDelay = delay;

            if (stopAfterOneCycle)
            {
                cycleDuration = (frameCount-2) * _animationDelay;
                cycleStart = Time.now;
            }
        }

        void Update()
        {
            if (cycleDuration != 0)
            {
                if (!(Time.now - cycleStart > cycleDuration))
                {
                    Animate(Time.deltaTime);
                }

            }
            else Animate(Time.deltaTime);
            
        }
    }
}