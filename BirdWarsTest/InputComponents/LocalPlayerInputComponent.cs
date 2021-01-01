using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.InputComponents
{
	class LocalPlayerInputComponent : InputComponent
	{
		public LocalPlayerInputComponent( StateHandler handlerIn )
		{
			handler = handlerIn;
			LastActiveVelocity = new Vector2( 1.0f, 0.0f );
			playerSpeed = 300.0f;
			lastUpdateTime = 1.0;
		}

		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState )
		{
			GetKeyboardStates( state );
			if( !gameObject.Health.IsDead() )
			{
				if( HandlePlayerMovement() )
				{
					handler.networkManager.SendPlayerStateChangeMessage( gameObject );
				}
				if( HandlePlayerAbilities( gameObject ) )
				{
					handler.networkManager.SendPlayerAttackMessage( gameObject.Identifier );
				}
			}
		}

		private bool HandlePlayerAbilities( GameObject localPlayer )
		{
			bool actionPerformed = false;

			if( IsKeyDown( Keys.A ) && !localPlayer.Attack.IsAttacking )
			{
				localPlayer.Attack.DoAttack();
				actionPerformed = true;
			}

			return actionPerformed;
		}

		private bool HandlePlayerMovement()
		{
			bool velocityChanged = false;
			velocity = Vector2.Zero;

			if( IsKeyDown( Keys.Up ) )
			{
				velocity = new Vector2( 0.0f, -1.0f );
				LastActiveVelocity = velocity;
				if( IsKeyPressed( Keys.Up ) )
				{
					velocityChanged = true;
				}
			}

			if( IsKeyReleased( Keys.Up ) )
			{
				velocity = new Vector2( velocity.X, 0.0f );
				velocityChanged = true;
			}

			if( IsKeyDown( Keys.Down ) )
			{
				velocity = new Vector2( 0.0f, 1.0f );
				LastActiveVelocity = velocity;
				if( IsKeyPressed( Keys.Down ) )
				{
					velocityChanged = true;
				}
			}

			if( IsKeyReleased( Keys.Down ) )
			{
				velocity = new Vector2( velocity.X, 0.0f );
				velocityChanged = true;
			}

			if( IsKeyDown( Keys.Left ) )
			{
				velocity = new Vector2( -1.0f, 0.0f );
				LastActiveVelocity = velocity;
				if( IsKeyPressed( Keys.Left ) )
				{
					velocityChanged = true;
				}
			}

			if( IsKeyReleased( Keys.Left ) )
			{
				velocity = new Vector2( 0.0f, velocity.Y );
				velocityChanged = true;
			}

			if( IsKeyDown( Keys.Right ) )
			{
				velocity = new Vector2( 1.0f, 0.0f );
				LastActiveVelocity = velocity;
				if( IsKeyPressed( Keys.Right ) )
				{
					velocityChanged = true;
				}
			}

			if( IsKeyReleased( Keys.Right ) )
			{
				velocity = new Vector2( 0.0f, velocity.Y );
				velocityChanged = true;
			}

			velocity *= playerSpeed;

			return velocityChanged;
		}

		private bool IsKeyDown( Keys keyToTest )
		{
			return currentKeyBoardState.IsKeyDown( keyToTest );
		}

		private bool IsKeyPressed( Keys keyToTest )
		{
			return currentKeyBoardState.IsKeyDown( keyToTest ) && lastKeyboardState.IsKeyUp( keyToTest );
		}

		private bool IsKeyReleased( Keys keyToTest )
		{
			return currentKeyBoardState.IsKeyUp( keyToTest ) && lastKeyboardState.IsKeyDown( keyToTest );
		}

		private void GetKeyboardStates( KeyboardState state )
		{
			lastKeyboardState = currentKeyBoardState;
			currentKeyBoardState = state;
		}

		public override Vector2 GetVelocity()
		{
			return velocity;
		}

		public override Vector2 GetLastActiveVelocity()
		{
			return LastActiveVelocity;
		}

		public override void SetVelocity( Vector2 newVelocity )
		{
			velocity = newVelocity;
		}

		public override double GetLastUpdateTime()
		{
			return lastUpdateTime;
		}

		public override void SetLastUpdateTime( double newTime )
		{
			lastUpdateTime = newTime;
		}

		public Vector2 LastActiveVelocity { get; private set; }

		private StateHandler handler;
		private KeyboardState currentKeyBoardState;
		private KeyboardState lastKeyboardState;
		private Vector2 velocity;
		private float playerSpeed;
		private double lastUpdateTime;
	}
}