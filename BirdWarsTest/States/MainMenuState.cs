using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using BirdWarsTest.GameObjects;
using BirdWarsTest.InputComponents;
using BirdWarsTest.GraphicComponents;

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
			test = new GameObject( Identifiers.Player, 100.0f, 100.0f, new PlayerInputComponent(), 
						           new TestGraphicsComponent( content ) );
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

		public override void UpdateLogic( KeyboardState state )
		{
			HandleInput();
			test.Update( state );
		}

		public override void Render( ref SpriteBatch batch )
		{
			test.Render( ref batch );
		}

		private GameObject test;
	}
}
