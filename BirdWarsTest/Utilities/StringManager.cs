using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace BirdWarsTest.Utilities
{
	public class StringManager
	{
		public StringManager()
		{
			Strings = new List< string >();
			CurrentLanguage = Languages.English;
			LoadStrings( CurrentLanguage );
		}

		private void LoadStrings( Languages currentLanguage )
		{
			string fileName = "";
			string filePath = "";
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

		public void ChangeLanguage( Languages newLanguage )
		{
			Strings.Clear();
			CurrentLanguage = newLanguage;
			LoadStrings( CurrentLanguage );
		}

		public string GetString( StringNames name )
		{
			return Strings[ ( int )name ];
		}

		public List< string > Strings { get; private set; }
		public Languages CurrentLanguage { get; private set; }
	}
}