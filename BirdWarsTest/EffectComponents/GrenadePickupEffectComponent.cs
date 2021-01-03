using BirdWarsTest.GameObjects.ObjectManagers;
using BirdWarsTest.Network;

namespace BirdWarsTest.EffectComponents
{
	public class GrenadePickupEffectComponent : EffectComponent
	{
		public GrenadePickupEffectComponent()
		{
			grenadesProvided = 2;
		}

		public override void DoEffect( INetworkManager networkManager, PlayerManager playerManager )
		{
			playerManager.GrenadeAmount += grenadesProvided;
		}

		private int grenadesProvided;
	}
}