/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
GrenadeAttackComponent inherits from AttackComponent.
It produces modified attacks that are used by grenade gameObjects.
*********************************************/
using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.AttackComponents
{
	/// <summary>
	/// GrenadeAttackComponent inherits from AttackComponent.
	/// It modifies attacks that are used by grenade gameObjects.
	/// </summary>
	public class GrenadeAttackComponent : AttackComponent
	{
		/// <summary>
		/// Default constructor. It constructs the componet and 
		/// sets variables to their defualt values.
		/// </summary>
		public GrenadeAttackComponent()
			:
			base( 3, 196, 196 )
		{
			exploded = false;
		}

		/// <summary>
		/// Constructor that takes a damage input parameter. It sets
		/// variables to their default values.
		/// </summary>
		/// <param name="damageIn">An integer value.</param>
		public GrenadeAttackComponent( int damageIn )
			:
			base( damageIn, 196, 196 )
		{
			exploded = false;
		}

		/// <summary>
		/// Activates GameObject attack.
		/// </summary>
		public override void DoAttack()
		{
			if( !exploded )
			{
				base.DoAttack();
				exploded = true;
			}
		}

		/// <summary>
		/// Calculates a specialized attack area for grenade which 
		/// is used to check if a gameObejct is hit by another object's
		/// attack.
		/// </summary>
		/// <param name="gameObject">The gameObject that is attacking.</param>
		/// <returns>The attack area rectangle</returns>
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