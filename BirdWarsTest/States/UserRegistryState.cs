/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Handles drawing and updating of all gameObjects 
necessary for UserRegistryState.
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
	/// necessary for UserRegistryState.
	/// </summary>
	public class UserRegistryState : GameState
	{
		/// <summary>
		/// Creates empty MainMenuState. Sets gamewindow reference and initializes
		/// gameObjects List.
		/// </summary>
		/// <param name="newContent">Game contentManager</param>
		/// <param name="gameWindowIn">Game window reference</param>
		/// <param name="newGraphics">Graphics device reference</param>
		/// <param name="networkManagerIn">Game network manager</param>
		/// <param name="width_in">State width</param>
		/// <param name="height_in">State height</param>
		public UserRegistryState( Microsoft.Xna.Framework.Content.ContentManager newContent,
								  GameWindow gameWindowIn, ref GraphicsDeviceManager newGraphics,
								  ref INetworkManager networkManagerIn, int width_in, int height_in )
			:
			base( newContent, ref newGraphics, ref networkManagerIn, width_in, height_in )
		{
			gameWindow = gameWindowIn;
			GameObjects = new List< GameObject >();
		}

		/// <summary>
		/// Creates all state gameObjects.
		/// </summary>
		/// <param name="handler">Game state</param>
		/// <param name="stringManager">Game string manager</param>
		public override void Init( StateHandler handler, StringManager stringManager ) 
		{
			isInitialized = true;
			ClearContents();
			GameObjects.Add( new GameObject( new SolidRectGraphicsComponent( Content ), null,
										     Identifiers.Background, new Vector2( 0.0f, 0.0f ) ) );
			GameObjects.Add( new GameObject( new RegisterBoxGraphicsComponent( Content ), null,
										     Identifiers.Background, new Vector2( 0.0f, 0.0f ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.Registration ),
																		"Fonts/BabeFont_17" ),
											 null, Identifiers.TextGraphics, stateWidth, GameObjects[ 1 ].Position.Y + 50 ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2", 
																		  stringManager.GetString( StringNames.RegisterWR ) ),
											 new RegisterButtonInputComponent( handler ), Identifiers.Button1, 
											 new Vector2( 70.0f, 425.0f ) ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2", 
																		  stringManager.GetString( StringNames.Cancel ) ),
											 new ButtonChangeStateInputComponent( handler, StateTypes.LoginState ),
										     Identifiers.Button1, new Vector2( 220.0f, 425.0f ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.Name ), 
																		"Fonts/BabeFont_10" ), 
											 null, Identifiers.TextArea, stateWidth, GameObjects[ 2 ].Position.Y + 35 ) );
			GameObjects.Add( new GameObject( new TextAreaGraphicsComponent( Content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
										     Identifiers.TextArea, stateWidth,
										    ( GameObjects[ 5 ].Position.Y + 20 ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.LastName ), 
																		"Fonts/BabeFont_10" ), 
											 null, Identifiers.TextArea, stateWidth, GameObjects[ 6 ].Position.Y + 35 ) );
			GameObjects.Add( new GameObject( new TextAreaGraphicsComponent( Content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
										     Identifiers.TextArea, stateWidth,
										     ( GameObjects[ 7 ].Position.Y + 20 ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.Username ), 
																		"Fonts/BabeFont_10" ), 
											 null, Identifiers.TextArea, stateWidth, GameObjects[ 8 ].Position.Y + 35 ) );
			GameObjects.Add( new GameObject( new TextAreaGraphicsComponent( Content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
										     Identifiers.TextArea, stateWidth,
										     ( GameObjects[ 9 ].Position.Y + 20 ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.Email ), 
																		"Fonts/BabeFont_10" ), 
											 null, Identifiers.TextArea, stateWidth, GameObjects[ 10 ].Position.Y + 35 ) );
			GameObjects.Add( new GameObject( new TextAreaGraphicsComponent( Content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
									   	     Identifiers.TextArea, stateWidth,
										     ( GameObjects[ 11 ].Position.Y + 20 ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.Password ), 
																		"Fonts/BabeFont_10" ), 
											 null, Identifiers.TextArea, stateWidth, GameObjects[ 12 ].Position.Y + 35 ) );
			GameObjects.Add( new GameObject( new PasswordAreaGraphicsComponent( Content ),
											 new TextAreaInputComponent( gameWindow ),
										     Identifiers.TextArea, stateWidth,
										     ( GameObjects[ 13 ].Position.Y + 20 ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.ConfirmPass ), 
																		"Fonts/BabeFont_10" ), 
											 null, Identifiers.TextArea, stateWidth, GameObjects[ 14 ].Position.Y + 35 ) );
			GameObjects.Add( new GameObject( new PasswordAreaGraphicsComponent( Content ),
											 new TextAreaInputComponent( gameWindow ),
										     Identifiers.TextArea, stateWidth,
										     ( GameObjects[ 15 ].Position.Y + 20 ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, Color.Red, "", "Fonts/BabeFont_8" ), null,
											 Identifiers.TextGraphics, stateWidth, GameObjects[ 16 ].Position.Y + 30 ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, Color.Blue, "", "Fonts/BabeFont_8" ), null,
											 Identifiers.TextGraphics, stateWidth, GameObjects[ 16 ].Position.Y + 30 ) );
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
		/// <param name="batch">Game Spritebatch</param>
		public override void Render( ref SpriteBatch batch ) 
		{
			foreach( var objects in GameObjects )
				objects.Render( ref batch );
		}

		/// <summary>
		/// Sets the error message on the error message object.
		/// </summary>
		/// <param name="errorMessage">Error message</param>
		public override void SetErrorMessage( string errorMessage )
		{
			GameObjects[ 18 ].Graphics.ClearText();
			( ( TextGraphicsComponent )GameObjects[ 17 ].Graphics ).SetText( errorMessage );
			GameObjects[ 17 ].RecenterXWidth( stateWidth );
		}

		/// <summary>
		/// Sets the message on the message object.
		/// </summary>
		/// <param name="message">The message</param>
		public override void SetMessage( string message )
		{
			GameObjects[ 17 ].Graphics.ClearText();
			( ( TextGraphicsComponent )GameObjects[ 18 ].Graphics ).SetText( message );
			GameObjects[ 18 ].RecenterXWidth( stateWidth );
		}

		/// <summary>
		/// Clear the text aras un objects at indices 6, 8, 10, 12, 14
		/// and 16.
		/// </summary>
		public override void ClearTextAreas()
		{
			GameObjects[ 6 ].Input.ClearText();
			GameObjects[ 8 ].Input.ClearText();
			GameObjects[ 10 ].Input.ClearText();
			GameObjects[ 12 ].Input.ClearText();
			GameObjects[ 14 ].Input.ClearText();
			GameObjects[ 16 ].Input.ClearText();
		}

		///<value>The list of state gameObjects</value>
		public List<GameObject> GameObjects { get; set; }

		private GameWindow gameWindow;

		///<value>Bool indicating if the state has been initialized.</value>
		public bool IsInitialized
		{
			get { return isInitialized; }
			private set {}
		}
	}
}