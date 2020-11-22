using BirdWarsTest.Network;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.States
{
	public class StateHandler
	{
		public StateHandler( Microsoft.Xna.Framework.Content.ContentManager content,
							 GameWindow gameWindow, ref GraphicsDeviceManager graphics, INetworkManager networkManagerIn )
		{
			currentState = StateTypes.LoginState;
			networkManager = networkManagerIn;
			gameStates = new GameState[ maxStates ];
			gameStates[ 0 ] = new LoginState( content, gameWindow, ref graphics, ref networkManager, loginWidth, loginHeight );
			gameStates[ 1 ] = new UserRegistryState( content, gameWindow, ref graphics, ref networkManagerIn, registerWidth, registerHeight );
			gameStates[ 2 ] = new PasswordRecoveryState( content, gameWindow, ref graphics, ref networkManagerIn, passwordWidth, passwordHeight );
			gameStates[ 3 ] = new OpeningAnimationState( content, ref graphics, ref networkManagerIn, stateWidth, stateHeight );
			gameStates[ 4 ] = new MainMenuState( content, ref graphics, ref networkManagerIn, stateWidth, stateHeight );
			gameStates[ 5 ] = new WaitingRoomState( content, gameWindow, ref graphics, ref networkManagerIn, stateWidth, stateHeight );
			gameStates[ 6 ] = new PlayState( content, ref graphics, ref networkManagerIn, stateWidth, stateHeight );
			gameStates[ 7 ] = new OptionsState( content, ref graphics, ref networkManagerIn, stateWidth, stateHeight );
			gameStates[ 8 ] = new StatisticsState( content, ref graphics, ref networkManagerIn, registerWidth, registerHeight );
		}

		public void InitializeStates()
		{
			gameStates[ ( int )currentState ].Enter( this );
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
		public INetworkManager networkManager;
		private StateTypes currentState;
		private const int maxStates = 9;
		private const int loginWidth = 388;
		private const int loginHeight = 450;
		private const int registerWidth = 428;
		private const int registerHeight = 500;
		private const int passwordWidth = 450;
		private const int passwordHeight = 450;
		private const int stateWidth = 800;
		private const int stateHeight = 600;
	}
}