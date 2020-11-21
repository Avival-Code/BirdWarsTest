using BirdWarsTest.GameObjects;
using BirdWarsTest.GraphicComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	public class ChatMessageGraphicsComponent : GraphicsComponent
	{
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
		public override void Render( GameObject gameObject, ref SpriteBatch batch )
		{
			batch.Draw( texture, gameObject.Position, Color.White );
			if( isFromOtherUser )
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

			var messagePosition = new Vector2( gameObject.Position.X + texture.Width - 10 - normalFont.MeasureString(username).X,
											   usernamePosition.Y + 5 );
			batch.DrawString( normalFont, message, messagePosition, textColor );
		}

		private SpriteFont normalFont;
		private Color textColor;
		private string username;
		private string message;
		private bool isFromOtherUser;
	}
}
