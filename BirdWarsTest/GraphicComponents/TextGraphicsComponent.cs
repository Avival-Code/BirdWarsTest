using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	class TextGraphicsComponent : GraphicsComponent
	{
		public TextGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content,
										string textIn, string fontName )
			:
			base( content.Load< Texture2D >( "button1" ) )
		{
			font = content.Load< SpriteFont >( fontName );
			text = textIn;
		}
		public override void Render( GameObject gameObject, ref SpriteBatch batch )
		{
			batch.DrawString( font, text, gameObject.position, Color.Black );
		}

		public override Vector2 GetTextureSize()
		{
			return font.MeasureString( text );
		}

		private SpriteFont font;
		private string text;
	}
}
