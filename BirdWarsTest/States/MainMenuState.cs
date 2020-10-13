using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.States
{
	class MainMenuState : GameState
	{
		public MainMenuState()
		{
			pos = new Vector2( 0, 0 ); 
		}

		public override void Init( Microsoft.Xna.Framework.Content.ContentManager newContent)
		{
			content = newContent;
			ball = content.Load<Texture2D>( "ball" );
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
			KeyboardState state = Keyboard.GetState();

			if( state.IsKeyDown( Keys.Right ) )
			{
				pos.X += 5;
			}
			if (state.IsKeyDown(Keys.Left ))
			{
				pos.X -= 5;
			}
			if (state.IsKeyDown(Keys.Up ))
			{
				pos.Y -= 5;
			}
			if (state.IsKeyDown(Keys.Down ))
			{
				pos.Y += 5;
			}
		}

		public override void UpdateLogic()
		{
			HandleInput();
		}

		public override void Render( ref SpriteBatch batch )
		{
			batch.Draw( ball, pos, Color.White );
		}

		private Texture2D ball;
		private Vector2 pos;
	}
}
