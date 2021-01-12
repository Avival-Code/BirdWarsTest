/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Used to keep track of states and their order.
Game states MUST be initialized according to the
order in this file.
*********************************************/
namespace BirdWarsTest.States
{
	/// <summary>
	/// Used to keep track of states and their order.
	///	Game states MUST be initialized according to the
	///order in this file.
	/// </summary>
	public enum StateTypes
	{
		/// <summary>
		/// The login state -0
		/// </summary>
		LoginState,
		/// <summary>
		/// The user registry state -1
		/// </summary>
		UserRegistryState,
		/// <summary>
		/// The password recovery state -2
		/// </summary>
		PasswordRecoveryState,
		/// <summary>
		/// The main menu state -3
		/// </summary>
		MainMenuState,
		/// <summary>
		/// The waiting room state -4
		/// </summary>
		WaitingRoomState,
		/// <summary>
		/// The play state -5
		/// </summary>
		PlayState,
		/// <summary>
		/// The options state -6
		/// </summary>
		OptionsState,
		/// <summary>
		/// The statistics state -7
		/// </summary>
		StatisticsState
	}
}
