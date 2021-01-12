/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Interface of Account Data Access Object.
*********************************************/
using System.Collections.Generic;

namespace BirdWarsTest.Database
{
	/// <summary>
	/// Interface of Account Data Access Object.
	/// </summary>
	public interface IAccountDAO
	{
		/// <summary>
		/// Create method.
		/// </summary>
		/// <param name="account">An existing account instance.</param>
		/// <returns>bool</returns>
		public bool Create( Account account );

		/// <summary>
		/// Read Method
		/// </summary>
		/// <returns>An account list.</returns>
		public List< Account > ReadAll();

		/// <summary>
		/// Read method.
		/// </summary>
		/// <param name="userId">User Id of desired account.</param>
		/// <returns>An account</returns>
		public Account Read( int userId );

		/// <summary>
		/// Update method.
		/// </summary>
		/// <param name="userId">An integer value.</param>
		/// <param name="totalMatchesPlayed">An integer value.</param>
		/// <param name="matchesWon">An integer value.</param>
		/// <param name="matchesLost">An integer value.</param>
		/// <param name="matchesSurvived">An integer value.</param>
		/// <param name="money">An integer value.</param>
		/// <param name="seconds">An integer value.</param>
		/// <returns>bool</returns>
		public bool Update( int userId, int totalMatchesPlayed, int matchesWon, int matchesLost, 
							int matchesSurvived, int money, int seconds );

		/// <summary>
		/// Delete method.
		/// </summary>
		/// <param name="userId">UserId</param>
		/// <returns>bool</returns>
		public bool Delete( int userId );
	}
}