using BirdWarsTest.GameObjects.ObjectManagers;
using BirdWarsTest.Network;

namespace BirdWarsTest.EffectComponents
{
	public abstract class EffectComponent
	{
		public abstract void DoEffect( INetworkManager networkManager, PlayerManager playerManager );
	}
}