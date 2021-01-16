/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Class used to check the validity of user input strings
used throughout the applicaction.
*********************************************/
using BirdWarsTest.InputComponents.EventArguments;

namespace BirdWarsTest.Utilities
{
	/// <summary>
	/// Class used to check the validity of user input strings
	/// used throughout the applicaction.
	/// </summary>
	public class StringValidator
	{
		/// <summary>
		/// Applies the required string validation to login arguments.
		/// </summary>
		/// <param name="loginEvent">Login events that hold user input</param>
		/// <returns>bool inidicating if arguments are valid.</returns>
		public bool AreLoginArgsValid( LoginEventArgs loginEvent )
		{
			return ( IsEmailValid( loginEvent.Email ) && IsPasswordValid( loginEvent.Password ) );
		}

		/// <summary>
		/// Appliess the required string validation to registration arguments.
		/// </summary>
		/// <param name="registerEvents">Registration events that hold user input.</param>
		/// <returns>bool indicating if arguments are valid.</returns>
		public bool AreRegisterArgsValid( RegisterEventArgs registerEvents )
		{
			return ( IsNameValid( registerEvents.Name ) && AreLastNamesValid( registerEvents.LastNames ) && 
					 IsUsernameValid( registerEvents.Username ) && IsEmailValid( registerEvents.Email ) && 
					 IsNewPasswordValid( registerEvents.Password, registerEvents.ConfirmPassword ) );
		}

		/// <summary>
		/// Applies validation checks to a name string.
		/// </summary>
		/// <param name="name">User input string</param>
		/// <returns>bool indicating if string is valid.</returns>
		public bool IsNameValid( string name )
		{
			return ( IsStringValidSize( name, MinNameCount, MaxNameCount ) && CheckForInvalidChar( name ) &&
					 !HasSpaces( name ) && !HasNumbers( name ) );
		}

		/// <summary>
		/// Applies validation checks to a last name string.
		/// </summary>
		/// <param name="lastNames">User input string</param>
		/// <returns>bool indicating if string is valid.</returns>
		public bool AreLastNamesValid( string lastNames )
		{
			return ( IsStringValidSize( lastNames, MinLastNameCount, MaxLastNameCount ) && CheckForInvalidChar( lastNames ) &&
					 !HasNumbers( lastNames ) );
		}

		/// <summary>
		/// Applies validation checks to a username string.
		/// </summary>
		/// <param name="username">User input string.</param>
		/// <returns>bool indicating if string is valid.</returns>
		public bool IsUsernameValid( string username )
		{
			return ( IsStringValidSize( username, MinUsernameCount, MaxUsernameCount ) && CheckForInvalidChar( username ) &&
					 !HasSpaces( username ) );
		}

		/// <summary>
		/// Applies validation checks to an email string.
		/// </summary>
		/// <param name="email">User input string</param>
		/// <returns>bool indicating if string is valid.</returns>
		public bool IsEmailValid( string email )
		{
			return ( IsStringValidSize( email, MinEmailCount, MaxEmailCount ) && CheckForInvalidChar( email ) &&
					 HasSingleAtChar( email ) && !HasSpaces( email ) );
		}

		/// <summary>
		/// Applies validation checks to a password string.
		/// </summary>
		/// <param name="password">User input string.</param>
		/// <returns>bool indicating if string is valid.</returns>
		public bool IsPasswordValid( string password )
		{
			return ( IsStringValidSize( password, MinPasswordCount, MaxPasswordCount ) && CheckForInvalidChar( password ) &&
					 !HasSpaces( password ) );
		}

		/// <summary>
		/// Applies validation checks to a new password string.
		/// </summary>
		/// <param name="password">User input string to compare to.</param>
		/// <param name="confirmPassword">User input string to validate.</param>
		/// <returns>bool indicating if string is valid.</returns>
		public bool IsNewPasswordValid( string password, string confirmPassword )
		{
			return ( IsStringValidSize( password, MinPasswordCount, MaxPasswordCount ) && CheckForInvalidChar( password ) && 
					 !HasSpaces( password ) && IsPasswordAndConfirmEqual( password, confirmPassword ) );
		}

		/// <summary>
		/// Applies validation string to an ip address string/
		/// </summary>
		/// <param name="address">User input string.</param>
		/// <returns>bool indicating if string is valid.</returns>
		public bool IsAddressValid( string address )
		{
			return ( IsAddressValidSize( address ) && IsValidAddress( address ) && HasValidAddressOrPortValues( address ) );
		}

		/// <summary>
		/// Applies validation chacks to a port string.
		/// </summary>
		/// <param name="port">User input string</param>
		/// <returns>bool indicating if string is valid.</returns>
		public bool IsPortValid( string port )
		{
			return HasValidAddressOrPortValues( port );
		}

		private bool IsAddressValidSize( string address )
		{
			return ( address.Length >= MinAddressSize && address.Length <= MaxAddressSize );
		}

		private bool IsValidAddress( string address )
		{
			bool isValidAddress = false;
			int totalPeriods = 0;
			foreach( var letter in address )
			{
				if( letter == '.' )
				{
					totalPeriods += 1;
				}
			}

			if( totalPeriods == 3 )
			{
				isValidAddress = true;
			}

			return isValidAddress;
		}

		private bool HasValidAddressOrPortValues( string addressOrPort )
		{
			bool hasValidValues = true;
			foreach( var letter in addressOrPort )
			{
				if( letter != '.' && letter < '0' && letter > '9' )
				{
					hasValidValues = false;
				}
			}

			return hasValidValues;
		}

		private bool IsStringValidSize( string input, int minSize, int maxSize )
		{
			return ( input.Length >= minSize && input.Length <= maxSize );
		}

		private bool CheckForInvalidChar( string input )
		{
			bool isValid = true;
			foreach( var character in input )
			{
				if( character == '|' || character == 39 || character == ';' ||
					character == '=' )
				{
					isValid = false;
				}
			}
			return isValid;
		}

		private bool HasNumbers( string input )
		{
			bool hasNumbers = false;
			foreach( var character in input )
			{
				if( character >= '0' && character <= '9' )
				{
					hasNumbers = true;
				}
			}
			return hasNumbers;
		}

		private bool HasSpaces( string input)
		{
			bool hasSpaces = false;
			foreach( var character in input )
			{
				if( character == ' ' )
				{
					hasSpaces = true;
				}
			}
			return hasSpaces;
		}

		private bool HasSingleAtChar( string email )
		{
			int atCounter = 0;
			foreach( var character in email )
			{
				if( character == '@' )
				{
					atCounter += 1;
				}
			}
			return atCounter == 1;
		}

		/// <summary>
		/// Checks to see whether the entered passwords are equal in value.
		/// </summary>
		/// <param name="password">User input to compare to.</param>
		/// <param name="confirmPassword">User input to compare to.</param>
		/// <returns>bool indicating if strings are of equal value.</returns>
		public bool IsPasswordAndConfirmEqual( string password, string confirmPassword )
		{
			return password.Equals( confirmPassword );
		}

		private const int MinNameCount = 3;
		private const int MaxNameCount = 20;
		private const int MinLastNameCount = 3;
		private const int MaxLastNameCount = 25;
		private const int MinUsernameCount = 3;
		private const int MaxUsernameCount = 25;
		private const int MinEmailCount = 4;
		private const int MaxEmailCount = 40;
		private const int MinPasswordCount = 8;
		private const int MaxPasswordCount = 20;
		private const int MinAddressSize = 7;
		private const int MaxAddressSize = 15;
	}
}