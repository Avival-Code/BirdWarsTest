using BirdWarsTest.GameObjects.ObjectManagers;
using BirdWarsTest.Network;

namespace BirdWarsTest.EffectComponents
{
	public class RecoveryEffectComponent : EffectComponent
	{
		public RecoveryEffectComponent( int healthValueIn )
		{
			healthValue = healthValueIn;
		}

		public override void DoEffect( INetworkManager networkManager, PlayerManager playerManager )
		{
			playerManager.GetLocalPlayer().Health.Heal( healthValue );
		}

		private int healthValue;
	}
}