namespace GXPEngine
{
    public class TempEnemy : Entity
    {
        public TempEnemy() : base("enemyHitbox.png", "square.png", 1, 1)
        {
            SetScaleXY(1,2);
        }
    }
}