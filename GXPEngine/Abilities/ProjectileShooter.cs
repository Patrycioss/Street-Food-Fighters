using GXPEngine.Core;
using GXPEngine.StageManagement;

namespace GXPEngine.Abilities
{
    public class ProjectileShooter : Ability
    {
        private float speed;
        
        protected ProjectileShooter() : base("hitboxes/meat_ball.png", 1, 1)
        {
            damage = 3;
            speed = 1;
            xCoordinates = new Vector2(0, width);
            y = -0.75f * height;
            coolDown = 500;
        }

        protected override void Action()
        {

            Projectile projectile = new Projectile(mirrorX?-speed:speed,damage, parent);

            Vector2 vector2 = TransformPoint(parent.parent.x + xCoordinates.x, parent.parent.y + y);
            projectile.SetXY(vector2.x,vector2.y);
            StageLoader.AddObject(projectile);
        }
    }
}