using System.Threading;
using GXPEngine.StageManagement;

namespace GXPEngine
{
    public class DeathAnimation : AnimationSprite
    {
        private int timeAtDeath;
        private int deathDuration;
        private bool paused;

        public DeathAnimation(string path, int cols, int rows, int frames, int duration, byte delay) : base(path, cols, rows, frames)
        {
            paused = true;
            timeAtDeath = Time.now;
            deathDuration = duration;

            _animationDelay = delay;
            
        }

        void Update()
        {
            if (!paused)
            {
                if (Time.now - timeAtDeath > deathDuration)
                {
                    LateDestroy();
                }
                else
                {
                    StageLoader.currentStage.AddChild(this);
                    Animate(Time.deltaTime);
                }
    
            } 
        }

        public void Unpause()
        {
            paused = false;
        }
        
    }
}