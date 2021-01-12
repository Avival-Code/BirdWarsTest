/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Input component that handles local player movement and
actions.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.InputComponents
{
	/// <summary>
	/// Input component that handles local player movement and
	/// actions.
	/// </summary>
	public class LocalPlayerInputComponent : InputComponent
	{
		/// <summary>
		/// Default constructor. sets default last update time, direction
		/// and player speed.
		/// </summary>
		/// <param name="handlerIn">Game statehandler</param>
		public LocalPlayerInputComponent( StateHandler handlerIn )
		{
			handler = handlerIn;
			lastActiveVelocity = new Vector2( 1.0f, 0.0f );
			playerSpeed = 300.0f;
			lastUpdateTime = 1.0;
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
		/// Handes the input recieved based on the current game object state,
		/// current keyboard state and current game state.
		/// </summary>
		/// <param name="gameObject">The Game object</param>
		/// <param name="state">current keyboard state</param>
		/// <param name="gameState">current game state</param>
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

		/// <summary>
		/// Returns the current player velocity.
		/// </summary>
		/// <returns>Returns the current player velocity.</returns>
		public override Vector2 GetVelocity()
		{
			return velocity;
		}

		/// <summary>
		/// Returns the player speed.
		/// </summary>
		/// <returns>Returns the player speed.</returns>
		public override float GetObjectSpeed()
		{
			return playerSpeed;
		}

		/// <summary>
		/// Returns the last active velocity.
		/// </summary>
		/// <returns>Returns the last active velocity.</returns>
		public override Vector2 GetLastActiveVelocity()
		{
			return lastActiveVelocity;
		}

		/// <summary>
		/// Sets the current player velocity.
		/// </summary>
		/// <param name="newVelocity">The new velocity</param>
		public override void SetVelocity( Vector2 newVelocity )
		{
			velocity = newVelocity;
		}

		/// <summary>
		/// Returns the last time the player object was updated
		/// by a game message.
		/// </summary>
		/// <returns>Returns the last time the player object was updated by a game message.</returns>
		public override double GetLastUpdateTime()
		{
			return lastUpdateTime;
		}

		/// <summary>
		/// Sets the last update time.
		/// </summary>
		/// <param name="newTime">The new update time.</param>
		public override void SetLastUpdateTime( double newTime )
		{
			lastUpdateTime = newTime;
		}

		private Vector2 lastActiveVelocity;
		private readonly StateHandler handler;
		private KeyboardState currentKeyBoardState;
		private KeyboardState lastKeyboardState;
		private Vector2 velocity;
		private readonly float playerSpeed;
		private double lastUpdateTime;
	}
}