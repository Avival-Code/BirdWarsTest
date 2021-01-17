/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Input component that handles grenade behavior like movement,
speed, and update.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.InputComponents
{
	/// <summary>
	/// Input component that handles grenade behavior like movement,
	/// speed, and update.
	/// </summary>
	public class GrenadeInputComponent : InputComponent
	{
		/// <summary>
		/// Creates an instance of the input component with the
		/// specified direction and speed.
		/// </summary>
		/// <param name="directionIn">Direction grenade was thrown.</param>
		/// <param name="playerSpeedIn">The speed at which it was thrown.</param>
		public GrenadeInputComponent( Vector2 directionIn, float playerSpeedIn )
		{
			Direction = directionIn;
			playerSpeed = playerSpeedIn;
			GrenadeSpeed = playerSpeed * 2.0f;
			grenadeTimer = 0.60f;
		}

		/// <summary>
		/// Handles the input recieved based on the current game object state
		/// and game time.
		/// </summary>
		/// <param name="gameObject">Current game object.</param>
		/// <param name="gameTime">Current game time.</param>
		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		/// <summary>
		/// Handles the input recieved based on the current game object state
		/// and keyboard state.
		/// </summary>
		/// <param name="gameObject">Current game object.</param>
		/// <param name="state">Current keyboard state.</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		/// <summary>
		/// Handles the input recieved based on the current game object state,
		/// current keyboard state and current game state.
		/// </summary>
		/// <param name="gameObject">The Game object</param>
		/// <param name="state">current keyboard state</param>
		/// <param name="gameState">current game state</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState ) {}

		public override void HandleInput( GameObject gameObject, GameTime gameTime, Rectangle cameraRenderBounds )
		{
			if( !gameObject.Health.IsDead() )
			{
				gameObject.Position += GetVelocity() * ( float )gameTime.ElapsedGameTime.TotalSeconds;
				UpdateTimer( gameTime );
				Explode( gameObject, cameraRenderBounds );

				if( gameObject.Attack.IsAttacking && gameObject.Attack.AttackTimer == 1 )
				{
					gameObject.Health.TakeFullDamage();
				}
			}
		}

		private void Explode( GameObject gameObject, Rectangle cameraRenderBounds )
		{
			if( grenadeTimer < 0.00 )
			{
				if( cameraRenderBounds.Intersects( gameObject.GetRectangle() ) )
				{
					gameObject.Audio.Play();
				}
				gameObject.Attack.DoAttack();
			}
		}

		private void UpdateTimer( GameTime gameTime )
		{
			grenadeTimer -= ( float )gameTime.ElapsedGameTime.TotalSeconds;
		}

		/// <summary>
		/// Returns the calculated object velocity.
		/// </summary>
		/// <returns>Return the calculated object velocity.</returns>
		public override Vector2 GetVelocity()
		{
			return Direction * GrenadeSpeed;
		}

		/// <summary>
		/// Return the object speed.
		/// </summary>
		/// <returns>Return the object speed.</returns>
		public override float GetObjectSpeed()
		{
			return playerSpeed;
		}

		///<value>The direction the grenade was thrown.</value>
		public Vector2 Direction { get; private set; }

		///<value>The grenade speed</value>
		public float GrenadeSpeed { get; private set; }
		private readonly float playerSpeed;
		private float grenadeTimer;
	}
}