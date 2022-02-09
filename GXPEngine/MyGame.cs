using System;									
using GXPEngine;                             
using System.Drawing;							

public class MyGame : Game
{
	private Player player;
	private Temp temp;
	
	public MyGame() : base(1366, 768, false)
	{
		//Makes a player and a temporary checkers sprite to test collisions (uses the barry.png)
		
		temp = new Temp(width/2,height/2);
		AddChild(temp);
		player = new Player();
		AddChild(player);

		player.FeetHitBoxIsVisible = false;
	}
	
	void Update()
	{
		if (Input.GetKey(Key.B))
		{
			player.FeetHitBoxIsVisible = !player.FeetHitBoxIsVisible;
		}
	}

	static void Main()							
	{
		new MyGame().Start();				
	}
}