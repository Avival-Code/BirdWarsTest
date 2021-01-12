/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Input component that handles the displayed user
input text.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BirdWarsTest.InputComponents
{
	/// <summary>
	/// Input component that handles the displayed user
	/// input text.
	/// </summary>
	public class TextAreaInputComponent : InputComponent
	{
		/// <summary>
		/// Sets the game window reference, and default values.
		/// </summary>
		/// <param name="gameWindowIn"></param>
		public TextAreaInputComponent( GameWindow gameWindowIn )
		{
			gameWindow = gameWindowIn;
			hasFocus = false;
			visualCharacterIsOn = false;
			Text = "";
			clickedBoxTimer = 0;
			visualCharacterTimer = 0;
		}

		/// <summary>
		/// Handles the input recieved based on the current game object state
		/// and game time.
		/// </summary>
		/// <param name="gameObject">Current game object.</param>
		/// <param name="gameTime">Current game time.</param>
		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		/// <summary>
		/// Checks if the user clicked on the objects text area texture and if so,
		/// sets focus on text area and allows user to type. If user clicks outside of
		/// the text area texture bounds and focus is on, it sets focus off.
		/// </summary>
		/// <param name="gameObject">Current game object.</param>
		/// <param name="state">Current keyboard state.</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state )
		{
			MouseState mouseState = Mouse.GetState();
			var clicked = mouseState.LeftButton == ButtonState.Pressed;
			CheckClick( mouseState.Position, clicked, gameObject );
			UpdateTimers();
		}

		/// <summary>
		/// Handles the input recieved based on the current game object state,
		/// current keyboard state and current game state.
		/// </summary>
		/// <param name="gameObject">The Game object</param>
		/// <param name="state">current keyboard state</param>
		/// <param name="gameState">current game state</param>
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

		/// <summary>
		/// Adds character to the user input string.
		/// </summary>
		/// <param name="newChar">The character to be added</param>
		public void AddCharacter( char newChar )
		{
			if( Text.Length < MaxCharacters && IsValidChar( newChar ) )
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

		/// <summary>
		/// Removes a character from the text string.
		/// </summary>
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

		/// <summary>
		/// Returns the user input text with visual characters.
		/// </summary>
		/// <returns>Returns the user input text with visual characters.</returns>
		public override string GetText()
		{
			return Text;
		}

		/// <summary>
		/// Returns the user input text without the visual characters.
		/// </summary>
		/// <returns>Returns the user input text without the visual characters.</returns>
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

		/// <summary>
		/// Clears the user input text.
		/// </summary>
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

		///<value>The user input text.</value>
		public string Text { get; set; }

		private readonly GameWindow gameWindow;
		private const int MaxCharacters = 40;
		private bool hasFocus;
		private bool clickedBox;
		private bool visualCharacterIsOn;
		private int clickedBoxTimer;
		private int visualCharacterTimer;
	}
}