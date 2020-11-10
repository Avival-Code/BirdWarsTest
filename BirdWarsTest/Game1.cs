using BirdWarsTest.States;
using BirdWarsTest.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Lidgren.Network;
using System;

namespace BirdWarsTest
{
	public class Game1 : Game
	{
		public Game1( INetworkManager networkManagerIn )
		{
			_graphics = new GraphicsDeviceManager( this );
			Content.RootDirectory = "Content";
			stateHandler = new StateHandler( Content, Window, ref _graphics );
			networkManager = networkManagerIn;
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			Window.Title = "Bird Wars";
			networkManager.Connect();
			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch( GraphicsDevice );
			stateHandler.InitializeStates();
		}

		protected override void Update( GameTime gameTime )
		{
			if ( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed || 
				 Keyboard.GetState().IsKeyDown( Keys.Escape ) )
				Exit();

			ProcessNetworkMessages();

			stateHandler.GetCurrentState().UpdateLogic( Keyboard.GetState() );

			base.Update( gameTime );
		}

		protected override void Draw( GameTime gameTime )
		{
			GraphicsDevice.Clear( Color.CornflowerBlue );

			_spriteBatch.Begin();

			stateHandler.GetCurrentState().Render( ref _spriteBatch );

			_spriteBatch.End();

			base.Draw( gameTime );
		}

		public void ProcessNetworkMessages()
		{
			NetIncomingMessage incomingMessage;
			while ( ( incomingMessage = networkManager.ReadMessage() ) != null )
			{
				switch ( incomingMessage.MessageType )
				{
					case NetIncomingMessageType.VerboseDebugMessage:
					case NetIncomingMessageType.DebugMessage:
					case NetIncomingMessageType.WarningMessage:
					case NetIncomingMessageType.ErrorMessage:
						Console.WriteLine( incomingMessage.ReadString() );
						break;
					case NetIncomingMessageType.StatusChanged:
						switch ( ( NetConnectionStatus )incomingMessage.ReadByte() )
						{
							case NetConnectionStatus.Connected:
								Console.WriteLine( "{0} Connected", incomingMessage.SenderEndPoint );
								break;
							case NetConnectionStatus.Disconnected:
								Console.WriteLine( "{0} Disconnected", incomingMessage.SenderEndPoint );
								break;
							case NetConnectionStatus.RespondedAwaitingApproval:
								incomingMessage.SenderConnection.Approve();
								break;
						}
						break;
				}
				networkManager.Recycle( incomingMessage );
			}
		}

		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private StateHandler stateHandler;
		private INetworkManager networkManager;
	}
}