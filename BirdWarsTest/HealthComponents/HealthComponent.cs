using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.HealthComponents
{
	public class HealthComponent
	{
		public HealthComponent()
		{
			maxHealth = Health = 10;
			coolDownTimer = 20;
			TookDamage = false;
		}

		public HealthComponent( int maxHealthIn )
		{
			maxHealth = Health = maxHealthIn;
			coolDownTimer = 20;
			TookDamage = false;
		}

		public void TakeDamage( int damage )
		{
			if( !TookDamage )
			{
				Health -= damage;
				TookDamage = true;
			}
		}

		public void TakeFullDamage()
		{
			Health = 0;
		}

		public void Heal( int health )
		{
			Health += health;
			if( Health > maxHealth )
			{
				Health = maxHealth;
			}
		}

		public bool IsDead()
		{
			return Health <= 0;
		}

		public float GetRemainingHealthPercent()
		{
			return ( float )Health / ( float )maxHealth;
		}

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

		public Rectangle GetPlayerHitBox( GameObject gameObject )
		{
			Rectangle playerHitBox = new Rectangle( 0, 0, 0, 0 );
			if( gameObject.Input.GetLastActiveVelocity().X == 0 &&
				gameObject.Input.GetLastActiveVelocity().Y > 0 )
			{
				Vector2 placeholder = new Vector2( gameObject.Position.X + 10, gameObject.Position.Y + 10 );
				playerHitBox = new Rectangle( ( int )placeholder.X, ( int )placeholder.Y, ( int )placeholder.X + 44, 
											  ( int )placeholder.Y + 70 );
			}

			if( gameObject.Input.GetLastActiveVelocity().X == 0 &&
				gameObject.Input.GetLastActiveVelocity().Y < 0 )
			{
				Vector2 placeholder = new Vector2( gameObject.Position.X + 10, gameObject.Position.Y + 10 );
				playerHitBox = new Rectangle( ( int )placeholder.X, ( int )placeholder.Y, ( int )placeholder.X + 44,
											  ( int )placeholder.Y + 70 );
			}

			if( ( gameObject.Input.GetLastActiveVelocity().X > 0 &&
				  gameObject.Input.GetLastActiveVelocity().Y == 0 ) ||
				( gameObject.Input.GetLastActiveVelocity().X > 0 &&
				  gameObject.Input.GetLastActiveVelocity().Y < 0 ) ||
				( gameObject.Input.GetLastActiveVelocity().X > 0 &&
				  gameObject.Input.GetLastActiveVelocity().Y > 0 ) )
			{
				Vector2 placeholder = new Vector2( gameObject.Position.X + 25, gameObject.Position.Y + 11 );
				playerHitBox = new Rectangle( ( int )placeholder.X, ( int )placeholder.Y, ( int )placeholder.X + 60,
											  ( int )placeholder.Y + 71 );
			}

			if( ( gameObject.Input.GetLastActiveVelocity().X < 0 &&
				  gameObject.Input.GetLastActiveVelocity().Y == 0 ) ||
				( gameObject.Input.GetLastActiveVelocity().X < 0 &&
				  gameObject.Input.GetLastActiveVelocity().Y < 0 ) ||
				( gameObject.Input.GetLastActiveVelocity().X < 0 &&
				  gameObject.Input.GetLastActiveVelocity().Y > 0 ) )
			{
				Vector2 placeholder = new Vector2( gameObject.Position.X + 5, gameObject.Position.Y + 11 );
				playerHitBox = new Rectangle( ( int )placeholder.X, ( int )placeholder.Y, ( int )placeholder.X + 60,
											  ( int )placeholder.Y + 71 );
			}
			return playerHitBox;
		}

		public int Health { get; private set; }
		public bool TookDamage { get; private set; }
		private int maxHealth;
		private int coolDownTimer;
	}
}