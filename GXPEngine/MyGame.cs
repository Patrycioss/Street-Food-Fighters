using System;
using System.Collections.Generic;
using GXPEngine.Core;
using GXPEngine.Entities;
using GXPEngine.StageManagement;

namespace GXPEngine
{
	public class MyGame : Game
	{
		public Player player;
		public Hud hud;
		private GameOver gameOver;  
		private Menu menu;          
		private int scrollX;

		private bool isGameOver = false;
		private bool isMenu = true;

		// private Sprite background;

		private Sound music;
		
		private MyGame() : base(1366, 768, false, pVSync: true)
		{
			music = new Sound("sounds/music.wav", true, true);
			music.Play(volume: 0.5f);

			scrollX = (int) (width / 1.5f);
			
			debugMode = false;

			menu = new Menu();  //StartMenu
			AddChild(menu);
		}
	
		void Update()
		{
			if (!isGameOver && player != null && hud != null && !isMenu)    //Update for Level
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


				if (player.health <= 0)
				{
					Console.WriteLine("player health" + player.health);
					isGameOver = true;
					GameOver();
				}
			}
			else if (isGameOver && !isMenu) //for Game Over
			{
				RemoveGameOver();
			}
			else if (!isGameOver && isMenu)
			{
				RemoveMenu();
			}
			
			Scroll();

		}

		/// <summary>
		/// show game over screen until G is pressed
		/// </summary>
		private void GameOver()
		{
			//play Animation until last frame
			StageLoader.ClearCurrentStage();
			List<GameObject> children = GetChildren();
			foreach (GameObject child in children)
			{
				child.Destroy();
			}
			//Create game over screen with last from of Game Over Animation (Image), overlay the button controls :
			gameOver = new GameOver(this.hud);
			AddChild(gameOver);
		}

		/// <summary>
		/// Destroy Game Over Screen when moving to the Level
		/// </summary>
		private void RemoveGameOver()
		{
			if (gameOver != null && gameOver.destroyMe)
			{
				StageLoader.LoadStage(Stages.Test);

				hud = new Hud();
				AddChild(hud);
				isGameOver = false;
				gameOver.LateDestroy();
			}
		}
		/// <summary>
		/// Destroy Menu when moving the the Level, needs destroyMe from Menu to activate
		/// </summary>
		private void RemoveMenu()
		{
			if (menu != null && menu.destroyMe)
			{
				isMenu = false;
				menu.LateDestroy();
			}
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
			}
		}
	

		static void Main()							
		{
			new MyGame().Start();				
		}
	}
}