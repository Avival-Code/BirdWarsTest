/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
PlayerAttackComponent inherits from AttackComponent.
It produces modified attacks that are used by player gameObjects.
*********************************************/
using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.AttackComponents
{
	/// <summary>
	/// PlayerAttackComponent inherits from AttackComponent.
	/// It produces modified attacks that are used by player gameObjects.
	/// </summary>
	public class PlayerAttackComponent : AttackComponent
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public PlayerAttackComponent()
			:
			base()
		{}

		/// <summary>
		/// Constructor that takes a damage input parameter.
		/// </summary>
		/// <param name="damageIn">An interger value</param>
		public PlayerAttackComponent( int damageIn )
			:
			base( damageIn )
		{}

		/// <summary>
		/// Calculates a specialized attack area which is
		/// used to check if a gameObject is being hit by another object's
		/// attack.
		/// </summary>
		/// <param name="gameObject">The gameObject that is attacking.</param>
		/// <returns></returns>
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