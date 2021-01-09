using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.AttackComponents
{
	public class GrenadeAttackComponent : AttackComponent
	{
		public GrenadeAttackComponent()
			:
			base( 3, 196, 196 )
		{
			exploded = false;
		}

		public GrenadeAttackComponent( int damageIn )
			:
			base( damageIn, 196, 196 )
		{
			exploded = false;
		}

		public override void DoAttack()
		{
			if( !exploded )
			{
				base.DoAttack();
				exploded = true;
			}
		}

		public override Rectangle GetAttackRectangle( GameObject gameObject )
		{
			Rectangle attackRectangle = new Rectangle( -100, -100, 1, 1 );
			if( gameObject.Attack.IsAttacking )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X  + ( int )( gameObject.Graphics.GetTextureSize().X / 2 ) - 
												 attackWidth / 2, ( int )gameObject.Position.Y + 
												 ( int )( gameObject.Graphics.GetTextureSize().Y / 2 ) - attackHeight / 2, 
												 attackWidth, attackHeight );
			}
			return attackRectangle;
		}

		private bool exploded;
	}
}