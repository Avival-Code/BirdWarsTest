using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.InputComponents
{
	class FindGameInputComponent : InputComponent
	{
		public override void HandleInput(GameObject gameObject, KeyboardState state) {}

		public override void HandleInput(GameObject gameObject, KeyboardState state, GameState gameState)
		{
			( ( MainMenuState )gameState ).NetworkManager.JoinRound();
		}
	}
}
