using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Database
{
	public class PasswordChangeManager
	{
		public PasswordChangeManager()
		{
			PasswordChangeWasSolicited = false;
			TargetUserEmail = "";
		}

		public void SetChangeCode( string userEmail )
		{
			var random = new Random();
			ChangeCode = random.Next(lowerLimit, upperLimit);
			TargetUserEmail = userEmail;
			PasswordChangeWasSolicited = true;
		}

		public void ResetManager()
		{
			ChangeCode = 0;
			TargetUserEmail = "";
			PasswordChangeWasSolicited = false;
		}

		public int ChangeCode { get; private set; }
		public bool PasswordChangeWasSolicited { get; private set; }
		public string TargetUserEmail { get; set; }

		private const int lowerLimit = 1000;
		private const int upperLimit = 10000;
	}
}