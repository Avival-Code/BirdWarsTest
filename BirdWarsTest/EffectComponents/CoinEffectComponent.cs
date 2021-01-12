/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Produces a modified item effect relevant to a coin item.
*********************************************/
using BirdWarsTest.GameObjects.ObjectManagers;
using BirdWarsTest.Network;

namespace BirdWarsTest.EffectComponents
{
	/// <summary>
	/// Produces modified item effect relevant to a coin item.
	/// </summary>
	public class CoinEffectComponent : EffectComponent
	{
		/// <summary>
		/// Creates CoinEffectComponent and set coin value.
		/// </summary>
		/// <param name="coinValueIn">The desired coin value.</param>
		public CoinEffectComponent( int coinValueIn )
		{
			coinValue = coinValueIn;
		}

		/// <summary>
		/// Executes coin effect on current user.
		/// </summary>
		/// <param name="networkManager">Game network manager.</param>
		/// <param name="playerManager">Player manager.</param>
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

		private readonly int coinValue;
	}
}