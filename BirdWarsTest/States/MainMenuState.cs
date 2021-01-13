/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Handles drawing and updating of all gameObjects 
necessary for Main menu state.
*********************************************/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.Network;
using BirdWarsTest.GameObjects;
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.InputComponents;
using BirdWarsTest.Utilities;
using System.Collections.Generic;

namespace BirdWarsTest.States
{
	/// <summary>
	/// Handles drawing and updating of all gameObjects 
	/// necessary for Main menu state.
	/// </summary>
	public class MainMenuState : GameState
	{
		/// <summary>
		/// Creates empty MainMenuState. Sets gamewindow reference and initializes
		/// gameObjects List.
		/// </summary>
		/// <param name="newContent">Game contentManager</param>
		/// <param name="newGraphics">Graphics device reference</param>
		/// <param name="networkManagerIn">Game network manager</param>
		/// <param name="width_in">State width</param>
		/// <param name="height_in">State height</param>
		public MainMenuState( Microsoft.Xna.Framework.Content.ContentManager newContent,
							  ref GraphicsDeviceManager newGraphics, ref INetworkManager networkManagerIn,
							  int width_in, int height_in )
			:
			base( newContent, ref newGraphics, ref networkManagerIn, width_in, height_in )
		{
			gameObjects = new List< GameObject >();
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
			gameObjects.Add( new GameObject( new SolidRectGraphicsComponent( Content ), null, Identifiers.Background,
											 new Vector2( 0.0f, 0.0f ) ) );
			gameObjects.Add( new GameObject( new DecorationGraphicsComponent( Content, "Decorations/MainMenuBar800x110" ),
											 null, Identifiers.Decoration, new Vector2( 0.0f, 60.0f ) ) );
			gameObjects.Add( new GameObject( new DecorationGraphicsComponent( Content, "Logos/BirdWarsLogo_440x246" ),
											 null, Identifiers.Decoration, new Vector2( 0.0f, 20.0f ) ) );
			InitializeMenuOptions( handler, stringManager );
		}

		/// <summary>
		/// Removes all gameObjects from state list.
		/// </summary>
		public override void ClearContents()
		{
			gameObjects.Clear();
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
			gameObjects[ 7 ].Update( state, this );
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
			foreach( var objects in gameObjects )
			{
				objects.Render( ref sprites );
			}
		}

		private void InitializeMenuOptions( StateHandler handler, StringManager stringManager )
		{
			if( networkManager.IsHost() )
			{
				InitializeHostMenuOptions( handler, stringManager );
			}
			else
			{
				InitializeClientMenuOptions( handler, stringManager );
			}
		}

		private void InitializeHostMenuOptions( StateHandler handler, StringManager stringManager )
		{
			gameObjects.Add( new GameObject( new MenuOptionGraphicsComponent( Content, stringManager.GetString( StringNames.CreateGame ) ),
											 new CreateLobbyInputComponent(), Identifiers.MenuOption,
											 new Vector2( 0.0f, gameObjects[ 2 ].Position.Y + 270.0f ) ) );
			gameObjects.Add( new GameObject( new MenuOptionGraphicsComponent( Content, stringManager.GetString( StringNames.Statistics ) ),
											 new SelectorChangeStateInputComponent( handler, StateTypes.StatisticsState ),
											 Identifiers.MenuOption, new Vector2( 0.0f, gameObjects[ 3 ].Position.Y + 60.0f ) ) );
			gameObjects.Add( new GameObject( new MenuOptionGraphicsComponent( Content, stringManager.GetString( StringNames.Settings ) ),
											 new SelectorChangeStateInputComponent( handler, StateTypes.OptionsState ),
											 Identifiers.MenuOption, new Vector2( 0.0f, gameObjects[ 4 ].Position.Y + 60.0f ) ) );
			gameObjects.Add( new GameObject( new MenuOptionGraphicsComponent(Content, stringManager.GetString( StringNames.Logout ) ),
											 new LogoutInputComponent( handler ), Identifiers.MenuOption,
											 new Vector2( 0.0f, gameObjects[ 5 ].Position.Y + 60.0f ) ) );
			gameObjects.Add( new GameObject( null, new SelectorInputComponent( GetMenuOptions() ), Identifiers.Selector,
											 new Vector2( 0.0f, 0.0f ) ) );
		}

		private void InitializeClientMenuOptions( StateHandler handler, StringManager stringManager )
		{
			gameObjects.Add( new GameObject( new MenuOptionGraphicsComponent( Content, stringManager.GetString( StringNames.FindGame ) ),
											 new FindGameInputComponent(), Identifiers.MenuOption,
											 new Vector2( 0.0f, gameObjects[ 2 ].Position.Y + 270.0f ) ) );
			gameObjects.Add( new GameObject( new MenuOptionGraphicsComponent( Content, stringManager.GetString( StringNames.Statistics ) ),
											 new SelectorChangeStateInputComponent( handler, StateTypes.StatisticsState ),
											 Identifiers.MenuOption, new Vector2( 0.0f, gameObjects[ 3 ].Position.Y + 60.0f ) ) );
			gameObjects.Add( new GameObject( new MenuOptionGraphicsComponent( Content, stringManager.GetString( StringNames.Settings ) ),
											 new SelectorChangeStateInputComponent( handler, StateTypes.OptionsState ),
											 Identifiers.MenuOption, new Vector2( 0.0f, gameObjects[ 4 ].Position.Y + 60.0f ) ) );
			gameObjects.Add( new GameObject( new MenuOptionGraphicsComponent( Content, stringManager.GetString( StringNames.Logout ) ),
											 new LogoutInputComponent( handler ), Identifiers.MenuOption,
											 new Vector2( 0.0f, gameObjects[ 5 ].Position.Y + 60.0f ) ) );
			gameObjects.Add( new GameObject( null, new SelectorInputComponent( GetMenuOptions() ), Identifiers.Selector,
											 new Vector2( 0.0f, 0.0f ) ) );
		}

		private List< GameObject > GetMenuOptions()
		{
			List<GameObject> menuOptions = new List< GameObject >();
			foreach( var objects in gameObjects )
			{
				if( objects.Identifier == Identifiers.MenuOption )
				{
					menuOptions.Add( objects );
				}
			}
			return menuOptions;
		}

		private List<GameObject> gameObjects;
		/// <summary>
		/// Exposes the state's protected network manager.
		/// </summary>
		public INetworkManager NetworkManager
		{
			get{ return networkManager; }
		}
	}
}