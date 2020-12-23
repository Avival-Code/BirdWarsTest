using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.InputComponents
{
	class ExternalPlayerInputComponent : InputComponent
	{
		public ExternalPlayerInputComponent()
		{
			velocity = Vector2.Zero;
			lastUpdateTime = 1.0;
		}

		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState ) {}

		public override Vector2 GetVelocity()
		{
			return velocity;
		}

		public override void SetVelocity( Vector2 newVelocity )
		{
			velocity = newVelocity;
		}

		public override double GetLastUpdateTime()
		{
			return lastUpdateTime;
		}

		public override void SetLastUpdateTime(double newTime)
		{
			lastUpdateTime = newTime;
		}

		private Vector2 velocity;
		private double lastUpdateTime;
	}
}
