using System;
using System.Collections.Generic;
using GXPEngine;                             
using System.Drawing;
using System.Runtime.InteropServices;

public class MyGame : Game
{
	private Player player;
	private Temp temp;

	private TempEnemy[] tempEnemies;
	
	public MyGame() : base(1366, 768, false)
	{
		//Makes a player and a temporary checkers sprite to test collisions (uses the barry.png)

		//4 x 64 =  256 
		//768 - 256 = 512
		//768 - 320 = 448

		debugMode = false;
		
		Sprite background = new Sprite("background.png",addCollider:false);
		AddChild(background);
		
		
		temp = new Temp(width/2,height/2);
		AddChild(temp);
		player = new Player();
		AddChild(player);

		tempEnemies = new TempEnemy[10];
		
		for (int i = 0; i < tempEnemies.Length; i++)
		{
			tempEnemies[i] = new TempEnemy();
			tempEnemies[i].SetXY(Utils.Random(10,width-10),Utils.Random(10,height-10));
			AddChild(tempEnemies[i]);
		}
		
		

		player.FeetHitBoxIsVisible = false;
		
	}
	
	void Update()
	{

		if (Input.GetKey(Key.B))
		{
			player.FeetHitBoxIsVisible = true;
			foreach (GameObject gameObject in GetChildren())
			{
				gameObject.debugMode = !gameObject.debugMode;
			}
		}
		
		
		SortDisplayHierarchy();
	}
	
	private void SortDisplayHierarchy()
	{
		List<GameObject> children = game.GetChildren();

		for (int i = children.Count-1; i >= 0; i--)
		{
			for (int j = i - 1; j >= 0; j--)
			{
				if (!children[i].Equals(children[j]))
				{
					if (children[i].y < children[j].y)
					{
						game.SetChildIndex(children[j],i);
					}
				}
			}
		}
	}

	static void Main()							
	{
		new MyGame().Start();				
	}
}