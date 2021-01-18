/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Handles the loading and retrieval of strings for user interface 
and language changes in application.
*********************************************/
using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace BirdWarsTest.Utilities
{
	/// <summary>
	/// Handles the loading and retrieval of strings for user interface 
	///and language changes in application.
	/// </summary>
	public class StringManager
	{
		/// <summary>
		/// Creates an instance of string Manager.
		/// Instantialtes string list, sets current language
		/// and retrieves strings pertaining to current language.
		/// </summary>
		public StringManager()
		{
			Strings = new List< string >();
			CurrentLanguage = Languages.Spanish;
			LoadStrings( CurrentLanguage );
		}

		private void LoadStrings( Languages currentLanguage )
		{
			string fileName;
			string filePath;
			string[] tempStrings;

			try
			{
				switch( currentLanguage )
				{
					case Languages.English:
						fileName = @"English.txt";
						filePath = Path.Combine( Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location ), fileName );
						tempStrings = File.ReadAllLines( filePath );
						foreach( var line in tempStrings )
						{
							Strings.Add( line );
						}
						break;

					case Languages.Spanish:
						fileName = @"Spanish.txt";
						filePath = Path.Combine( Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location ), fileName );
						tempStrings = File.ReadAllLines( filePath );
						foreach( var line in tempStrings )
						{
							Strings.Add( line );
						}
						break;
				}
			}
			catch( FileNotFoundException e )
			{
				Console.WriteLine( e.Message );
			}
		}

		/// <summary>
		/// Changes the currently selected language to
		/// the specified input language.
		/// </summary>
		/// <param name="newLanguage">Specified input language.</param>
		public void ChangeLanguage( Languages newLanguage )
		{
			Strings.Clear();
			CurrentLanguage = newLanguage;
			LoadStrings( CurrentLanguage );
		}

		/// <summary>
		/// Returns a string from the list of game strings
		/// at the specified identifer value.
		/// </summary>
		/// <param name="name">The Stringname enumerator</param>
		/// <returns></returns>
		public string GetString( StringNames name )
		{
			return Strings[ ( int )name ];
		}

		///<value>The list of game strings</value>
		public List< string > Strings { get; private set; }

		///<value>Current selected language</value>
		public Languages CurrentLanguage { get; private set; }
	}
}