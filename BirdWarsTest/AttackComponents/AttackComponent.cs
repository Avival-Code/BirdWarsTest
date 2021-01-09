using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.AttackComponents
{
	public class AttackComponent
	{
		public AttackComponent()
		{
			Damage = 1;
			IsAttacking = false;
			AttackTimer = 13;
			attackWidth = 75;
			attackHeight = 75;
		}

		public AttackComponent( int damageIn )
		{
			Damage = damageIn;
			IsAttacking = false;
			AttackTimer = 13;
			attackWidth = 75;
			attackHeight = 75;
		}

		public AttackComponent( int damageIn, int attackWidthIn, int attackHeightIn )
		{
			Damage = damageIn;
			IsAttacking = false;
			AttackTimer = 13;
			attackWidth = attackWidthIn;
			attackHeight = attackHeightIn;
		}

		public virtual void DoAttack()
		{
			if( !IsAttacking )
			{
				IsAttacking = true;
			}
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
			AttackTimer -= 1;
			if( AttackTimer <= 0 )
			{
				AttackTimer = 13;
				IsAttacking = false;
			}
		}

		virtual public Rectangle GetAttackRectangle( GameObject gameObject ) 
		{
			return new Rectangle( -100, -100, 1, 1 );
		}

		public int Damage { get; private set; }

		public bool IsAttacking { get; private set; }

		public int AttackTimer { get; private set; }
		protected int attackWidth;
		protected int attackHeight;
	}
}