using BirdWarsTest.GameObjects;
using BirdWarsTest.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.GraphicComponents
{
	class LanguageSelectorGraphicsComponent : GraphicsComponent
	{
		public LanguageSelectorGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content,
												  Languages currentLanguage, StringManager stringManager )
			:
			base( content.Load< Texture2D >( "Buttons/SelectorButtonLeft" ) )
		{
			RightArrowTexture = content.Load< Texture2D >( "Buttons/SelectorButtonRight" );
			font = content.Load< SpriteFont >( "Fonts/BabeFont_17" );
			switch( currentLanguage )
			{
				case Languages.English:
					Text = stringManager.GetString( StringNames.English );
					break;

				case Languages.Spanish:
					Text = stringManager.GetString( StringNames.Spanish );
					break;
			}
		}

		public override void Render( GameObject gameObject, ref SpriteBatch batch ) 
		{
			batch.Draw( texture, gameObject.Position, Color.White );
			batch.DrawString( font, Text, new Vector2( gameObject.Position.X + 35, gameObject.Position.Y ), Color.Black );
			batch.Draw( RightArrowTexture, new Vector2( gameObject.Position.X + 135, gameObject.Position.Y ), Color.White );
		}

		public override void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds ) {}

		public string Text { get; set; }
		public Texture2D LeftArrowTexture { get { return texture; } }
		public Texture2D RightArrowTexture { get; private set; }

		private SpriteFont font;
	}
}
