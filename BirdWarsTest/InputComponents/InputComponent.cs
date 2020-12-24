using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.InputComponents
{
	public abstract class InputComponent
	{
		abstract public void HandleInput( GameObject gameObject, GameTime gameTime );

		abstract public void HandleInput( GameObject gameObject, KeyboardState state );

		abstract public void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState );

		public virtual string GetText() { return ""; }

		public virtual Vector2 GetVelocity() { return Vector2.Zero; }

		public virtual void SetVelocity( Vector2 newVelocity ) {}

		public virtual double GetLastUpdateTime() { return 0.0; }

		public virtual void SetLastUpdateTime( double newTime ) {}

		public virtual void ClearText() {}

		public virtual int GetRemainingMinutes() { return 0; }

		public virtual int GetRemainingSeconds() { return 0; }
	}
}