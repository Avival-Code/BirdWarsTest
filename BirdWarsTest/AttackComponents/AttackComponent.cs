/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Attack component handles gameObject attacks.
Creates attack area rectangle, attacks, stores damage values
and has methods that handle attack duration.
*********************************************/
using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.AttackComponents
{
	/// <summary>
	/// AttackComponent handles gameObject attacks.
	/// Creates attack area rectangle, attacks, stores damage values
	/// and has methods that handle attack timer.
	/// </summary>
	public class AttackComponent
	{
		/// <summary>
		/// Default constructor. Sets Damage to 1, AttackTimer to 13,
		/// attackWidth and attackHeight to 75.
		/// </summary>
		public AttackComponent()
		{
			Damage = 1;
			IsAttacking = false;
			AttackTimer = 13;
			attackWidth = 75;
			attackHeight = 75;
		}

		/// <summary>
		/// Constructor that takes a damage input parameter. It sets
		/// Damage to the damage input, AttackTimer to 13, attackWidth
		/// and attackHeight to 75
		/// </summary>
		/// <param name="damageIn">An integer value</param>
		public AttackComponent( int damageIn )
		{
			Damage = damageIn;
			IsAttacking = false;
			AttackTimer = 13;
			attackWidth = 75;
			attackHeight = 75;
		}

		/// <summary>
		/// Constructor that takes a damage, width and height parameter.
		/// AttackTimer is set to 13. Remainig properties are set to their
		/// respective input values.
		/// </summary>
		/// <param name="damageIn">An integer value</param>
		/// <param name="attackWidthIn">An integer value</param>
		/// <param name="attackHeightIn">An integer value</param>
		public AttackComponent( int damageIn, int attackWidthIn, int attackHeightIn )
		{
			Damage = damageIn;
			IsAttacking = false;
			AttackTimer = 13;
			attackWidth = attackWidthIn;
			attackHeight = attackHeightIn;
		}

		/// <summary>
		/// Activates gameObject attack.
		/// If the property IsAttacking is false, it is set to true.
		/// </summary>
		public virtual void DoAttack()
		{
			if( !IsAttacking )
			{
				IsAttacking = true;
			}
		}

		/// <summary>
		/// Updates the AttackTimer if the IsAttacking property 
		/// is true.
		/// </summary>
		public void UpdateAttackTimer()
		{
			if( IsAttacking )
			{
				TimerTick();
			}
		}

		/// <summary>
		/// Decreases attackTimer by 1 everyframe.
		/// If AttackTimer is less than or equal to zero,
		/// the AttackTimer is reset to its starting value
		/// and IsAttacking is reset to false;
		/// </summary>
		public void TimerTick( )
		{
			AttackTimer -= 1;
			if( AttackTimer <= 0 )
			{
				AttackTimer = 13;
				IsAttacking = false;
			}
		}

		/// <summary>
		/// Calculates and returns the attack area rectangle which
		/// is used to check if a gameobject is hit by another object's
		/// attack.
		/// </summary>
		/// <param name="gameObject">The gameObject that is attacking.</param>
		/// <returns>The attack area rectangle.</returns>
		virtual public Rectangle GetAttackRectangle( GameObject gameObject ) 
		{
			return new Rectangle( -100, -100, 1, 1 );
		}

		/// <Value>The damage Property of AttackComponent.</Value>
		public int Damage { get; private set; }

		/// <Value>Bool used to check whether a gameObject is attacking or not.</Value>
		public bool IsAttacking { get; private set; }

		/// <Value>Property used to maintain attack for a certain amount of time.</Value>
		public int AttackTimer { get; private set; }

		/// <summary>
		/// Width used to calculate the attack area rectangle.
		/// </summary>
		protected int attackWidth;

		/// <summary>
		/// Height used to calculate the attack area rectangle.
		/// </summary>
		protected int attackHeight;
	}
}