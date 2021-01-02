using System;

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

		public int Health { get; private set; }
		public bool TookDamage { get; private set; }
		private int maxHealth;
		private int coolDownTimer;
	}
}