using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.States
{
	class StateHandler
	{
		public StateHandler( Microsoft.Xna.Framework.Content.ContentManager content,
							 GameWindow gameWindow, ref GraphicsDeviceManager graphics )
		{
			currentState = StateTypes.LoginState;
			gameStates = new GameState[ maxStates ];
			gameStates[ 0 ] = new LoginState( content, gameWindow, ref graphics, loginWidth, loginHeight );
			gameStates[ 1 ] = new UserRegistryState( content, gameWindow, ref graphics, registerWidth, registerHeight );
			gameStates[ 2 ] = new OpeningAnimationState( content, ref graphics, stateWidth, stateHeight );
			gameStates[ 3 ] = new MainMenuState( content, ref graphics, stateWidth, stateHeight );
			gameStates[ 4 ] = new WaitingRoomState( content, ref graphics, stateWidth, stateHeight );
			gameStates[ 5 ] = new PlayState( content, ref graphics, stateWidth, stateHeight );
			gameStates[ 6 ] = new OptionsState( content, ref graphics, stateWidth, stateHeight );
			gameStates[ 7 ] = new StatisticsState( content, ref graphics, stateWidth, stateHeight );
		}

		public void InitializeStates()
		{
			gameStates[ 0 ].Enter( this );
		}

		public void ChangeState( StateTypes state )
		{
			currentState = state;
			gameStates[ ( int )state ].Enter( this );
		}

		public ref GameState GetCurrentState()
		{
			return ref gameStates[ ( int )currentState ];
		}

		private GameState[] gameStates;
		private StateTypes currentState;
		private GameWindow gameWindow;
		private const int maxStates = 8;
		private const int loginWidth = 388;
		private const int loginHeight = 450;
		private const int registerWidth = 428;
		private const int registerHeight = 500;
		private const int stateWidth = 800;
		private const int stateHeight = 600;
	}
}