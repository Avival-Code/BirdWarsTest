/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Produces a recovery item effect relevant to a recovery item.
*********************************************/
using BirdWarsTest.GameObjects.ObjectManagers;
using BirdWarsTest.Network;

namespace BirdWarsTest.EffectComponents
{
	/// <summary>
	/// Produces a recovery item effect relevant to a recovery item.
	/// </summary>
	public class RecoveryEffectComponent : EffectComponent
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="healthValueIn"></param>
		public RecoveryEffectComponent( int healthValueIn )
		{
			healthValue = healthValueIn;
		}

		/// <summary>
		/// Executes recovery effect on local player.
		/// </summary>
		/// <param name="networkManager">Game network manager.</param>
		/// <param name="playerManager">Player manager.</param>
		public override void DoEffect( INetworkManager networkManager, PlayerManager playerManager )
		{
			playerManager.GetLocalPlayer().Health.Heal( healthValue );
		}

		private readonly int healthValue;
	}
}