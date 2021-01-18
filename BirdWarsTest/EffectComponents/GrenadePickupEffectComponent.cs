/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Produces a modified item effect relevant to a grenade item.
*********************************************/
using BirdWarsTest.GameObjects.ObjectManagers;
using BirdWarsTest.Network;

namespace BirdWarsTest.EffectComponents
{
	/// <summary>
	/// Produces a modified item effect relevant to a grenade item.
	/// </summary>
	public class GrenadePickupEffectComponent : EffectComponent
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public GrenadePickupEffectComponent()
		{
			grenadesProvided = 3;
		}

		/// <summary>
		/// Executes grenade effect on local player.
		/// </summary>
		/// <param name="networkManager">Game network manager.</param>
		/// <param name="playerManager">Player manager.</param>
		public override void DoEffect( INetworkManager networkManager, PlayerManager playerManager )
		{
			playerManager.GrenadeAmount += grenadesProvided;
		}

		private readonly int grenadesProvided;
	}
}