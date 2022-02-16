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
		
		private Sprite background;

		private Sound music;
		
		private MyGame() : base(1366, 768, false, pVSync: true)
		{
			music = new Sound("sounds/music.wav", true, true);
			music.Play(volume: 0.5f);

			scrollX = width / 2;
			
			debugMode = false;
		
			background = new Sprite("background.png",addCollider:false);
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
				if (player.x + StageLoader.currentStage.x < scrollX)
				{
					StageLoader.currentStage.x = scrollX - player.x;
				}
				if (player.x + StageLoader.currentStage.x > width - scrollX)
				{
					StageLoader.currentStage.x = width - scrollX - player.x;
				}
				
				//If the player is to the right of the center of the screen it will move to the right with the player until it hits the end of the stage
				if (StageLoader.currentStage.x > 0)
				{
					StageLoader.currentStage.x = 0;
				}
				else if (StageLoader.currentStage.x < -StageLoader.currentStage.stageWidth + game.width)
				{
					StageLoader.currentStage.x = -StageLoader.currentStage.stageWidth + game.width;
				}

				background.x = StageLoader.currentStage.x;
			}
		}
	

		static void Main()							
		{
			new MyGame().Start();				
		}
	}
}