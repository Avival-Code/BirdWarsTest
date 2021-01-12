/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Graphics component for text input area objects.
*********************************************/
using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	/// <summary>
	/// Graphics component for text input area objects.
	/// </summary>
	public class TextAreaGraphicsComponent : GraphicsComponent
	{
		/// <summary>
		/// Creates an isntance of the graphics component.
		/// </summary>
		/// <param name="content">Game content manager.</param>
		/// <param name="textureName">The texture to load.</param>
		public TextAreaGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content,
										  string textureName )
			:
			base( content.Load< Texture2D >( textureName ) )
		{
			font = content.Load< SpriteFont >( "Fonts/MainFont_S10" );
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
			string text = gameObject.Input.GetText();
			Vector2 temp = new Vector2( ( gameObject.Position.X + ( texture.Width / 2 ) ) - ( font.MeasureString( text ).X / 2 ),
									    ( gameObject.Position.Y + ( texture.Height / 2 ) ) - ( font.MeasureString( text ).Y / 2 ) );
			batch.DrawString( font, text, temp, textColor );
		}

		/// <summary>
		/// Draws the texture to the screen if it's within
		/// the cameraBounds rectangle.
		/// </summary>
		/// <param name="gameObject">GameObject.</param>
		/// <param name="batch">Game spritebatch</param>
		/// <param name="cameraBounds">Current camera area rectangle.</param>
		public override void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds ) {}

		private readonly SpriteFont font;
		private Color textColor;
	}
}