/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
The base abstract input component class.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.InputComponents
{
	/// <summary>
	/// The base abstract input component class.
	/// </summary>
	public abstract class InputComponent
	{

		/// <summary>
		/// Handles the input recieved based on the current game object state
		/// and game time.
		/// </summary>
		/// <param name="gameObject">Current game object.</param>
		/// <param name="gameTime">Current game time.</param>
		abstract public void HandleInput( GameObject gameObject, GameTime gameTime );

		/// <summary>
		/// Handles the input recieved based on the current game object state
		/// and keyboard state.
		/// </summary>
		/// <param name="gameObject">Current game object.</param>
		/// <param name="state">Current keyboard state.</param>
		abstract public void HandleInput( GameObject gameObject, KeyboardState state );

		/// <summary>
		/// Handes the input recieved based on the current game object state,
		/// current keyboard state and current game state.
		/// </summary>
		/// <param name="gameObject">The Game object</param>
		/// <param name="state">current keyboard state</param>
		/// <param name="gameState">current game state</param>
		abstract public void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState );

		/// <summary>
		/// Method used by GrenadeInputComponent to set off a grenade. 
		/// </summary>
		/// <param name="gameObject">The GameObject.</param>
		/// <param name="state">The current keyboard state.</param>
		/// <param name="cameraRenderBounds">The current cameraRenderBounds.</param>
		/// <param name="gameTime">The current gameTime.</param>
		public virtual void HandleInput( GameObject gameObject, GameTime gameTime, Rectangle cameraRenderBounds ) {}
		/// <summary>
		/// Method used to return certain text if needed.
		/// </summary>
		/// <returns>Text</returns>
		public virtual string GetText() { return ""; }

		/// <summary>
		/// Used Text area input components to remove visual characters
		/// from text.
		/// </summary>
		/// <returns>A clean usable text string.</returns>
		public virtual string GetTextWithoutVisualCharacter() { return ""; }

		/// <summary>
		/// Returns the velocity
		/// </summary>
		/// <returns>Velocity</returns>
		public virtual Vector2 GetVelocity() { return Vector2.Zero; }

		/// <summary>
		/// REturns object speed
		/// </summary>
		/// <returns>oBject speed.</returns>
		public virtual float GetObjectSpeed() { return 0.0f; }

		/// <summary>
		/// Sets the object velocity.
		/// </summary>
		/// <param name="newVelocity">The new specified velocity.</param>
		public virtual void SetVelocity( Vector2 newVelocity ) {}

		/// <summary>
		/// Returns the last active velovity.
		/// </summary>
		/// <returns>The last active velocity.</returns>
		public virtual Vector2 GetLastActiveVelocity() { return Vector2.Zero; }

		/// <summary>
		/// Returns the last update time.
		/// </summary>
		/// <returns>REturns the last update time.</returns>
		public virtual double GetLastUpdateTime() { return 0.0; }

		/// <summary>
		/// Sets the last update time
		/// </summary>
		/// <param name="newTime">The new update time.</param>
		public virtual void SetLastUpdateTime( double newTime ) {}

		/// <summary>
		/// Clears the necessary text.
		/// </summary>
		public virtual void ClearText() {}

		/// <summary>
		/// Returns the remaining round time minutes.
		/// </summary>
		/// <returns>Remaining minutes.</returns>
		public virtual int GetRemainingMinutes() { return 0; }

		/// <summary>
		/// Return the remaining round time seconds.
		/// </summary>
		/// <returns>Remaining round time seconds.</returns>
		public virtual int GetRemainingSeconds() { return 0; }
	}
}