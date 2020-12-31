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

		public override Rectangle GetAttackRectangle( GameObject gameObject )
		{
			Rectangle attackRectangle = new Rectangle(-100, -100, 1, 1);
			if( gameObject.Attack.IsAttacking &&
				( ( ExternalPlayerInputComponent )gameObject.Input ).LastActiveVelocity == new Vector2( 0.0f, -1.0f ) )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X, ( int )gameObject.Position.Y - attackHeight,
												 attackWidth, attackHeight );
			}

			if( gameObject.Attack.IsAttacking &&
				( ( ExternalPlayerInputComponent )gameObject.Input ).LastActiveVelocity == new Vector2( 0.0f, 1.0f ) )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X, ( int )gameObject.Position.Y + attackHeight,
												 attackWidth, attackHeight );
			}

			if( gameObject.Attack.IsAttacking &&
				( ( ExternalPlayerInputComponent )gameObject.Input ).LastActiveVelocity == new Vector2( -1.0f, 0.0f ) )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X - attackWidth, ( int )gameObject.Position.Y,
												 attackWidth, attackHeight );
			}

			if( gameObject.Attack.IsAttacking &&
				( ( ExternalPlayerInputComponent )gameObject.Input ).LastActiveVelocity == new Vector2( 1.0f, 0.0f ) )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X + attackWidth, ( int )gameObject.Position.Y,
												 attackWidth, attackHeight );
			}

			return attackRectangle;
		}
	}
}