/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
The graphics component for simple text objects.
*********************************************/
using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	/// <summary>
	/// The grahics compoennt for simple text objects.
	/// </summary>
	public class TextGraphicsComponent : GraphicsComponent
	{
		/// <summary>
		/// Creates an instance of Text Graphics Component.
		/// </summary>
		/// <param name="content">Game conteng Manager</param>
		/// <param name="textIn">Text value</param>
		/// <param name="fontName">Font name</param>
		public TextGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content,
									  string textIn, string fontName )
			:
			base( content.Load< Texture2D >( "button1" ) )
		{
			font = content.Load< SpriteFont >( fontName );
			text = textIn;
			textColor = Color.Black;
		}

		/// <summary>
		/// Creates an instance of a text graphics component 
		/// that is rendered with a specified color.
		/// </summary>
		/// <param name="content">Game content manager.</param>
		/// <param name="colorIn">Specified Color</param>
		/// <param name="textIn">Specified text</param>
		/// <param name="fontName">Font name</param>
		public TextGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content,
									  Color colorIn, string textIn, string fontName )
			:
			base( content.Load<Texture2D>( "button1" ) )
		{
			font = content.Load<SpriteFont>(fontName);
			text = textIn;
			textColor = colorIn;
		}

		/// <summary>
		/// Draws the texture to the screen.
		/// </summary>
		/// <param name="gameObject">Game object</param>
		/// <param name="batch">Game spritebatch</param>
		public override void Render( GameObject gameObject, ref SpriteBatch batch )
		{
			batch.DrawString( font, text, gameObject.Position, textColor );
		}

		/// <summary>
		/// Draws the texture to the screen if its within the camera bounds 
		/// rectangle.
		/// </summary>
		/// <param name="gameObject">Game object</param>
		/// <param name="batch">Game spritebatch</param>
		/// <param name="cameraBounds">Current camera area rectangle.</param>
		public override void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds ) {}

		/// <summary>
		/// Returns the texture string size.
		/// </summary>
		/// <returns>Returns the texture string size.</returns>
		public override Vector2 GetTextureSize()
		{
			return font.MeasureString( text );
		}

		/// <summary>
		/// Sets the current text.
		/// </summary>
		/// <param name="newText"> specified text.</param>
		public override void SetText( string newText )
		{
			text = newText;
		}

		/// <summary>
		/// Clears the text.
		/// </summary>
		public override void ClearText()
		{
			text = "";
		}

		private readonly SpriteFont font;
		private Color textColor;
		private string text;
	}
}