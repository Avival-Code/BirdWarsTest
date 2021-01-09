using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.InputComponents
{
	public class GrenadeInputComponent : InputComponent
	{
		public GrenadeInputComponent( Vector2 directionIn, float playerSpeedIn )
		{
			Direction = directionIn;
			playerSpeed = playerSpeedIn;
			GrenadeSpeed = playerSpeed * 2.0f;
			grenadeTimer = 0.55f;
		}

		public override void HandleInput( GameObject gameObject, GameTime gameTime )
		{
			if( !gameObject.Health.IsDead() )
			{
				gameObject.Position += GetVelocity() * ( float )gameTime.ElapsedGameTime.TotalSeconds;
				UpdateTimer( gameObject, gameTime );
				Explode( gameObject );

				if( gameObject.Attack.IsAttacking && gameObject.Attack.AttackTimer == 1 )
				{
					gameObject.Health.TakeFullDamage();
				}
			}
		}

		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState ) {}

		private void Explode( GameObject gameObject )
		{
			if( grenadeTimer < 0.00 )
			{
				gameObject.Attack.DoAttack();
			}
		}

		private void UpdateTimer( GameObject gameObject, GameTime gameTime )
		{
			grenadeTimer -= ( float )gameTime.ElapsedGameTime.TotalSeconds;
		}

		public override Vector2 GetVelocity()
		{
			return Direction * GrenadeSpeed;
		}

		public override float GetObjectSpeed()
		{
			return playerSpeed;
		}

		public Vector2 Direction { get; private set; }
		public float GrenadeSpeed { get; private set; }
		private float playerSpeed;
		private float grenadeTimer;
	}
}