using BirdWarsTest.GameObjects;
using BirdWarsTest.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	public class LanguageSelectorGraphicsComponent : GraphicsComponent
	{
		public LanguageSelectorGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content,
												  StringManager stringManager, Languages currentLanguage,
												  Vector2 positionIn )
			:
			base( content.Load< Texture2D >( "Buttons/SelectorButtonLeft" ) )
		{
			rightArrowTexture = content.Load< Texture2D >( "Buttons/SelectorButtonRight" );
			font = content.Load< SpriteFont >( "Fonts/BabeFont_17" );
			SelectedLanguage = currentLanguage;
			position = positionIn;
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
			batch.Draw( rightArrowTexture, new Vector2( gameObject.Position.X + 135, gameObject.Position.Y ), Color.White );
		}

		public override void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds ) {}

		public Rectangle GetLeftArrowBounds()
		{
			return new Rectangle( ( int )position.X, ( int )position.Y, texture.Width, texture.Height );
		}

		public Rectangle GetRightArrowBounds()
		{
			return new Rectangle( ( int )position.X + 135, ( int )position.Y, rightArrowTexture.Width, rightArrowTexture.Height );
		}

		private Texture2D rightArrowTexture;
		private SpriteFont font;
		private Vector2 position;
		public Languages SelectedLanguage { get; set; }
		public string Text { get; set; }
	}
}