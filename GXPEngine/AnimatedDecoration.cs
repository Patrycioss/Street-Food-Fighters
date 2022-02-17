using System;

namespace GXPEngine
{
    /// <summary>
    /// Class for animated decorations
    /// </summary>
    public class AnimatedDecoration : AnimationSprite
    {
        private int cycleDuration;
        private int cycleStart;
        private bool paused;

        private bool destroyOnFinish;
        
        public AnimatedDecoration(string path, int cols, int rows, byte delay, bool stopAfterOneCycle = false, bool paused = false, bool destroyWhenFinished = false) : base(path, cols, rows, addCollider:false)
        {
            _animationDelay = delay;

            if (stopAfterOneCycle)
            {
                cycleDuration = (frameCount-2) * _animationDelay;
                cycleStart = Time.now;
            }

            destroyOnFinish = destroyWhenFinished;
            
            this.paused = paused;
        }

        void Update()
        {
            if (!paused)
            {
                if (cycleDuration != 0)
                {
                    if (!(Time.now - cycleStart > cycleDuration))
                    {
                        Animate(Time.deltaTime);
                    }
                    else if (destroyOnFinish)
                    {
                        LateDestroy();
                    }

                }
                else Animate(Time.deltaTime);
            }
        }

        public void UnPause()
        {
            if (paused)
            {
                paused = false;
                cycleStart = Time.now;
            }
        }
    }
}