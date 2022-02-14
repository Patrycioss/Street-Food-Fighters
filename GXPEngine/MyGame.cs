using System;
using GXPEngine.Entities;
using GXPEngine.StageManagement;

namespace GXPEngine
{
	public class MyGame : Game
	{
		public Player player;
		private Hud hud;
	
		private MyGame() : base(1366, 768, false)
		{
			//4 x 64 =  256 
			//768 - 256 = 512
			//768 - 320 = 448


			
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
		}
	

		static void Main()							
		{
			new MyGame().Start();				
		}
	}
}