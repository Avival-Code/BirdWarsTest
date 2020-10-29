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
			gameStates[ 0 ] = new LoginState( loginWidth, loginHeight );
			gameStates[ 1 ] = new OpeningAnimationState( stateWidth, stateHeight );
			gameStates[ 2 ] = new MainMenuState( stateWidth, stateHeight );
			gameStates[ 3 ] = new WaitingRoomState( stateWidth, stateHeight );
			gameStates[ 4 ] = new PlayState( stateWidth, stateHeight );
			gameStates[ 5 ] = new OptionsState( stateWidth, stateHeight );
			gameStates[ 6 ] = new UserRegistryState( stateWidth, stateHeight );
			gameStates[ 7 ] = new StatisticsState( stateWidth, stateHeight );
		}

		public void InitializeStates( Microsoft.Xna.Framework.Content.ContentManager content, 
									  ref GraphicsDeviceManager graphics )
		{
			gameStates[0].Enter( content, ref graphics );
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
		private const int loginWidth = 388;
		private const int loginHeight = 450;
		private const int stateWidth = 800;
		private const int stateHeight = 600;
	}
}