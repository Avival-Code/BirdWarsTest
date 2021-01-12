/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
stores all game states and handles their initialization,
and transitions.
*********************************************/
using BirdWarsTest.Network;
using BirdWarsTest.Utilities;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.States
{
	/// <summary>
	/// stores all game states and handles their initialization,
	///and transitions.
	/// </summary>
	public class StateHandler
	{
		/// <summary>
		/// Creates an isntance of a statehandler, an empty shell of all
		/// game states and sets network manager.
		/// </summary>
		/// <param name="content">Game content manager</param>
		/// <param name="gameWindow">Game window</param>
		/// <param name="graphics">Graphics device reference</param>
		/// <param name="networkManagerIn">Game network Manager</param>
		public StateHandler( Microsoft.Xna.Framework.Content.ContentManager content,
							 GameWindow gameWindow, ref GraphicsDeviceManager graphics, INetworkManager networkManagerIn )
		{
			currentState = StateTypes.LoginState;
			networkManager = networkManagerIn;
			StringManager = new StringManager();
			gameStates = new GameState[ maxStates ];
			gameStates[ 0 ] = new LoginState( content, gameWindow, ref graphics, ref networkManager, LoginWidth, LoginHeight );
			gameStates[ 1 ] = new UserRegistryState( content, gameWindow, ref graphics, ref networkManagerIn, RegisterWidth, RegisterHeight );
			gameStates[ 2 ] = new PasswordRecoveryState( content, gameWindow, ref graphics, ref networkManagerIn, PasswordWidth, PasswordHeight );
			gameStates[ 3 ] = new MainMenuState( content, ref graphics, ref networkManagerIn, StateWidth, StateHeight );
			gameStates[ 4 ] = new WaitingRoomState( content, gameWindow, ref graphics, ref networkManagerIn, StateWidth, StateHeight );
			gameStates[ 5 ] = new PlayState( content, ref graphics, ref networkManagerIn, StateWidth, StateHeight );
			gameStates[ 6 ] = new OptionsState( content, gameWindow, ref graphics, ref networkManagerIn, ConfigWidth, ConfigHeight );
			gameStates[ 7 ] = new StatisticsState( content, ref graphics, ref networkManagerIn, RegisterWidth, RegisterHeight );
		}

		/// <summary>
		/// Initializes the first state on application startup.
		/// </summary>
		public void InitializeInitialState()
		{
			gameStates[ ( int )currentState ].Enter( this, StringManager );
		}

		/// <summary>
		/// Sets LastState to currentState, currentState to the new state
		/// and calls enter method of new state.
		/// </summary>
		/// <param name="state">The target game state.</param>
		public void ChangeState( StateTypes state )
		{
			LastState = currentState;
			currentState = state;
			gameStates[ ( int )state ].Enter( this, StringManager );
		}

		/// <summary>
		/// Returns the current state.
		/// </summary>
		/// <returns>returns the current state.</returns>
		public ref GameState GetCurrentState()
		{
			return ref gameStates[ ( int )currentState ];
		}

		/// <summary>
		/// Get the specified game state
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		public ref GameState GetState( StateTypes state )
		{
			return ref gameStates[ ( int )state ];
		}

		///<value>The Game String Manager</value>
		public StringManager StringManager { get; private set; }
		private GameState[] gameStates;

		///<value>The Game network manager</value>
		public INetworkManager networkManager;

		///<value>The previous state accessed.</value>
		public StateTypes LastState { get; private set; }
		private StateTypes currentState;
		private const int maxStates = 8;
		private const int LoginWidth = 388;
		private const int LoginHeight = 450;
		private const int RegisterWidth = 428;
		private const int RegisterHeight = 500;
		private const int PasswordWidth = 450;
		private const int PasswordHeight = 450;
		private const int ConfigWidth = 450;
		private const int ConfigHeight = 400;
		private const int StateWidth = 800;
		private const int StateHeight = 600;
	}
}