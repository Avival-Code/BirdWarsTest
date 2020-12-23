using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.InputComponents
{
	class RoundTimeInputComponent : InputComponent
	{
		public RoundTimeInputComponent()
		{

		}

		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState ) {}

		public float RemainingRoundTime { get; private set; }
	}
}