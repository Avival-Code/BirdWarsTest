/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Health component for all gameObjects. Handles damage, 
healing, and has health related utilies.
*********************************************/
using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.HealthComponents
{
	/// <summary>
	/// Health component for all gameObjects. Handles damage, 
	/// healing, and has health related utilies.
	/// </summary>
	public class HealthComponent
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public HealthComponent()
		{
			maxHealth = Health = 10;
			coolDownTimer = 20;
			TookDamage = false;
		}

		/// <summary>
		/// Creates instance of  health component with specified
		/// HP.
		/// </summary>
		/// <param name="maxHealthIn">Max HP</param>
		public HealthComponent( int maxHealthIn )
		{
			maxHealth = Health = maxHealthIn;
			coolDownTimer = 20;
			TookDamage = false;
		}

		/// <summary>
		/// If the gameobject hasn't taken damage,
		/// subtract damage from remaining health.	
		/// </summary>
		/// <param name="damage">recieved damage.</param>
		public void TakeDamage( int damage )
		{
			if( !TookDamage )
			{
				Health -= damage;
				TookDamage = true;
			}
		}

		/// <summary>
		/// Kills object.
		/// </summary>
		public void TakeFullDamage()
		{
			Health = 0;
		}

		/// <summary>
		/// Replenishes health by specified amount.
		/// </summary>
		/// <param name="health"></param>
		public void Heal( int health )
		{
			Health += health;
			if( Health > maxHealth )
			{
				Health = maxHealth;
			}
		}

		/// <summary>
		/// Checks if Health > 0.
		/// </summary>
		/// <returns>bool</returns>
		public bool IsDead()
		{
			return Health <= 0;
		}

		/// <summary>
		/// Used by life bar graphics component to calculate
		/// color of bar and length.
		/// </summary>
		/// <returns>float.</returns>
		public float GetRemainingHealthPercent()
		{
			return ( float )Health / ( float )maxHealth;
		}

		/// <summary>
		/// Updates the TookDamageTimer.
		/// </summary>
		public void UpdateCoolDownTimer()
		{
			if( TookDamage )
			{
				coolDownTimer -= 1;
				if( coolDownTimer <= 0 )
				{
					coolDownTimer = 20;
					TookDamage = false;
				}
			}
		}

		/// <summary>
		/// Returns a calculated area rectangle used to check if payer has been
		/// hit. Calculations are based on the objects texture size.
		/// </summary>
		/// <param name="gameObject"></param>
		/// <returns>Rectangle of area where object can be hit.</returns>
		public Rectangle GetPlayerHitBox( GameObject gameObject )
		{
			Rectangle playerHitBox = new Rectangle( -1000, -1000, 1, 1 );
			if( gameObject.Input.GetLastActiveVelocity().X == 0 &&
				gameObject.Input.GetLastActiveVelocity().Y > 0 )
			{
				Vector2 placeholder = new Vector2( gameObject.Position.X + 10, gameObject.Position.Y + 10 );
				playerHitBox = new Rectangle( ( int )placeholder.X, ( int )placeholder.Y, 44, 70 );
			}

			if( gameObject.Input.GetLastActiveVelocity().X == 0 &&
				gameObject.Input.GetLastActiveVelocity().Y < 0 )
			{
				Vector2 placeholder = new Vector2( gameObject.Position.X + 10, gameObject.Position.Y + 10 );
				playerHitBox = new Rectangle( ( int )placeholder.X, ( int )placeholder.Y, 44, 70 );
			}

			if( ( gameObject.Input.GetLastActiveVelocity().X > 0 &&
				  gameObject.Input.GetLastActiveVelocity().Y == 0 ) ||
				( gameObject.Input.GetLastActiveVelocity().X > 0 &&
				  gameObject.Input.GetLastActiveVelocity().Y < 0 ) ||
				( gameObject.Input.GetLastActiveVelocity().X > 0 &&
				  gameObject.Input.GetLastActiveVelocity().Y > 0 ) )
			{
				Vector2 placeholder = new Vector2( gameObject.Position.X + 25, gameObject.Position.Y + 11 );
				playerHitBox = new Rectangle( ( int )placeholder.X, ( int )placeholder.Y, 60, 71 );
			}

			if( ( gameObject.Input.GetLastActiveVelocity().X < 0 &&
				  gameObject.Input.GetLastActiveVelocity().Y == 0 ) ||
				( gameObject.Input.GetLastActiveVelocity().X < 0 &&
				  gameObject.Input.GetLastActiveVelocity().Y < 0 ) ||
				( gameObject.Input.GetLastActiveVelocity().X < 0 &&
				  gameObject.Input.GetLastActiveVelocity().Y > 0 ) )
			{
				Vector2 placeholder = new Vector2( gameObject.Position.X + 5, gameObject.Position.Y + 11 );
				playerHitBox = new Rectangle( ( int )placeholder.X, ( int )placeholder.Y, 60, 71 );
			}
			return playerHitBox;
		}

		/// <value>The object health.</value>
		public int Health { get; private set; }

		/// <value>Bool indicating if object took damage.</value>
		public bool TookDamage { get; private set; }
		private readonly int maxHealth;
		private int coolDownTimer;
	}
}