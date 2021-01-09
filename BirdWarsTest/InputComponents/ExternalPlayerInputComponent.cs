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
			lastActiveVelocity = new Vector2( 1.0f, 0.0f );
			lastUpdateTime = 1.0;
		}

		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState ) 
		{
			if( !gameObject.Health.IsDead() )
			{
				gameObject.Attack.UpdateAttackTimer();
				gameObject.Health.UpdateCoolDownTimer();
			}
		}

		public override Vector2 GetVelocity()
		{
			return velocity;
		}

		public override Vector2 GetLastActiveVelocity()
		{
			return lastActiveVelocity;
		}

		public override void SetVelocity( Vector2 newVelocity )
		{
			if( newVelocity != new Vector2( 0.0f, 0.0f ) )
			{
				lastActiveVelocity = newVelocity;
			}
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

		private Vector2 lastActiveVelocity;
		private Vector2 velocity;
		private double lastUpdateTime;
	}
}