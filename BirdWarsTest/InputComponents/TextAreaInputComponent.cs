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
			visualCharacterIsOn = false;
			Text = "";
			clickedBoxTimer = 0;
			visualCharacterTimer = 0;
		}

		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state )
		{
			MouseState mouseState = Mouse.GetState();
			var clicked = mouseState.LeftButton == ButtonState.Pressed;
			CheckClick( mouseState.Position, clicked, gameObject );
			UpdateTimers();
		}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState ) 
		{
			HandleInput( gameObject, state );
		}

		private void CheckClick( Point mouseClick, bool clicked, GameObject gameObject )
		{
			if( gameObject.GetRectangle().Contains( mouseClick ) && clicked && !clickedBox )
			{
				StartClickedTimer();
				if( !hasFocus )
				{
					hasFocus = !hasFocus;
					SetTextEventHandler( OnInput );
				}
			}

			if( !clickedBox && clicked && !gameObject.GetRectangle().Contains( mouseClick ) )
			{
				StartClickedTimer();
				if( hasFocus )
				{
					if( visualCharacterIsOn )
					{
						RemoveVisualCharacter();
					}
					hasFocus = !hasFocus;
					RemoveTextEventHandler( OnInput );
				}
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

		private void StartClickedTimer()
		{
			clickedBox = true;
		}

		private void UpdateTimers()
		{
			if( clickedBox )
			{
				clickedBoxTimer += 1;
				ResetClickedTimer();
			}

			if( hasFocus )
			{
				visualCharacterTimer += 1;
				ResetVisualCharacterTimer();
			}
		}

		private void ResetClickedTimer()
		{
			if( clickedBoxTimer >= 25 )
			{
				clickedBoxTimer = 0;
				clickedBox = false;
			}
		}

		private void ResetVisualCharacterTimer()
		{
			if( visualCharacterTimer >= 40 && !visualCharacterIsOn )
			{
				AddVisualCharacter();
			}

			if( visualCharacterTimer >= 40 && visualCharacterIsOn )
			{
				RemoveVisualCharacter();
			}
		}

		private void AddVisualCharacter()
		{
			visualCharacterIsOn = true;
			AddVisualQeueCharacter();
			visualCharacterTimer = 0;
		}

		private void RemoveVisualCharacter()
		{
			visualCharacterIsOn = false;
			Text = Text.Remove( Text.Length - 1, 1 );
			visualCharacterTimer = 0;
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
			if( Text.Length < maxCharacters && IsValidChar( newChar ) )
			{
				if( visualCharacterIsOn )
				{
					Text = Text.Remove( Text.Length - 1, 1 );
					Text += newChar;
					AddVisualQeueCharacter();
				}
				else
				{
					Text += newChar;
				}
			}
		}

		private void AddVisualQeueCharacter()
		{
			if( hasFocus )
			{
				Text += '/';
			}
		}

		public void RemoveCharacter()
		{
			if( Text.Length >= 1 )
			{
				if( visualCharacterIsOn && Text.Length >= 2 )
				{
					Text = Text.Remove( Text.Length - 2, 1 );
				}
				
				if( !visualCharacterIsOn )
				{
					Text = Text.Remove( Text.Length - 1, 1 );
				}
			}
		}

		private bool IsValidChar( char character )
		{
			if( ( character >= 32 && character <= 37 ) ||
				( character >= 39 && character <= 46 ) ||
				( character >= 48 && character <= 125 ) )
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public override string GetText()
		{
			return Text;
		}

		public override string GetTextWithoutVisualCharacter()
		{
			if( visualCharacterIsOn )
			{
				return Text.Substring( 0, Text.Length - 1 ); 
			}
			else
			{
				return Text;
			}
		}

		public override void ClearText()
		{
			if( visualCharacterIsOn )
			{
				Text = "/";
			}	
			else
			{
				Text = "";
			}
		}

		public string Text { get; set; }

		private GameWindow gameWindow;
		private const int maxCharacters = 40;
		private bool hasFocus;
		private bool clickedBox;
		private bool visualCharacterIsOn;
		private int clickedBoxTimer;
		private int visualCharacterTimer;
	}
}