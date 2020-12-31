﻿using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.AttackComponents
{
	public class AttackComponent
	{
		public AttackComponent()
		{
			Damage = 1;
			IsAttacking = false;
			attackTimer = 10;
			attackWidth = 50;
			attackHeight = 50;
		}

		public AttackComponent( int damageIn )
		{
			Damage = damageIn;
			IsAttacking = false;
			attackTimer = 10;
		}

		public void Attack()
		{
			IsAttacking = true;
		}

		public void UpdateAttackTimer()
		{
			if( IsAttacking )
			{
				TimerTick();
			}
		}

		public void TimerTick( )
		{
			attackTimer -= 1;
			if( attackTimer <= 0 )
			{
				attackTimer = 10;
				IsAttacking = false;
			}
		}

		virtual public Rectangle GetAttackRectangle( GameObject gameObject ) 
		{
			return new Rectangle( -100, -100, 1, 1 );
		}

		public int Damage { get; private set; }

		public bool IsAttacking { get; set; }

		private int attackTimer;
		protected int attackWidth;
		protected int attackHeight;
	}
}