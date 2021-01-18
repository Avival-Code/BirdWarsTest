using Microsoft.VisualStudio.TestTools.UnitTesting;
using BirdWarsTest.Utilities;
using BirdWarsTest.InputComponents.EventArguments;

namespace BirdWarsTesting
{
	[TestClass]
	public class BirdWarsUtilityTest
	{	
		[TestMethod]
		public void TestNameValid()
		{
			Assert.IsTrue( stringValidator.IsNameValid( "Christian" ) );
			Assert.IsFalse( stringValidator.IsNameValid( "T" ) );
			Assert.IsFalse( stringValidator.IsNameValid( "123dsadsa" ) );
			Assert.IsFalse( stringValidator.IsNameValid( "ThisIsAReallyLongNameToodsadsadsa" ) );
		}

		[TestMethod]
		public void TestLastNameValid()
		{
			Assert.IsTrue( stringValidator.AreLastNamesValid( "Avila Valdes" ) );
			Assert.IsFalse( stringValidator.AreLastNamesValid( "T" ) );
			Assert.IsFalse( stringValidator.AreLastNamesValid( "Rupert12" ) );
			Assert.IsFalse( stringValidator.AreLastNamesValid( "ThisIsAReallyLongNameToodsdsdsadsads" ) );
		}

		[TestMethod]
		public void TestEmailValid()
		{
			Assert.IsTrue( stringValidator.IsEmailValid( "christian@gmail.com" ) );
			Assert.IsFalse( stringValidator.IsEmailValid( "randomth3ing3211" ) );
			Assert.IsFalse(stringValidator.IsEmailValid( "sa1" ) );
		}

		[TestMethod]
		public void TestUsernameValid()
		{
			Assert.IsTrue( stringValidator.IsUsernameValid( "Avival" ) );
			Assert.IsFalse( stringValidator.IsUsernameValid( "sa" ) );
			Assert.IsFalse(stringValidator.IsUsernameValid( "sajshdgrteundhfgcbshawjehdhsdsjbds" ) );
		}

		[TestMethod]
		public void TestPasswordValid()
		{
			Assert.IsTrue( stringValidator.IsPasswordValid( "papitas1" ) );
			Assert.IsFalse( stringValidator.IsPasswordValid( "sa" ) );
			Assert.IsFalse( stringValidator.IsPasswordValid( "sajshdgrteundhfgcbsdsadsasasasahawjehdhsdsjbds" ) );
		}

		[TestMethod]
		public void TestNewPasswordValid()
		{
			Assert.IsTrue( stringValidator.IsNewPasswordValid( "papitas1", "papitas1" ) );
			Assert.IsFalse( stringValidator.IsNewPasswordValid( "sa", "sa" ) );
			Assert.IsFalse( stringValidator.IsNewPasswordValid( "sajshdgrteundhfgcbsdsadsasasasahawjehdhsdsj", "obelisk" ) );
		}

		[TestMethod]
		public void AreLoginArgsValid()
		{
			LoginEventArgs eventArgs = new LoginEventArgs();
			eventArgs.Email = "christian.avival@gmail.com";
			eventArgs.Password = "papitas1";

			LoginEventArgs falseArgs = new LoginEventArgs();
			falseArgs.Email = "testing";
			falseArgs.Password = "no";

			Assert.IsTrue( stringValidator.AreLoginArgsValid( eventArgs ) );
			Assert.IsFalse( stringValidator.AreLoginArgsValid( falseArgs ) );
		}

		[TestMethod]
		public void AreRegisterArgsValid()
		{
			RegisterEventArgs trueEvents = new RegisterEventArgs();
			trueEvents.Name = "Christian";
			trueEvents.LastNames = "Avila Valdes";
			trueEvents.Email = "christian.avival@gmail.com";
			trueEvents.Username = "Avival";
			trueEvents.Password = "papitas1";
			trueEvents.ConfirmPassword = "papitas1";

			RegisterEventArgs falseEvents = new RegisterEventArgs();
			falseEvents.Name = "C";
			falseEvents.LastNames = "Avila Valdes";
			falseEvents.Email = "christian.avivalgmail.com";
			falseEvents.Username = "A";
			falseEvents.Password = "papi";
			falseEvents.ConfirmPassword = "papitas1";

			Assert.IsTrue( stringValidator.AreRegisterArgsValid( trueEvents ) );
			Assert.IsFalse( stringValidator.AreRegisterArgsValid( falseEvents ) );
		}

		[TestMethod]
		public void IsIPAddressValid()
		{
			Assert.IsTrue( stringValidator.IsAddressValid( "127.0.0.1" ) );
			Assert.IsFalse( stringValidator.IsAddressValid( "1469273" ) );
			Assert.IsFalse(stringValidator.IsAddressValid( "14Asjdfds9273" ) );
			Assert.IsFalse(stringValidator.IsAddressValid( "143.21.3" ) );
		}

		[TestMethod]
		public void IsPortValid()
		{
			Assert.IsTrue( stringValidator.IsPortValid( "1234" ) );
		}

		[TestMethod]
		public void StringManagerHasValidStrings()
		{
			Assert.IsNotNull( stringManager );
		}

		StringValidator stringValidator = new StringValidator();
		StringManager stringManager = new StringManager();
	}
}