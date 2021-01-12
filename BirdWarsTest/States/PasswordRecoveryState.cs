/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Handles drawing and updating of all gameObjects 
necessary for PasswordRecovery State.
*********************************************/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.GameObjects;
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.InputComponents;
using BirdWarsTest.Network;
using BirdWarsTest.Utilities;
using System.Collections.Generic;

namespace BirdWarsTest.States
{
	/// <summary>
	/// Handles drawing and updating of all gameObjects 
	/// necessary for PasswordRecovery State.
	/// </summary>
	public class PasswordRecoveryState : GameState
	{
		/// <summary>
		/// Creates empty MainMenuState. Sets gamewindow reference and initializes
		/// gameObjects List.
		/// </summary>
		/// <param name="newContent">Game contentManager</param>
		/// <param name="gameWindowIn">Game window</param>
		/// <param name="newGraphics">Graphics device reference</param>
		/// <param name="networkManagerIn">Game network manager</param>
		/// <param name="width_in">State width</param>
		/// <param name="height_in">State height</param>
		public PasswordRecoveryState( Microsoft.Xna.Framework.Content.ContentManager newContent,
						   GameWindow gameWindowIn, ref GraphicsDeviceManager newGraphics,
						   ref INetworkManager networkManagerIn, int width_in, int height_in )
			:
			base( newContent, ref newGraphics, ref networkManagerIn, width_in, height_in )
		{
			GameObjects = new List< GameObject >();
			gameWindow = gameWindowIn;
		}

		/// <summary>
		/// Creates all state gameObjects.
		/// </summary>
		/// <param name="handler">Game state</param>
		/// <param name="stringManager">Game string manager</param>
		public override void Init( StateHandler handler, StringManager stringManager ) 
		{
			ClearContents();
			GameObjects.Add( new GameObject( new SolidRectGraphicsComponent( Content ), null,
										 Identifiers.Background, new Vector2( 0.0f, 0.0f ) ) );
			GameObjects.Add( new GameObject( new MenuBoxGraphicsComponent( Content ), null,
										 Identifiers.PasswordBox, new Vector2( 0.0f, 0.0f ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.RecoverPass ), 
																		"Fonts/BabeFont_17" ),
							                 null, Identifiers.TextGraphics, stateWidth, 50.0f ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.Email ), 
																		"Fonts/BabeFont_10" ),
											 null, Identifiers.TextGraphics, stateWidth, 
											 GameObjects[ 2 ].Position.Y + 50.0f ) );
			GameObjects.Add( new GameObject( new TextAreaGraphicsComponent( Content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
										     Identifiers.TextArea, stateWidth,
										     GameObjects[ 3 ].Position.Y + 20 ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2", 
																		  stringManager.GetString( StringNames.SendCode ) ),
											 new SolicitPasswordResetInputComponent( handler ), 
											 Identifiers.Button1, stateWidth, 
										     GameObjects[ 4 ].Position.Y + 40.0f ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.EnterCode ), 
																		"Fonts/BabeFont_10" ),
											 null, Identifiers.TextGraphics, stateWidth,
											 GameObjects[ 5 ].Position.Y + 60.0f ) );
			GameObjects.Add( new GameObject( new TextAreaGraphicsComponent( Content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
											 Identifiers.TextArea, stateWidth,
											 GameObjects[ 6 ].Position.Y + 20 ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.NewPassword ), 
																		"Fonts/BabeFont_10" ),
											 null, Identifiers.TextGraphics, stateWidth,
											 GameObjects[ 7 ].Position.Y + 35.0f ) );
			GameObjects.Add( new GameObject( new TextAreaGraphicsComponent( Content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
											 Identifiers.TextArea, stateWidth,
											 GameObjects[ 8 ].Position.Y + 20 ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2", 
																		  stringManager.GetString( StringNames.Reset ) ),
											 new ResetPasswordInputComponent( handler ), 
											 Identifiers.Button1, 
											 new Vector2( GameObjects[ 9 ].Position.X - 30,
														  GameObjects[ 9 ].Position.Y + 50.0f ) ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2", 
																		  stringManager.GetString( StringNames.Cancel ) ),
											 new ButtonChangeStateInputComponent( handler, StateTypes.LoginState ),
										     Identifiers.Button1, 
											 new Vector2( GameObjects[ 10 ].Position.X + 160.0f,
												          GameObjects[ 9 ].Position.Y + 50.0f ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, Color.Red, "", "Fonts/BabeFont_8" ), null,
											 Identifiers.TextGraphics, stateWidth, GameObjects[ 9 ].Position.Y + 30 ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, Color.Blue, "", "Fonts/BabeFont_8" ), null,
											 Identifiers.TextGraphics, stateWidth, GameObjects[ 9 ].Position.Y + 30 ) );
		}

		/// <summary>
		/// Removes all gameObjects from state list.
		/// </summary>
		public override void ClearContents()
		{
			GameObjects.Clear();
		}

		/// <summary>
		/// Handles network incoming messages. Updates all gameObjects
		/// in state.
		/// </summary>
		/// <param name="handler">Game statehandler</param>
		/// <param name="state">current keyboard state</param>
		public override void UpdateLogic( StateHandler handler, KeyboardState state ) 
		{
			networkManager.ProcessMessages( handler );
			foreach( var objects in GameObjects )
				objects.Update( state, this );
		}

		/// <summary>
		/// Handles network incoming messages. Updates all gameObjects
		/// in state.
		/// </summary>
		/// <param name="handler">Game statehandler</param>
		/// <param name="state">current keyboard state</param>
		/// <param name="gameTime">GAme time</param>
		public override void UpdateLogic( StateHandler handler, KeyboardState state, GameTime gameTime )
		{
			UpdateLogic( handler, state );
		}

		/// <summary>
		/// Draws all gameObjects on the screen.
		/// </summary>
		/// <param name="sprites"></param>
		public override void Render( ref SpriteBatch sprites ) 
		{
			foreach( var objects in GameObjects )
				objects.Render( ref sprites );
		}

		/// <summary>
		/// Sets the error message on the error message object.
		/// </summary>
		/// <param name="errorMessage">Error message</param>
		public override void SetErrorMessage( string errorMessage )
		{
			GameObjects[ 13 ].Graphics.ClearText();
			GameObjects[ 12 ].Graphics.SetText( errorMessage );
			GameObjects[ 12 ].RecenterXWidth( stateWidth );
		}

		/// <summary>
		/// Sets the message on the message object.
		/// </summary>
		/// <param name="message">The message</param>
		public override void SetMessage( string message )
		{
			GameObjects[ 12 ].Graphics.ClearText();
			GameObjects[ 13 ].Graphics.SetText( message );
			GameObjects[ 13 ].RecenterXWidth( stateWidth );
		}

		/// <summary>
		/// Clears the text areas in objects 4, 7 and 9 of GameObjects list.
		/// </summary>
		public override void ClearTextAreas()
		{
			GameObjects[ 4 ].Graphics.ClearText();
			GameObjects[ 7 ].Graphics.ClearText();
			GameObjects[ 9 ].Graphics.ClearText();
		}

		///<value>The list of state gameObjects</value>
		public List<GameObject> GameObjects { get; set; }

		private GameWindow gameWindow;
	}
}