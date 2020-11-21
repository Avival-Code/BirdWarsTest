﻿using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	class ButtonGraphicsComponent : GraphicsComponent
	{
		public ButtonGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content,
										string textureName, string buttonText )
			:
			base( content.Load< Texture2D >( textureName ) )
		{
			font = content.Load< SpriteFont >( "Fonts/MainFont_S10" );
			text = buttonText;
			textColor = Color.Black;
		}

		public override void Render( GameObject gameObject, ref SpriteBatch batch )
		{
			batch.Draw( texture, gameObject.position, Color.White );
			if( !string.IsNullOrEmpty( text ) )
			{
				Vector2 temp = new Vector2( ( gameObject.position.X + ( texture.Width / 2 ) ) - ( font.MeasureString( text ).X / 2 ),
											( gameObject.position.Y + ( texture.Height / 2 ) ) - ( font.MeasureString( text ).Y / 2 ) );
				batch.DrawString( font, text, temp, textColor );
			}
		}

		public void changeColor( Color newColor )
		{
			buttonColor = newColor;
		}

		public string Text
		{
			get{ return text; }
			set{ text = value; }
		}

		private SpriteFont font;
		private string text;
		public Color buttonColor;
		public Color textColor;
	}
}