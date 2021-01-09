using BirdWarsTest.InputComponents.EventArguments;

namespace BirdWarsTest.Utilities
{
	public class StringValidator
	{
		public bool AreLoginArgsValid( LoginEventArgs loginEvent )
		{
			return ( IsEmailValid( loginEvent.Email ) && IsPasswordValid( loginEvent.Password ) );
		}

		public bool AreRegisterArgsValid( RegisterEventArgs registerEvents )
		{
			return ( IsNameValid( registerEvents.Name ) && AreLastNamesValid( registerEvents.LastNames ) && 
					 IsUsernameValid( registerEvents.Username ) && IsEmailValid( registerEvents.Email ) && 
					 IsNewPasswordValid( registerEvents.Password, registerEvents.ConfirmPassword ) );
		}

		public bool IsNameValid( string name )
		{
			return ( IsStringValidSize( name, minNameCount, maxNameCount) && CheckForInvalidChar( name ) &&
					 !HasSpaces( name ) );
		}

		public bool AreLastNamesValid( string lastNames )
		{
			return ( IsStringValidSize( lastNames, minLastNameCount, maxLastNameCount ) && CheckForInvalidChar( lastNames ) );
		}

		public bool IsUsernameValid( string username )
		{
			return ( IsStringValidSize( username, minUsernameCount, maxUsernameCount ) && CheckForInvalidChar( username ) &&
					 !HasSpaces( username ) );
		}

		public bool IsEmailValid( string email )
		{
			return ( IsStringValidSize( email, minEmailCount, maxEmailCount ) && CheckForInvalidChar( email ) &&
					 HasSingleAtChar( email ) && !HasSpaces( email ) );
		}

		public bool IsPasswordValid( string password )
		{
			return ( IsStringValidSize( password, minPasswordCount, maxPasswordCount ) && CheckForInvalidChar( password ) &&
					 !HasSpaces( password ) );
		}

		public bool IsNewPasswordValid( string password, string confirmPassword )
		{
			return ( IsStringValidSize( password, minPasswordCount, maxPasswordCount ) && CheckForInvalidChar( password ) && 
					 !HasSpaces( password ) && IsPasswordAndConfirmEqual( password, confirmPassword ) );
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

		public bool IsPasswordAndConfirmEqual( string password, string confirmPassword )
		{
			return password.Equals( confirmPassword );
		}

		private const int minNameCount = 3;
		private const int maxNameCount = 20;
		private const int minLastNameCount = 3;
		private const int maxLastNameCount = 25;
		private const int minUsernameCount = 3;
		private const int maxUsernameCount = 25;
		private const int minEmailCount = 4;
		private const int maxEmailCount = 40;
		private const int minPasswordCount = 8;
		private const int maxPasswordCount = 20;
	}
}