using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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
			batch.DrawString( font, text, gameObject.Position, Color.Black );
		}

		public override void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds ) {}

		public override Vector2 GetTextureSize()
		{
			return font.MeasureString( text );
		}

		public override void SetText( string newText )
		{
			text = newText;
		}

		private SpriteFont font;
		private string text;
	}
}