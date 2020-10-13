using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BirdWarsTest.States
{
	class StateHandler
	{
		public StateHandler( Microsoft.Xna.Framework.Content.ContentManager content )
		{
			gameStates = new GameState[ 6 ];
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
