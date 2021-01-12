/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Input component starts the process to change the applications
current confuiguration when clicked on.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using BirdWarsTest.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BirdWarsTest.InputComponents
{
	/// <summary>
	/// Input component starts the process to change the applications
	/// current confuiguration when clicked on.
	/// </summary>
	public class ButtonChangeConfigurationInputComponent : InputComponent
	{
		/// <summary>
		/// Sets the selector Object.
		/// </summary>
		/// <param name="handlerIn"></param>
		/// <param name="selectorInputIn"></param>
		/// <param name="stringManagerIn"></param>
		public ButtonChangeConfigurationInputComponent( StateHandler handlerIn, GameObject selectorInputIn, 
														StringManager stringManagerIn )
		{
			handler = handlerIn;
			selectorInput = selectorInputIn;
			stringManager = stringManagerIn;
			Click += ToOtherScreen;
		}

		/// <summary>
		/// Activates the change process when clicked on.
		/// </summary>
		/// <param name="gameObject">The gameObject</param>
		/// <param name="state">current keyboard state</param>
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
					Click?.Invoke( this, new EventArgs() );
				}
			}
		}

		/// <summary>
		/// Handles the current input on every frame.
		/// </summary>
		/// <param name="gameObject">the GameObject</param>
		/// <param name="gameTime">Game time</param>
		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		/// <summary>
		/// Handles the current input on every frame.
		/// </summary>
		/// <param name="gameObject">the GameObject</param>
		/// <param name="state">current keyboard state</param>
		/// <param name="gameState">current game state</param>
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

		private readonly StateHandler handler;
		private readonly StringManager stringManager;
		private event EventHandler Click;
		private readonly GameObject selectorInput;
		private MouseState currentMouseState;
		private MouseState previousMouseState;
	}
}