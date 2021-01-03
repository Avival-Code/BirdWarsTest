using BirdWarsTest.GameObjects.ObjectManagers;
using BirdWarsTest.Network;

namespace BirdWarsTest.EffectComponents
{
	public class CoinEffectComponent : EffectComponent
	{
		public CoinEffectComponent( int coinValueIn )
		{
			coinValue = coinValueIn;
		}

		public override void DoEffect( INetworkManager networkManager, PlayerManager playerManager )
		{
			if( networkManager.IsHost() )
			{
				( ( ServerNetworkManager )networkManager ).UserSession.CurrentAccount.AddMoney( coinValue );
			}
			else
			{
				( ( ClientNetworkManager )networkManager ).UserSession.CurrentAccount.AddMoney( coinValue );
			}
		}

		private int coinValue;
	}
}