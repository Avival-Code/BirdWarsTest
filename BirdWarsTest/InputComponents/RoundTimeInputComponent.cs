using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.InputComponents
{
	public class RoundTimeInputComponent : InputComponent
	{
		public RoundTimeInputComponent()
		{
			RemainingRoundTime = 300.0f;
		}

		public override void HandleInput( GameObject gameObject, GameTime gameTime ) 
		{
			RemainingRoundTime -= ( float )gameTime.ElapsedGameTime.TotalSeconds;
		}

		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState ) {}

		public override int GetRemainingMinutes()
		{
			return ( int )( RemainingRoundTime / 60 );
		}

		public override int GetRemainingSeconds()
		{
			return ( int )( RemainingRoundTime % 60 );
		}

		public float RemainingRoundTime { get; private set; }
	}
}