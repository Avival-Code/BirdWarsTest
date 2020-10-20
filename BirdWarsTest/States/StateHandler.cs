using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.States
{
	class StateHandler
	{
		public StateHandler()
		{
			currentState = StateTypes.MainMenuState;
			gameStates = new GameState[ maxStates ];
			gameStates[ 0 ] = new OpeningAnimationState();
			gameStates[ 1 ] = new MainMenuState();
			gameStates[ 2 ] = new WaitingRoomState();
			gameStates[ 3 ] = new PlayState();
			gameStates[ 4 ] = new OptionsState();
			gameStates[ 5 ] = new UserRegistryState();
			gameStates[ 6 ] = new StatisticsState();
		}

		public void InitializeStates( Microsoft.Xna.Framework.Content.ContentManager content )
		{
			foreach( GameState state in gameStates )
				state.Init( content );
		}

		public void ChangeState( StateTypes state )
		{
			currentState = state;
		}

		public ref GameState GetCurrentState()
		{
			return ref gameStates[ ( int )currentState ];
		}

		private const int maxStates = 7;
		private StateTypes currentState;
		private GameState[] gameStates;
	}
}