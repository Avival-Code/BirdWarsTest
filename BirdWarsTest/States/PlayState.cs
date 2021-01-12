/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Handles drawing and updating of all gameObjects 
necessary for PlayState.
*********************************************/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.Network;
using BirdWarsTest.GameObjects.ObjectManagers;
using BirdWarsTest.GameRounds;
using BirdWarsTest.Utilities;

namespace BirdWarsTest.States
{
	/// <summary>
	/// Handles drawing and updating of all gameObjects 
	///necessary for PlayState.
	/// </summary>
	public class PlayState : GameState
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
		public PlayState( Microsoft.Xna.Framework.Content.ContentManager newContent,
						  ref GraphicsDeviceManager newGraphics, ref INetworkManager networkManagerIn, 
						  int width_in, int height_in )
			:
			base( newContent, ref newGraphics, ref networkManagerIn, width_in, height_in )
		{
			DisplayManager = new HeadsUpDisplayManager();
			PlayerManager = new PlayerManager();
			mapManager = new MapManager();
			ItemManager = new ItemManager( Content );
			camera = new Camera2D();
			sentEndRoundMessage = false;
		}

		/// <summary>
		/// Creates all state gameObjects.
		/// </summary>
		/// <param name="handler">Game state</param>
		/// <param name="stringManager">Game string manager</param>
		public override void Init( StateHandler handler, StringManager stringManager ) 
		{
			ClearContents();
			DisplayManager.InitializeInterfaceComponents( Content, handler );
			mapManager.InitializeMapTiles( Content );
			ItemManager.SetMapBounds( mapManager.GetMapBounds() );
		}

		/// <summary>
		/// Removes all gameObjects from state list.
		/// </summary>
		public override void ClearContents() 
		{
			mapManager.ClearAllTiles();
			PlayerManager.ClearAllPlayers();
			DisplayManager.ClearAllDisplayObjects();
			ItemManager.ClearAllItems();
			camera.ResetCamera();
			sentEndRoundMessage = false;
		}

		/// <summary>
		/// Handles network incoming messages. Updates all gameObjects
		/// in state.
		/// </summary>
		/// <param name="handler">Game statehandler</param>
		/// <param name="state">current keyboard state</param>
		public override void UpdateLogic( StateHandler handler, KeyboardState state ) {}

		/// <summary>
		/// Handles network incoming messages. Updates all gameObjects
		/// in state.
		/// </summary>
		/// <param name="handler">Game statehandler</param>
		/// <param name="state">current keyboard state</param>
		/// <param name="gameTime">GAme time</param>
		public override void UpdateLogic( StateHandler handler, KeyboardState state, GameTime gameTime )
		{
			networkManager.ProcessMessages( handler );
			camera.Update( PlayerManager.GetLocalPlayer().Position, mapManager.GetMapBounds(), 
						   PlayerManager.GetLocalPlayer().GetRectangle(), PlayerManager.CreatedPlayers );
			DisplayManager.Update( gameTime );
			PlayerManager.Update( this, gameTime, state, mapManager.GetMapBounds(), networkManager );
			ItemManager.Update( networkManager, PlayerManager, gameTime, mapManager.GetMapBounds() );
			CheckEndRound();
		}

		/// <summary>
		/// Draws all gameObjects on the screen.
		/// </summary>
		/// <param name="batch">Game Spritebatch</param>
		public override void Render( ref SpriteBatch batch )
		{
			mapManager.Render( ref batch, camera.GetCameraRenderBounds(), camera.GetCameraBounds() );
			PlayerManager.Render( ref batch, camera.GetCameraRenderBounds(), camera.GetCameraBounds() );
			ItemManager.Render( ref batch, camera.GetCameraRenderBounds(), camera.GetCameraBounds() );
			DisplayManager.Render( ref batch, PlayerManager.GetLocalPlayer().Health );
		}

		///<value>Exposes the state's protected network manager.</value>
		public INetworkManager NetWorkManager 
		{
			get { return networkManager; }
		}

		private void CheckEndRound()
		{
			if( networkManager.IsHost() )
			{
				if( ( !sentEndRoundMessage && DisplayManager.IsRoundTimeOver() ) ||
					( !sentEndRoundMessage && PlayerManager.GetNumberOfPlayersStillAlive() == 1 ) )
				{
					sentEndRoundMessage = true;
					networkManager.SendRoundFinishedMessage( DisplayManager.GetRemainingRoundTime() );
				}
			}
		}

		///<value>The state's Player manager.</value>
		public PlayerManager PlayerManager { get; private set; }

		///<value>The state's Item manager.</value>
		public ItemManager ItemManager { get; private set; }

		///<value>The state's Display manager.</value>
		public HeadsUpDisplayManager DisplayManager { get; private set; }
		private readonly MapManager mapManager;
		private readonly Camera2D camera;
		private bool sentEndRoundMessage;
	}
}