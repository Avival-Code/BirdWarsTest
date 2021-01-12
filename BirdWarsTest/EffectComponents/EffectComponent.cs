/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Base Effect component class of all gameObjects.
*********************************************/
using BirdWarsTest.GameObjects.ObjectManagers;
using BirdWarsTest.Network;

namespace BirdWarsTest.EffectComponents
{
	/// <summary>
	/// Base Effect component class of all gameObjects.
	/// </summary>
	public abstract class EffectComponent
	{
		/// <summary>
		/// Executes the effect on a given gameObject.
		/// </summary>
		/// <param name="networkManager">Game network manager</param>
		/// <param name="playerManager">Player manager</param>
		public abstract void DoEffect( INetworkManager networkManager, PlayerManager playerManager );
	}
}