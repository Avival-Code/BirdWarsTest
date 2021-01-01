using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using BirdWarsTest.InputComponents;

namespace BirdWarsTest.AttackComponents
{
	public class LocalPlayerAttackComponent : AttackComponent
	{
		public LocalPlayerAttackComponent()
			: 
			base()
		{}

		public LocalPlayerAttackComponent( int damageIn )
			:
			base( damageIn )
		{}

		public override Rectangle GetAttackRectangle( GameObject gameObject )
		{
			Rectangle attackRectangle = new Rectangle( -100, -100, 1, 1 );
			if( gameObject.Attack.IsAttacking && 
				( ( LocalPlayerInputComponent )gameObject.Input ).LastActiveVelocity == new Vector2( 0.0f, -1.0f ) )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X, ( int )gameObject.Position.Y - attackHeight,
												 attackWidth, attackHeight );
			}

			if( gameObject.Attack.IsAttacking &&
				( ( LocalPlayerInputComponent )gameObject.Input ).LastActiveVelocity == new Vector2( 0.0f, 1.0f ) )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X, ( int )gameObject.Position.Y + attackHeight,
												 attackWidth, attackHeight );
			}

			if( gameObject.Attack.IsAttacking &&
				( ( LocalPlayerInputComponent )gameObject.Input ).LastActiveVelocity == new Vector2( -1.0f, 0.0f ) )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X - attackWidth, ( int )gameObject.Position.Y,
												 attackWidth, attackHeight );
			}

			if( gameObject.Attack.IsAttacking &&
				( ( LocalPlayerInputComponent )gameObject.Input ).LastActiveVelocity == new Vector2( 1.0f, 0.0f ) )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X + attackWidth, ( int )gameObject.Position.Y,
												 attackWidth, attackHeight );
			}

			return attackRectangle;
		}
	}
}