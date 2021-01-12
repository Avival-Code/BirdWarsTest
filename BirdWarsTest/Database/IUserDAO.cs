/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Interface for user Data Access Object.
*********************************************/
using System.Collections.Generic;

namespace BirdWarsTest.Database
{
	/// <summary>
	/// Interface for user Data Access Object.
	/// </summary>
	public interface IUserDAO
	{
		/// <summary>
		/// Create method.
		/// </summary>
		/// <param name="user">An existing user instance.</param>
		/// <returns>bool</returns>
		public bool Create( User user );

		/// <summary>
		/// Read method.
		/// </summary>
		/// <returns>A user list.</returns>
		public List< User > ReadAll();

		/// <summary>
		/// Read method.
		/// </summary>
		/// <param name="email">Email of desired User.</param>
		/// <returns>A user</returns>
		public User Read( string email );

		/// <summary>
		/// Read method
		/// </summary>
		/// <param name="email">Email of desired user.</param>
		/// <param name="password">Password of desired user.</param>
		/// <returns></returns>
		public User Read( string email, string password );

		/// <summary>
		/// Update method.
		/// </summary>
		/// <param name="userId">An integer value.</param>
		/// <param name="names">A string value.</param>
		/// <param name="lastName">A string value.</param>
		/// <param name="username">A string value.</param>
		/// <param name="email">A string value.</param>
		/// <param name="password">A string value.</param>
		/// <returns>bool</returns>
		public bool Update( int userId, string names, string lastName, string username, 
							string email, string password );

		/// <summary>
		/// Delete method.
		/// </summary>
		/// <param name="userId">UserOd of desired user.</param>
		/// <returns>bool</returns>
		public bool Delete( int userId );
	}
}