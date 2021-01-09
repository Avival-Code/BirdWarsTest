using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.InputComponents
{
	public class RoundTimeInputComponent : InputComponent
	{
		public RoundTimeInputComponent( StateHandler handlerIn )
		{
			handler = handlerIn;
			RemainingRoundTime = 300.0f;
		}

		public override void HandleInput( GameObject gameObject, GameTime gameTime ) 
		{
			RemainingRoundTime -= ( float )gameTime.ElapsedGameTime.TotalSeconds;
			UpdateAllGameTimers();
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

		public void SetRemainingRoundTime( float remainingTime )
		{
			RemainingRoundTime = remainingTime;
		}

		private void UpdateAllGameTimers()
		{
			if( handler.networkManager.IsHost() )
			{
				if( ( int )RemainingRoundTime >= 30 && ( int )RemainingRoundTime % 30 == 0 )
				{
					handler.networkManager.SendUpdateRemainingTimeMessage( RemainingRoundTime );
				}
			}
		}

		public bool IsRoundTimeOver()
		{
			return ( int )RemainingRoundTime <= 0;
		}

		private StateHandler handler;
		public float RemainingRoundTime { get; private set; }
	}
}