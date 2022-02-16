using System;
using GXPEngine.Core;
using GXPEngine.Entities;
using GXPEngine.StageManagement;

namespace GXPEngine
{
	public class MyGame : Game
	{
		public Player player;
		public Hud hud;
		private int scrollX;

		public Vector2 xBoundaries;




		private MyGame() : base(1366, 768, false, pVSync: true)
		{
			//4 x 64 =  256 
			//768 - 256 = 512
			//768 - 320 = 448
			scrollX = width / 2;
			
			debugMode = false;
		
			Sprite background = new Sprite("background.png",addCollider:false);
			AddChild(background);

			StageLoader.LoadStage(Stages.Test);
			
			hud = new Hud();    
			AddChild(hud);
			

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

			if (Input.GetKeyUp(Key.C))
			{
				Console.WriteLine(player.chargedAmount);
			}
			
			Scroll();
		}
		
		void Scroll()
		{
			if (player != null && StageLoader.currentStage != null)
			{
			
				//If the player is to the left of the center of the screen it will move to the left with the player until it hits the start of the stage
				if (player.x + game.x < scrollX)
				{
					game.x = scrollX - player.x;
				}
				if (player.x + game.x > width - scrollX)
				{
					game.x = width - scrollX - player.x;
				}
				
				//If the player is to the right of the center of the screen it will move to the right with the player until it hits the end of the stage
				if (game.x > 0)
				{
					game.x = 0;
				}
				else if (player.x - game.x < width + game.width)
				{
					game.x = width - scrollX - player.x;
				}
				
			}
		}
	

		static void Main()							
		{
			new MyGame().Start();				
		}
	}
}