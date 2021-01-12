/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Graphics component for a chat message object.
*********************************************/
using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	/// <summary>
	/// Graphics component for a chat message object.
	/// </summary>
	public class ChatMessageGraphicsComponent : GraphicsComponent
	{
		/// <summary>
		/// Creates an instance of the graphcis component.
		/// </summary>
		/// <param name="content">Game manager</param>
		/// <param name="textureName">Texture name</param>
		/// <param name="usernameIn">player username</param>
		/// <param name="messageIn">Message body</param>
		/// <param name="isFromOtherUserIn">bool indicating if message is from a remote user.</param>
		public ChatMessageGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content,
											 string textureName, string usernameIn, string messageIn,
											 bool isFromOtherUserIn )
			:
			base( content.Load< Texture2D >( textureName ) )
		{
			normalFont = content.Load< SpriteFont >( "Fonts/MainFont_S10" );
			textColor = Color.Black;
			username = usernameIn;
			message = messageIn;
			isFromOtherUser = isFromOtherUserIn;
		}

		/// <summary>
		/// Draws the texture to the screen at the object's position.
		/// </summary>
		/// <param name="gameObject">The game object</param>
		/// <param name="batch">Game Spritebatch</param>
		public override void Render( GameObject gameObject, ref SpriteBatch batch )
		{
			batch.Draw( texture, gameObject.Position, Color.White );
			if( !isFromOtherUser )
			{
				AlignLeftRender( gameObject, ref batch );
			}
			else 
			{
				AlignRightRender( gameObject, ref batch );
			}
		}

		private void AlignLeftRender( GameObject gameObject, ref SpriteBatch batch )
		{
			var usernamePosition = new Vector2( gameObject.Position.X + 10, gameObject.Position.Y + 10 );
			batch.DrawString( normalFont, username, usernamePosition, textColor );
			batch.DrawString( normalFont, message, 
							  new Vector2( usernamePosition.X, usernamePosition.Y + ( 5 + normalFont.MeasureString( username ).Y ) ),
							  textColor );
		}

		private void AlignRightRender( GameObject gameObject, ref SpriteBatch batch )
		{
			var usernamePosition = new Vector2( gameObject.Position.X + texture.Width - 10 - normalFont.MeasureString( username ).X, 
										   gameObject.Position.Y + 10 );
			batch.DrawString( normalFont, username, usernamePosition, textColor);

			var messagePosition = new Vector2( gameObject.Position.X + texture.Width - 10 - normalFont.MeasureString( message ).X,
											   usernamePosition.Y + ( normalFont.MeasureString( message ).Y + 5 ) );
			batch.DrawString( normalFont, message, messagePosition, textColor );
		}

		/// <summary>
		/// Draws the texture to the screen if it's within
		/// the cameraBounds rectangle.
		/// </summary>
		/// <param name="gameObject">GameObject.</param>
		/// <param name="batch">Game spritebatch</param>
		/// <param name="cameraBounds">Current camera area rectangle.</param>
		public override void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds ) {}

		private readonly SpriteFont normalFont;
		private Color textColor;
		private readonly string username;
		private readonly string message;
		private readonly bool isFromOtherUser;
	}
}