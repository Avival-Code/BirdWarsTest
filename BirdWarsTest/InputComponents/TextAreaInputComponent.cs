using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BirdWarsTest.InputComponents
{
	class TextAreaInputComponent : InputComponent
	{
		public TextAreaInputComponent( GameWindow gameWindowIn )
		{
			gameWindow = gameWindowIn;
			hasFocus = false;
			text = "";
		}

		public override void HandleInput( GameObject gameObject, KeyboardState state )
		{
			MouseState mouseState = Mouse.GetState();
			var clicked = mouseState.LeftButton == ButtonState.Pressed;
			CheckClick( mouseState.Position, clicked, gameObject );
		}

		private void CheckClick( Point mouseClick, bool clicked, GameObject gameObject )
		{
			if( gameObject.GetRectangle().Contains( mouseClick ) && clicked )
			{
				hasFocus = !hasFocus;
				if ( hasFocus )
					SetTextEventHandler( OnInput );
				else
					RemoveTextEventHandler( OnInput );
			}
		}

		private void SetTextEventHandler( System.EventHandler< TextInputEventArgs > method )
		{
			gameWindow.TextInput += OnInput;
		}

		private void RemoveTextEventHandler( System.EventHandler< TextInputEventArgs > method )
		{
			gameWindow.TextInput -= OnInput;
		}

		private void OnInput( Object sender, TextInputEventArgs e )
		{
			var k = e.Key;
			var character = e.Character;
			if ( ( int )character == 8 )
				RemoveCharacter();
			else
				AddCharacter( character );
		}

		public void AddCharacter(char newChar)
		{
			text += newChar;
		}

		public void RemoveCharacter()
		{
			if( text.Length >= 1 )
			text = text.Remove(text.Length - 1, 1);
		}

		public override string GetText()
		{
			return text;
		}

		private GameWindow gameWindow;
		private bool hasFocus;
		public string text;
	}
}
