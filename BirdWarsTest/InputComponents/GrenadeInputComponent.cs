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
			grenadeTimer = 0.75f;
		}

		public override void HandleInput( GameObject gameObject, GameTime gameTime )
		{
			if( !gameObject.Health.IsDead() )
			{
				gameObject.Position += GetVelocity() * ( float )gameTime.ElapsedGameTime.TotalSeconds;
				UpdateTimer( gameTime );
				Explode( gameObject );
			}
		}

		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState ) {}

		private void Explode( GameObject gameObject )
		{
			if( ( int )grenadeTimer < 0 )
			{
				gameObject.Attack.DoAttack();
				gameObject.Health.TakeFullDamage();
			}
		}

		private void UpdateTimer( GameTime gameTime )
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