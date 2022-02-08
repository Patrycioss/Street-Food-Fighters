using System;									
using GXPEngine;                             
using System.Drawing;							

public class MyGame : Game
{
	private Player player;
	private Temp temp;
	
	public MyGame() : base(1366, 768, false)
	{
		temp = new Temp(width/2,height/2);
		AddChild(temp);
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