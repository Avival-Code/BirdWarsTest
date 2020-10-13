using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BirdWarsTest.States
{
	class MainMenuState : GameState
	{
		public MainMenuState()
		{
		}

		public override void Init( Microsoft.Xna.Framework.Content.ContentManager newContent)
		{
			content = newContent;
		}

		public override void Pause()
		{
		}

		public override void Resume()
		{
		}

		public override void Enter()
		{
		}

		public override void HandleInput()
		{
		}

		public override void UpdateLogic()
		{
			HandleInput();
		}

		public override void Render( ref SpriteBatch batch )
		{
		}
	}
}
