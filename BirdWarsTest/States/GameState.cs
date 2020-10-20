using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.States
{
	abstract class GameState
	{
		public abstract void Init( Microsoft.Xna.Framework.Content.ContentManager newContent );

		public abstract void Pause();
		public abstract void Resume();
		public abstract void Enter();

		public abstract void HandleInput();
		public abstract void UpdateLogic( KeyboardState state );
		public abstract void Render( ref SpriteBatch sprites );

		public void ChangeState( StateHandler stateHandler, StateTypes state )
		{
			stateHandler.ChangeState( state );
		}

		protected Microsoft.Xna.Framework.Content.ContentManager content;
	}
}
