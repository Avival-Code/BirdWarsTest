using BirdWarsTest.GameObjects;
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.States;
using BirdWarsTest.Utilities;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.InputComponents
{
	class LanguageSelectorInputComponent : InputComponent
	{
		public LanguageSelectorInputComponent( GameObject languageObjectIn, StringManager stringManagerIn )
		{
			languageObject = languageObjectIn;
			stringManager = stringManagerIn;
			minLanguageValue = 0;
			maxLanguageValue = 1;
			CurrentLanguageValue = ( int )stringManager.CurrentLanguage;
			timer = 0;
			isTimerActivated = false;
			ChangedLanguage = false;
		}
		public override void HandleInput( GameObject gameObject, KeyboardState state )
		{
			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();

			HandleLeftArrowInput();
			HandleRightArrowInput();
			UpdateTimer();
		}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState ) {}

		private void HandleLeftArrowInput()
		{
			var mouseRectangle = new Rectangle( currentMouseState.X, currentMouseState.Y, 1, 1 );

			if( mouseRectangle.Intersects( ( ( LanguageSelectorGraphicsComponent )languageObject.Graphics ).GetLeftArrowBounds() ) )
			{
				if( currentMouseState.LeftButton == ButtonState.Released &&
					previousMouseState.LeftButton == ButtonState.Pressed )
				{
					HandleLeftArrowClick();
				}
			}
		}

		private void HandleLeftArrowClick()
		{
			if( CurrentLanguageValue > minLanguageValue )
			{
				CurrentLanguageValue -= 1;
				( ( LanguageSelectorGraphicsComponent )languageObject.Graphics ).SelectedLanguage = ( Languages )CurrentLanguageValue;
				switch( CurrentLanguageValue )
				{
					case ( int )Languages.English:
						( ( LanguageSelectorGraphicsComponent )languageObject.Graphics ).Text = stringManager.GetString( StringNames.English );
						break;

					case ( int )Languages.Spanish:
						( ( LanguageSelectorGraphicsComponent )languageObject.Graphics ).Text = stringManager.GetString( StringNames.Spanish );
						break;
				}
				isTimerActivated = true;
				ChangedLanguage = true;
			}
		}

		private void HandleRightArrowInput()
		{
			var mouseRectangle = new Rectangle( currentMouseState.X, currentMouseState.Y, 1, 1 );

			if( mouseRectangle.Intersects( ( ( LanguageSelectorGraphicsComponent )languageObject.Graphics ).GetRightArrowBounds() ) )
			{
				if( currentMouseState.LeftButton == ButtonState.Released &&
					previousMouseState.LeftButton == ButtonState.Pressed )
				{
					HandleRightArrowClick();
				}
			}
		}

		private void HandleRightArrowClick()
		{
			if( CurrentLanguageValue < maxLanguageValue )
			{
				CurrentLanguageValue += 1;
				( ( LanguageSelectorGraphicsComponent )languageObject.Graphics ).SelectedLanguage = ( Languages )CurrentLanguageValue;
				switch( CurrentLanguageValue )
				{
					case ( int )Languages.English:
						( ( LanguageSelectorGraphicsComponent )languageObject.Graphics ).Text = stringManager.GetString( StringNames.English );
						break;

					case ( int )Languages.Spanish:
						( ( LanguageSelectorGraphicsComponent )languageObject.Graphics ).Text = stringManager.GetString( StringNames.Spanish );
						break;
				}
				isTimerActivated = true;
				ChangedLanguage = true;
			}
		}

		private void UpdateTimer()
		{
			if( isTimerActivated )
			{
				timer += 1;
				if( timer >= 10 )
				{
					timer = 0;
					isTimerActivated = !isTimerActivated;
				}
			}
		}

		private GameObject languageObject;
		private StringManager stringManager;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
		private int minLanguageValue;
		private int maxLanguageValue;
		private int timer;
		private bool isTimerActivated;
		public bool ChangedLanguage { get; private set; }
		public int CurrentLanguageValue { get; private set; }
	}
}