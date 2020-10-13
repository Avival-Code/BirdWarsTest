using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BirdWarsTest.States
{
	class StateHandler
	{
		public StateHandler( ref Microsoft.Xna.Framework.Content.ContentManager content )
		{
			gameStates = new GameState[ 7 ];
			gameStates[ 0 ] = new MainAnimationState( ref content );
			gameStates[ 1 ] = new MainMenuState( ref content );
			gameStates[ 2 ] = new WaitingRoomState( ref content );
			gameStates[ 3 ] = new PlayState( ref content );
			gameStates[ 4 ] = new OptionsState( ref content );
			gameStates[ 5 ] = new UserRegistryState( ref content );
			gameStates[ 6 ] = new StatisticsState(ref content);
		}

		public void ChangeState( StateTypes state )
		{
			currentState = state;
		}

		public ref GameState GetCurrentState()
		{
			return ref gameStates[ ( int )currentState ];
		}

		private StateTypes currentState = StateTypes.PlayState;
		private GameState[] gameStates;

	}
}
