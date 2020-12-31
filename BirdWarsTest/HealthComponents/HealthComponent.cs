using System;

namespace BirdWarsTest.HealthComponents
{
	public class HealthComponent
	{
		public HealthComponent()
		{
			maxHealth = Health = 10;
		}

		public HealthComponent( int maxHealthIn )
		{
			maxHealth = Health = maxHealthIn;
		}

		public void TakeDamage( int damage )
		{
			Health -= damage;
		}

		public void Heal( int health )
		{
			Health += health;
		}

		public bool IsDead()
		{
			return Health <= 0;
		}

		public float GetRemainingHealthPercent()
		{
			return ( float )Health / ( float )maxHealth;
		}

		public int Health { get; private set; }
		private int maxHealth;
	}
}