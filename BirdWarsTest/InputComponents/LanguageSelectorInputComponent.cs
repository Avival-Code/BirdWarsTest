/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Input component used to change the current selected
language in the options state.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.States;
using BirdWarsTest.Utilities;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.InputComponents
{
	/// <summary>
	/// Input component used to change the current selected
	/// language in the options state.
	/// </summary>
	public class LanguageSelectorInputComponent : InputComponent
	{
		/// <summary>
		/// Default constructor. Sets a reference to the game string
		/// manager, a reference to the language object the user
		/// can see and basic values.
		/// </summary>
		/// <param name="languageObjectIn"></param>
		/// <param name="stringManagerIn"></param>
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

		/// <summary>
		/// Handles the input recieved based on the current game object state
		/// and game time.
		/// </summary>
		/// <param name="gameObject">Current game object.</param>
		/// <param name="gameTime">Current game time.</param>
		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		/// <summary>
		/// Sets the last mouse state and gets current mouse state. Handles
		/// click to language object arrows.
		/// </summary>
		/// <param name="gameObject">THe current gameObject.</param>
		/// <param name="state">The current keyboard state.</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state )
		{
			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();

			HandleLeftArrowInput();
			HandleRightArrowInput();
			UpdateTimer();
		}

		/// <summary>
		/// Handes the input recieved based on the current game object state,
		/// current keyboard state and current game state.
		/// </summary>
		/// <param name="gameObject">The Game object</param>
		/// <param name="state">current keyboard state</param>
		/// <param name="gameState">current game state</param>
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

		private readonly GameObject languageObject;
		private StringManager stringManager;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
		private int minLanguageValue;
		private int maxLanguageValue;
		private int timer;
		private bool isTimerActivated;

		///<value>Bool indicating if language selection was changed</value>
		public bool ChangedLanguage { get; private set; }

		///<value>The currently selected language.</value>
		public int CurrentLanguageValue { get; private set; }
	}
}