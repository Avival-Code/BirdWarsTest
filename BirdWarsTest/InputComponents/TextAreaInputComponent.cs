using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
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
			Text = "";
			timer = 0;
		}

		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state )
		{
			MouseState mouseState = Mouse.GetState();
			var clicked = mouseState.LeftButton == ButtonState.Pressed;
			CheckClick( mouseState.Position, clicked, gameObject );
			AdvanceTimer();
		}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState ) 
		{
			HandleInput( gameObject, state );
		}

		private void CheckClick( Point mouseClick, bool clicked, GameObject gameObject )
		{
			if( gameObject.GetRectangle().Contains( mouseClick ) && clicked && !timerIsOn )
			{
				StartTimer();
				hasFocus = !hasFocus;
				if ( hasFocus )
					SetTextEventHandler( OnInput );
				else
					RemoveTextEventHandler( OnInput );
			}
		}

		private void StartTimer()
		{
			timerIsOn = true;
		}

		private void AdvanceTimer()
		{
			if( timerIsOn )
			{
				timer += 1;
				ResetTimer();
			}
		}

		private void ResetTimer()
		{
			if( timer == 25 )
			{
				timer = 0;
				timerIsOn = false;
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

		public void AddCharacter( char newChar )
		{
			if( Text.Length < maxCharacters )
			{
				Text += newChar;
			}
		}

		public void RemoveCharacter()
		{
			if( Text.Length >= 1 )
			Text = Text.Remove( Text.Length - 1, 1 );
		}

		public override string GetText()
		{
			return Text;
		}

		public override void ClearText()
		{
			Text = "";
		}

		public string Text { get; set; }

		private GameWindow gameWindow;
		private bool hasFocus;
		private bool timerIsOn;
		private const int maxCharacters = 40;
		private int timer;
	}
}