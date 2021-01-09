using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.AttackComponents
{
	public class PlayerAttackComponent : AttackComponent
	{
		public PlayerAttackComponent()
			:
			base()
		{}

		public PlayerAttackComponent( int damageIn )
			:
			base( damageIn )
		{}

		public override Rectangle GetAttackRectangle( GameObject gameObject )
		{
			Rectangle attackRectangle = new Rectangle( -100, -100, 1, 1 );
			if( gameObject.Attack.IsAttacking &&
				( int )gameObject.Input.GetLastActiveVelocity().Y < 0 &&
				( int )gameObject.Input.GetLastActiveVelocity().X == 0 )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X, ( int )gameObject.Position.Y - attackHeight,
												 attackWidth, attackHeight );
			}

			if( gameObject.Attack.IsAttacking &&
				( int )gameObject.Input.GetLastActiveVelocity().Y > 0 &&
				( int )gameObject.Input.GetLastActiveVelocity().X == 0 )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X, ( int )gameObject.Position.Y + attackHeight,
												 attackWidth, attackHeight );
			}

			if( gameObject.Attack.IsAttacking &&
				( int )gameObject.Input.GetLastActiveVelocity().X < 0 &&
				( int )gameObject.Input.GetLastActiveVelocity().Y == 0 )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X - attackWidth, ( int )gameObject.Position.Y,
												 attackWidth, attackHeight );
			}

			if( gameObject.Attack.IsAttacking &&
				( int )gameObject.Input.GetLastActiveVelocity().X > 0 &&
				( int )gameObject.Input.GetLastActiveVelocity().Y == 0 )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X + attackWidth, ( int )gameObject.Position.Y,
												 attackWidth, attackHeight );
			}

			if( gameObject.Attack.IsAttacking &&
				( int )gameObject.Input.GetLastActiveVelocity().X > 0 &&
				( int )gameObject.Input.GetLastActiveVelocity().Y < 0 )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X + attackWidth, 
												 ( int )gameObject.Position.Y - attackHeight, attackWidth, attackHeight);
			}

			if( gameObject.Attack.IsAttacking &&
				( int )gameObject.Input.GetLastActiveVelocity().X > 0 &&
				( int )gameObject.Input.GetLastActiveVelocity().Y > 0 )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X + attackWidth, 
												 ( int )gameObject.Position.Y + attackHeight, attackWidth, attackHeight );
			}

			if( gameObject.Attack.IsAttacking &&
				( int )gameObject.Input.GetLastActiveVelocity().X < 0 &&
				( int )gameObject.Input.GetLastActiveVelocity().Y < 0 )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X - attackWidth, 
											     ( int )gameObject.Position.Y - attackHeight, attackWidth, attackHeight );
			}

			if( gameObject.Attack.IsAttacking &&
				( int )gameObject.Input.GetLastActiveVelocity().X < 0 &&
				( int )gameObject.Input.GetLastActiveVelocity().Y > 0 )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X - attackWidth, 
												 ( int )gameObject.Position.Y + attackHeight, attackWidth, attackHeight );
			}

			return attackRectangle;
		}
	}
}