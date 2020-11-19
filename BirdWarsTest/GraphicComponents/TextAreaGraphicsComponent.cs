using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.GraphicComponents
{
	class TextAreaGraphicsComponent : GraphicsComponent
	{
		public TextAreaGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content )
			:
			base( content.Load< Texture2D >( "TextArea1" ) )
		{
			font = content.Load< SpriteFont >( "Fonts/MainFont_S10" );
			textColor = Color.Black;
		}

		public override void Render(GameObject gameObject, ref SpriteBatch batch)
		{
			batch.Draw( texture, gameObject.position, Color.White );
			string text = gameObject.input.GetText();
			Vector2 temp = new Vector2( ( gameObject.position.X + ( texture.Width / 2 ) ) - ( font.MeasureString( text ).X / 2 ),
									    ( gameObject.position.Y + ( texture.Height / 2 ) ) - ( font.MeasureString( text ).Y / 2 ) );
			batch.DrawString( font, text, temp, textColor );
		}

		private SpriteFont font;
		public Color textColor;
	}
}
