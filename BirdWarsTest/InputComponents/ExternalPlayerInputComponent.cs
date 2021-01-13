/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Input component that handles remote player input using
the player's known velocities.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;

namespace BirdWarsTest.InputComponents
{
	/// <summary>
	/// Input component that handles remote player input using
	/// the player's known velocities.
	/// </summary>
	public class ExternalPlayerInputComponent : InputComponent
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public ExternalPlayerInputComponent()
		{
			velocity = Vector2.Zero;
			lastActiveVelocity = new Vector2( 1.0f, 0.0f );
			lastUpdateTime = 1.0;
		}

		/// <summary>
		/// Handles the input recieved based on the current game object
		/// and game time.
		/// </summary>
		/// <param name="gameObject">Current game object.</param>
		/// <param name="gameTime">Current game time.</param>
		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		/// <summary>
		/// Handles the input recieved based on the current game object
		/// and keyboard state.
		/// </summary>
		/// <param name="gameObject">Current game object.</param>
		/// <param name="state">Current keyboard state.</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		/// <summary>
		/// Handles remote player input based on current gameobject state, 
		/// keyboard state and game state.
		/// </summary>
		/// <param name="gameObject">Current game object</param>
		/// <param name="state">Current keyboard state</param>
		/// <param name="gameState">current game state</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState ) 
		{
			if( !gameObject.Health.IsDead() )
			{
				gameObject.Attack.UpdateAttackTimer();
				gameObject.Health.UpdateCoolDownTimer();
			}
		}

		/// <summary>
		/// Returns the current velocity
		/// </summary>
		/// <returns>Returns the current velocity</returns>
		public override Vector2 GetVelocity()
		{
			return velocity;
		}

		/// <summary>
		/// Returns tha last active velocity used to
		/// determine direction.
		/// </summary>
		/// <returns>Return the last active velocity.</returns>
		public override Vector2 GetLastActiveVelocity()
		{
			return lastActiveVelocity;
		}

		/// <summary>
		/// Sets the LastActive velocity to the last velocity state
		/// and the new velocity for the object.
		/// </summary>
		/// <param name="newVelocity">New gameObject velocity.</param>
		public override void SetVelocity( Vector2 newVelocity )
		{
			if( newVelocity != new Vector2( 0.0f, 0.0f ) )
			{
				lastActiveVelocity = newVelocity;
			}
			velocity = newVelocity;
		}

		/// <summary>
		/// Returns the last Update time.
		/// </summary>
		/// <returns>Returns the last update time.</returns>
		public override double GetLastUpdateTime()
		{
			return lastUpdateTime;
		}

		/// <summary>
		/// Sets the last update time.
		/// </summary>
		/// <param name="newTime">Sets the last update time.</param>
		public override void SetLastUpdateTime(double newTime)
		{
			lastUpdateTime = newTime;
		}

		private Vector2 lastActiveVelocity;
		private Vector2 velocity;
		private double lastUpdateTime;
	}
}