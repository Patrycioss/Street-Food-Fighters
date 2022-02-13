using System;
using GXPEngine;
using GXPEngine.Core;

public class Enemy : Entity
{
    public int detectionRadius;
    protected int attackRadius;
    
    private GameObject target;              //setting to player in game (later change it to level)
    private Sprite vision;                  //show rotation from enemy to player
    private Vector2 direction;              //moving direction
    
    /// <summary>
    /// A temporary class for an enemy that tracks the player, will later most likely function as a base class for enemies.
    /// </summary>
    protected Enemy(string feetPath, string modelPath, int cols, int rows) : base(feetPath, modelPath, cols,rows)
    {
        //Invincibility duration for all enemies (can be overriden individually)
        invincibilityDuration = 2000;

        tag = "enemy";
        
        collider.isTrigger = false;
        speed = 0.1f;
        vision = new Sprite("colors.png", addCollider: false);

        // without this the vision is set from the upper left corner of the feet Hit box (better for making the enemy move to the player's y axis?)
        vision.SetXY(x + width / 2, -model.height / 2 + canvas.height); //canvas.height :feet hit box, height: entire entity including model height + canvas height
        vision.SetScaleXY(1, 0.1f);
        AddChild(vision);

        //SetScaleXY(1, 2);        
    }

    /// <summary>
    /// Sets the target of this enemy
    /// </summary>
    /// <param name="_target">Target GameObject that this enemy tracks</param>
    public void SetTarget(GameObject _target)
    {
        target = _target;
        System.Console.WriteLine("Set target to: " + target);
    }
    
    protected override void Update()
    {
        if (target != null)
        {
            if (DistanceTo(target) >= attackRadius)
            {
                //Calculates the the difference between the target and the current position and then normalizes it to get the direction
                Vector2 targetPosition = new Vector2(target.x, target.y);
                direction = targetPosition - new Vector2(x, y);
                direction.Normalize();
                velocity = direction;
            
                //Calculates the angle at which the enemy is tracking the player and visually updates it
                float angle = Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI;
                vision.rotation = angle;
            }
            
        }
        else Console.WriteLine("Assign a target to enemy: " + this);
        
        base.Update();
    }

}

public class PizzaZombie : Enemy
{
    public PizzaZombie() : base("player_feet_blue.png", "pizza_zombie.png", 1, 1)
    {
        health = 2.0f;
        damage = 1.0f;
        
        SetWeapon(new BurgerPunch());
        
        SetTarget(myGame.player);
        speed = 0.2f;
        attackRadius = 100;
        detectionRadius = 100;
        
        SetBodyHitbox("pizza_body.png", -16,-96);
    }
}
