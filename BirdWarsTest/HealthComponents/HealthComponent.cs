﻿using System;

namespace BirdWarsTest.HealthComponents
{
	public class HealthComponent
	{
		public HealthComponent()
		{
			Health = 10;
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

		public int Health { get; set; }
	}
}