using BirdWarsTest.Network;
using BirdWarsTest.Utilities;
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
			StringManager = new StringManager();
			gameStates = new GameState[ maxStates ];
			gameStates[ 0 ] = new LoginState( content, gameWindow, ref graphics, ref networkManager, loginWidth, loginHeight );
			gameStates[ 1 ] = new UserRegistryState( content, gameWindow, ref graphics, ref networkManagerIn, registerWidth, registerHeight );
			gameStates[ 2 ] = new PasswordRecoveryState( content, gameWindow, ref graphics, ref networkManagerIn, passwordWidth, passwordHeight );
			gameStates[ 3 ] = new MainMenuState( content, ref graphics, ref networkManagerIn, stateWidth, stateHeight );
			gameStates[ 4 ] = new WaitingRoomState( content, gameWindow, ref graphics, ref networkManagerIn, stateWidth, stateHeight );
			gameStates[ 5 ] = new PlayState( content, ref graphics, ref networkManagerIn, stateWidth, stateHeight );
			gameStates[ 6 ] = new OptionsState( content, ref graphics, ref networkManagerIn, configWidth, configHeight );
			gameStates[ 7 ] = new StatisticsState( content, ref graphics, ref networkManagerIn, registerWidth, registerHeight );
		}

		public void InitializeStates()
		{
			gameStates[ ( int )currentState ].Enter( this, StringManager );
		}

		public void ChangeState( StateTypes state )
		{
			LastState = currentState;
			currentState = state;
			gameStates[ ( int )state ].Enter( this, StringManager );
		}

		public ref GameState GetCurrentState()
		{
			return ref gameStates[ ( int )currentState ];
		}

		public ref GameState GetState( StateTypes state )
		{
			return ref gameStates[ ( int )state ];
		}

		public StringManager StringManager { get; private set; }
		private GameState[] gameStates;
		public INetworkManager networkManager;
		public StateTypes LastState { get; private set; }
		private StateTypes currentState;
		private const int maxStates = 8;
		private const int loginWidth = 388;
		private const int loginHeight = 450;
		private const int registerWidth = 428;
		private const int registerHeight = 500;
		private const int passwordWidth = 450;
		private const int passwordHeight = 450;
		private const int configWidth = 450;
		private const int configHeight = 400;
		private const int stateWidth = 800;
		private const int stateHeight = 600;
	}
}