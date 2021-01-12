/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Graphics component for a passwird area object.
*********************************************/
using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	/// <summary>
	/// Graphics component for a passwird area object.
	/// </summary>
	public class PasswordAreaGraphicsComponent : GraphicsComponent
	{
		/// <summary>
		/// Creates an isntance of the graphics component.
		/// </summary>
		/// <param name="content">Game content manager.</param>
		public PasswordAreaGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content )
			:
			base( content.Load< Texture2D >( "TextArea1" ) )
		{
			font = content.Load<SpriteFont>( "Fonts/MainFont_S10" );
			textColor = Color.Black;
		}

		/// <summary>
		/// Draws the texture to the screen at the object's position.
		/// </summary>
		/// <param name="gameObject">The game object</param>
		/// <param name="batch">Game Spritebatch</param>
		public override void Render( GameObject gameObject, ref SpriteBatch batch )
		{
			batch.Draw( texture, gameObject.Position, Color.White );
			string text = HideText( gameObject.Input.GetText() );
			Vector2 temp = new Vector2( ( gameObject.Position.X + ( texture.Width / 2 ) ) - ( font.MeasureString( text ).X / 2 ),
										( gameObject.Position.Y + ( texture.Height / 2 ) ) - (font.MeasureString( text ).Y / 2 ) );
			batch.DrawString(font, text, temp, textColor);
		}

		/// <summary>
		/// Draws the texture to the screen if it's within
		/// the cameraBounds rectangle.
		/// </summary>
		/// <param name="gameObject">GameObject.</param>
		/// <param name="batch">Game spritebatch</param>
		/// <param name="cameraBounds">Current camera area rectangle.</param>
		public override void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds ) {}

		private string HideText( string password )
		{
			string temp = "";
			for( int i = 0; i < password.Length; i++ )
			{
				if( password[ i ] != '/' )
				{
					temp += '*';
				}
				else
				{
					temp += password[ i ];
				}
			}
			return temp;
		}

		private readonly SpriteFont font;
		private Color textColor;
	}
}