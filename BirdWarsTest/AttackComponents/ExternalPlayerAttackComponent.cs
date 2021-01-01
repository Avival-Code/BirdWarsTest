using BirdWarsTest.GameObjects;
using BirdWarsTest.InputComponents;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.AttackComponents
{
	public class ExternalPlayerAttackComponent : AttackComponent
	{
		public ExternalPlayerAttackComponent()
			:
			base()
		{}

		public ExternalPlayerAttackComponent( int damageIn )
			:
			base( damageIn )
		{}

		public override Rectangle GetAttackRectangle( GameObject gameObject )
		{
			Rectangle attackRectangle = new Rectangle( -100, -100, 1, 1 );
			if( gameObject.Attack.IsAttacking &&
				( int )( ( ExternalPlayerInputComponent )gameObject.Input ).LastActiveVelocity.Y < 0 &&
				( int )( ( ExternalPlayerInputComponent )gameObject.Input ).LastActiveVelocity.X == 0 )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X, ( int )gameObject.Position.Y - attackHeight,
												 attackWidth, attackHeight );
			}

			if( gameObject.Attack.IsAttacking &&
				( int )( ( ExternalPlayerInputComponent )gameObject.Input ).LastActiveVelocity.Y > 0 &&
				( int )( ( ExternalPlayerInputComponent )gameObject.Input ).LastActiveVelocity.X == 0 )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X, ( int )gameObject.Position.Y + attackHeight,
												 attackWidth, attackHeight );
			}

			if( gameObject.Attack.IsAttacking &&
				( int )( ( ExternalPlayerInputComponent )gameObject.Input ).LastActiveVelocity.X < 0 &&
				( int )( ( ExternalPlayerInputComponent )gameObject.Input ).LastActiveVelocity.Y == 0 )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X - attackWidth, ( int )gameObject.Position.Y,
												 attackWidth, attackHeight );
			}

			if( gameObject.Attack.IsAttacking &&
				( int )( ( ExternalPlayerInputComponent )gameObject.Input ).LastActiveVelocity.X > 0 &&
				( int )( ( ExternalPlayerInputComponent )gameObject.Input ).LastActiveVelocity.Y == 0)
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X + attackWidth, ( int )gameObject.Position.Y,
												 attackWidth, attackHeight );
			}

			return attackRectangle;
		}
	}
}