using Microsoft.VisualStudio.TestTools.UnitTesting;
using BirdWarsTest.AttackComponents;
using BirdWarsTest.EffectComponents;
using BirdWarsTest.HealthComponents;

namespace BirdWarsTesting
{
	[TestClass]
	public class BirdWarsComponentTest
	{
		[TestMethod]
		public void TestAttackComponents()
		{
			int normalDamage = 1;
			int grenadeDamage = 3;
			int playerDamage = 2;
			AttackComponent normalAttack = new AttackComponent( normalDamage );
			GrenadeAttackComponent grenadeAttack = new GrenadeAttackComponent( grenadeDamage );
			PlayerAttackComponent playerAttack = new PlayerAttackComponent( playerDamage );

			Assert.IsNotNull( normalAttack );
			Assert.IsNotNull( grenadeAttack );
			Assert.IsNotNull( playerAttack );

			Assert.IsTrue( normalAttack.Damage == normalDamage );
			Assert.IsTrue( grenadeAttack.Damage == grenadeDamage );
			Assert.IsTrue( playerAttack.Damage == playerDamage );
		}

		[TestMethod]
		public void TestEffectComponents()
		{
			int coinValue = 1;
			int recoveryValue = 3;

			CoinEffectComponent coinEffect = new CoinEffectComponent( coinValue );
			GrenadePickupEffectComponent grenadeEffect = new GrenadePickupEffectComponent();
			RecoveryEffectComponent recoveryEffect = new RecoveryEffectComponent( recoveryValue );

			Assert.IsNotNull( coinEffect );
			Assert.IsNotNull( grenadeEffect );
			Assert.IsNotNull( recoveryEffect );
		}

		[TestMethod]
		public void TestHealthComponents()
		{
			int maxHealth = 20;
			HealthComponent defaultHealth = new HealthComponent();
			HealthComponent setHealth = new HealthComponent( maxHealth );

			Assert.IsNotNull( defaultHealth );
			Assert.IsNotNull( setHealth );

			Assert.IsTrue( defaultHealth.Health == 10 );
			Assert.IsTrue( setHealth.Health == maxHealth );

			defaultHealth.TakeDamage( 5 );
			setHealth.TakeDamage( 10 );

			Assert.IsTrue( defaultHealth.Health == 5 );
			Assert.IsTrue( setHealth.Health == 10 );

			defaultHealth.Heal( 3 );
			setHealth.Heal( 5 );

			Assert.IsTrue( defaultHealth.Health == 8 );
			Assert.IsTrue( setHealth.Health == 15 );

			defaultHealth.TakeFullDamage();
			setHealth.TakeFullDamage();

			Assert.IsTrue( defaultHealth.Health == 0 );
			Assert.IsTrue( setHealth.Health == 0 );
		}
	}
}