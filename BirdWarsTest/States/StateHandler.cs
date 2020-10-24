using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.States
{
	class StateHandler
	{
		public StateHandler()
		{
			currentState = StateTypes.LoginState;
			gameStates = new GameState[ maxStates ];
			gameStates[ 0 ] = new LoginState();
			gameStates[ 1 ] = new OpeningAnimationState();
			gameStates[ 2 ] = new MainMenuState();
			gameStates[ 3 ] = new WaitingRoomState();
			gameStates[ 4 ] = new PlayState();
			gameStates[ 5 ] = new OptionsState();
			gameStates[ 6 ] = new UserRegistryState();
			gameStates[ 7 ] = new StatisticsState();
		}

		public void InitializeStates( Microsoft.Xna.Framework.Content.ContentManager content, Vector2 screenSize )
		{
			foreach( GameState state in gameStates )
				state.Init( content, screenSize );
		}

		public void ChangeState( StateTypes state )
		{
			currentState = state;
		}

		public ref GameState GetCurrentState()
		{
			return ref gameStates[ ( int )currentState ];
		}

		private GameState[] gameStates;
		private StateTypes currentState;
		private const int maxStates = 8;
	}
}