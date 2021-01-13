/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Handles drawing and updating of all gameObjects 
necessary for login.
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
	/// necessary for login.
	/// </summary>
	public class LoginState : GameState
	{
		/// <summary>
		/// Creates empty login state. Sets gamewindow reference and initializes
		/// gameObjects List.
		/// </summary>
		/// <param name="newContent">Game contentManager</param>
		/// <param name="gameWindowIn">Game window</param>
		/// <param name="newGraphics">Graphics device reference</param>
		/// <param name="networkManagerIn">Game network manager</param>
		/// <param name="width_in">State width</param>
		/// <param name="height_in">State height</param>
		public LoginState( Microsoft.Xna.Framework.Content.ContentManager newContent,
						   GameWindow gameWindowIn, ref GraphicsDeviceManager newGraphics, 
						   ref INetworkManager networkManagerIn,
						   int width_in, int height_in ) 
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
			IsInitialized = true;
			ClearContents();
			GameObjects.Add( new GameObject( new SolidRectGraphicsComponent( Content ), null,
										     Identifiers.Background, new Vector2( 0.0f, 0.0f ) ) );
			GameObjects.Add( new GameObject( new LoginLogoGraphicsComponent( Content ), null, 
								             Identifiers.LoginLogo, stateWidth, 15 ) );
			GameObjects.Add( new GameObject( new LoginBoxGraphicsComponent( Content ), null, 
								             Identifiers.LoginBox1, stateWidth, 195 ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button", 
																		  stringManager.GetString( StringNames.Login ) ), 
											 new LoginButtonInputComponent( handler ), Identifiers.Button1, new Vector2( 
									   	     ( GameObjects[ 2 ].Position.X + 40 ), 
										     ( GameObjects[ 2 ].Position.Y + 155 ) ) ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button", 
																		  stringManager.GetString( StringNames.RegisterL ) ), 
											 new ButtonChangeStateInputComponent( handler, StateTypes.UserRegistryState ),
										     Identifiers.Button1, new Vector2(
										     ( GameObjects[ 2 ].Position.X + 160 ),
										     ( GameObjects[ 2 ].Position.Y + 155 ) ) ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2", 
																		  stringManager.GetString( StringNames.LostPassword ) ),
										     new ButtonChangeStateInputComponent( handler, StateTypes.PasswordRecoveryState ),
										     Identifiers.Button2, stateWidth,
										     GameObjects[ 2 ].Position.Y + 190 ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.Email ),
																	    "Fonts/BabeFont_10" ), null, 
										      Identifiers.TextArea, stateWidth, GameObjects[ 2 ].Position.Y + 30 ) );
			GameObjects.Add( new GameObject( new TextAreaGraphicsComponent( Content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
										     Identifiers.TextArea, stateWidth,
										     ( GameObjects[ 6 ].Position.Y + 20 ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.Password ), 
																		"Fonts/BabeFont_10"), null,
										      Identifiers.TextGraphics, stateWidth, GameObjects[ 2 ].Position.Y + 90 ) );
			GameObjects.Add( new GameObject( new PasswordAreaGraphicsComponent( Content ),
											 new TextAreaInputComponent( gameWindow ),  Identifiers.PasswordArea, 
											 stateWidth, ( GameObjects[ 8 ].Position.Y + 20 ) ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Buttons/Configuration", "" ),
											 new ButtonChangeStateInputComponent( handler, StateTypes.OptionsState ),
											 Identifiers.Button2, 
											 new Vector2( GameObjects[ 5 ].Position.X + 145, GameObjects[ 5 ].Position.Y ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, Color.Red ,"", "Fonts/BabeFont_8" ), null, 
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

			foreach( var objects in GameObjects  )
				objects.Update( state, this );
		}

		/// <summary>
		/// Handles network incoming messages. Updates all gameObjects
		/// in state.
		/// </summary>
		/// <param name="handler">Game statehandler</param>
		/// <param name="state">current keyboard state</param>
		/// <param name="gameTime">GAme time</param>
		public override void UpdateLogic(StateHandler handler, KeyboardState state, GameTime gameTime)
		{
			UpdateLogic( handler, state );
		}

		/// <summary>
		/// Draws all gameObjects on the screen.
		/// </summary>
		/// <param name="sprites">Game Spritebatch</param>
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
			( ( TextGraphicsComponent )GameObjects[ 11 ].Graphics ).SetText( errorMessage );
			GameObjects[ 11 ].RecenterXWidth( stateWidth );
		}

		///<value>List of all game objects in state.</value>
		public List< GameObject > GameObjects { get; set; }

		private GameWindow gameWindow;

		public bool IsInitialized 
		{
			get { return isInitialized; }
			private set {}
		}
	}
}