using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.AttackComponents
{
	public class GrenadeAttackComponent : AttackComponent
	{
		public GrenadeAttackComponent()
			:
			base( 3, 196, 196 )
		{}

		public GrenadeAttackComponent( int damageIn )
			:
			base( damageIn, 196, 196 )
		{}

		public override Rectangle GetAttackRectangle( GameObject gameObject )
		{
			Rectangle attackRectangle = new Rectangle( -100, -100, 1, 1 );
			if( gameObject.Attack.IsAttacking )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X - attackWidth / 2, 
												 ( int )gameObject.Position.Y - attackHeight / 2, attackWidth, attackHeight );
			}
			return attackRectangle;
		}
	}
}