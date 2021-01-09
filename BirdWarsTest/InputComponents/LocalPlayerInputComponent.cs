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
			lastActiveVelocity = new Vector2( 1.0f, 0.0f );
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
				HandleGrenadeThrow( gameState );
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

		private void HandleGrenadeThrow( GameState gameState )
		{
			if( IsKeyDown( Keys.S ) )
			{
				( ( PlayState )gameState ).ItemManager.SpawnGrenade( ( ( PlayState )gameState ).NetWorkManager,
																	 ( ( PlayState )gameState ).PlayerManager );
			}
		}

		private bool HandlePlayerMovement()
		{
			bool velocityChanged = false;
			velocity = Vector2.Zero;

			if( IsKeyDown( Keys.Up ) )
			{
				velocity = new Vector2( 0.0f, -1.0f );
				lastActiveVelocity = velocity;
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
				lastActiveVelocity = velocity;
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
				lastActiveVelocity = velocity;
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
				lastActiveVelocity = velocity;
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

			velocityChanged = HandleDiagonalPlayerMovement( velocityChanged );

			velocity *= playerSpeed;

			return velocityChanged;
		}

		private bool HandleDiagonalPlayerMovement( bool velocityChangedIn )
		{
			bool velocityChanged = velocityChangedIn;

			if( IsKeyDown( Keys.Up ) && IsKeyDown( Keys.Right ) )
			{
				velocity = new Vector2( 1.0f, -1.0f );
				lastActiveVelocity = velocity;
				if( IsKeyPressed( Keys.Up ) && IsKeyPressed( Keys.Right ) )
				{
					velocityChanged = true;
				}
			}

			if( IsKeyReleased( Keys.Up ) && IsKeyReleased( Keys.Right ) )
			{
				velocity = new Vector2( 0.0f, 0.0f );
				velocityChanged = true;
			}

			if( IsKeyDown( Keys.Up ) && IsKeyDown( Keys.Left ) )
			{
				velocity = new Vector2( -1.0f, -1.0f );
				lastActiveVelocity = velocity;
				if( IsKeyPressed( Keys.Up ) && IsKeyPressed( Keys.Left ) )
				{
					velocityChanged = true;
				}
			}

			if( IsKeyReleased( Keys.Up ) && IsKeyReleased( Keys.Left ) )
			{
				velocity = new Vector2( 0.0f, 0.0f );
				velocityChanged = true;
			}

			if( IsKeyDown( Keys.Down ) && IsKeyDown( Keys.Right ) )
			{
				velocity = new Vector2( 1.0f, 1.0f );
				lastActiveVelocity = velocity;
				if( IsKeyPressed( Keys.Down ) && IsKeyPressed( Keys.Right ) )
				{
					velocityChanged = true;
				}
			}

			if( IsKeyReleased( Keys.Down ) && IsKeyReleased( Keys.Right ) )
			{
				velocity = new Vector2( 0.0f, 0.0f );
				velocityChanged = true;
			}

			if( IsKeyDown( Keys.Down ) && IsKeyDown( Keys.Left ) )
			{
				velocity = new Vector2( -1.0f, 1.0f );
				lastActiveVelocity = velocity;
				if( IsKeyPressed( Keys.Down ) && IsKeyPressed( Keys.Left ) )
				{
					velocityChanged = true;
				}
			}

			if( IsKeyReleased( Keys.Down ) && IsKeyReleased( Keys.Left ) )
			{
				velocity = new Vector2( 0.0f, 0.0f );
				velocityChanged = true;
			}

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

		public override float GetObjectSpeed()
		{
			return playerSpeed;
		}

		public override Vector2 GetLastActiveVelocity()
		{
			return lastActiveVelocity;
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

		private Vector2 lastActiveVelocity;
		private StateHandler handler;
		private KeyboardState currentKeyBoardState;
		private KeyboardState lastKeyboardState;
		private Vector2 velocity;
		public float playerSpeed;
		private double lastUpdateTime;
	}
}