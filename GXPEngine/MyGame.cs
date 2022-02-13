using System;
using System.Collections.Generic;
using GXPEngine;                             
using System.Drawing;
using System.Runtime.InteropServices;

public class MyGame : Game
{
	public Player player;
	// private TempEnemy[] tempEnemies;
	
	public MyGame() : base(1366, 768, false)
	{
		//Makes a player and a temporary checkers sprite to test collisions (uses the barry.png)

		//4 x 64 =  256 
		//768 - 256 = 512
		//768 - 320 = 448
		

		debugMode = false;
		
		Sprite background = new Sprite("background.png",addCollider:false);
		AddChild(background);
		

		StageLoader.LoadStage(Stages.Test);

		
		Barrier barrier = new Barrier();
		barrier.SetXY(width/2,height/2);
		AddChild(barrier);
		
		//
		// tempEnemies = new TempEnemy[2];
		//
		// for (int i = 0; i < tempEnemies.Length; i++)
		// {
		// 	tempEnemies[i] = new TempEnemy();
		// 	tempEnemies[i].SetXY(Utils.Random(10,width-10),Utils.Random(height*0.7f,height-10)); //temporarily spawn random enemies
		// 	tempEnemies[i].SetTarget(player);
		// 	tempEnemies[i].FeetHitBoxIsVisible = false;
		// 	AddChild(tempEnemies[i]);
		// }
		//
		//

		Console.WriteLine(StageLoader.GetEnemies().Count);
		


	}
	
	void Update()
	{

		if (Input.GetKeyUp(Key.B))
		{
			foreach (GameObject gameObject in StageLoader.GetChildren())
			{
				gameObject.debugMode = !gameObject.debugMode;
			}
		}

		// Console.WriteLine(player.x);

	}
	

	static void Main()							
	{
		new MyGame().Start();				
	}
}