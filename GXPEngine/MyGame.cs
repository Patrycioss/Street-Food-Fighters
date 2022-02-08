using System;									
using GXPEngine;                             
using System.Drawing;							

public class MyGame : Game
{
	private Player player;
	
	
	public MyGame() : base(1920, 1080, false)
	{
		player = new Player();
		AddChild(player);
	}
	
	void Update()
	{
		
	}

	static void Main()							
	{
		new MyGame().Start();				
	}
}