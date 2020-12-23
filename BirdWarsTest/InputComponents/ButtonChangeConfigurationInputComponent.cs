using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using BirdWarsTest.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BirdWarsTest.InputComponents
{
	class ButtonChangeConfigurationInputComponent : InputComponent
	{
		public ButtonChangeConfigurationInputComponent( StateHandler handlerIn, GameObject selectorInputIn, 
														StringManager stringManagerIn )
		{
			handler = handlerIn;
			selectorInput = selectorInputIn;
			stringManager = stringManagerIn;
			click += ToOtherScreen;
		}

		public override void HandleInput( GameObject gameObject, KeyboardState state )
		{
			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();

			var mouseRectangle = new Rectangle( currentMouseState.X, currentMouseState.Y, 1, 1 );

			if (mouseRectangle.Intersects( gameObject.GetRectangle() ) )
			{
				if( currentMouseState.LeftButton == ButtonState.Released &&
					previousMouseState.LeftButton == ButtonState.Pressed )
				{
					click?.Invoke( this, new EventArgs() );
				}
			}
		}

		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState )
		{
			HandleInput( gameObject, state );
		}

		private void ToOtherScreen( Object sender, System.EventArgs e )
		{
			ChangeLanguage();
			handler.ChangeState( handler.LastState );
		}

		private void ChangeLanguage()
		{
			if( ( ( LanguageSelectorInputComponent )selectorInput.Input ).ChangedLanguage )
			{
				stringManager.ChangeLanguage( ( Languages )( ( LanguageSelectorInputComponent )selectorInput.Input ).CurrentLanguageValue );
			}
		}

		private StateHandler handler;
		private StringManager stringManager;
		public event EventHandler click;
		private GameObject selectorInput;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
	}
}
