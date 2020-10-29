using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.States
{
	class StateHandler
	{
		public StateHandler( Microsoft.Xna.Framework.Content.ContentManager content,
							 ref GraphicsDeviceManager graphics )
		{
			currentState = StateTypes.LoginState;
			gameStates = new GameState[ maxStates ];
			gameStates[ 0 ] = new LoginState( content, ref graphics, loginWidth, loginHeight );
			gameStates[ 1 ] = new OpeningAnimationState( content, ref graphics, stateWidth, stateHeight );
			gameStates[ 2 ] = new MainMenuState( content, ref graphics, stateWidth, stateHeight );
			gameStates[ 3 ] = new WaitingRoomState( content, ref graphics, stateWidth, stateHeight );
			gameStates[ 4 ] = new PlayState( content, ref graphics, stateWidth, stateHeight );
			gameStates[ 5 ] = new OptionsState( content, ref graphics, stateWidth, stateHeight );
			gameStates[ 6 ] = new UserRegistryState( content, ref graphics, stateWidth, stateHeight );
			gameStates[ 7 ] = new StatisticsState( content, ref graphics, stateWidth, stateHeight );
		}

		public void InitializeStates()
		{
			gameStates[ 0 ].Enter();
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