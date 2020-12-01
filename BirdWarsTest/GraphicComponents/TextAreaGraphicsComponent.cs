using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	class TextAreaGraphicsComponent : GraphicsComponent
	{
		public TextAreaGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content,
										  string textureName )
			:
			base( content.Load< Texture2D >( textureName ) )
		{
			font = content.Load< SpriteFont >( "Fonts/MainFont_S10" );
			textColor = Color.Black;
		}

		public override void Render( GameObject gameObject, ref SpriteBatch batch )
		{
			batch.Draw( texture, gameObject.Position, Color.White );
			string text = gameObject.Input.GetText();
			Vector2 temp = new Vector2( ( gameObject.Position.X + ( texture.Width / 2 ) ) - ( font.MeasureString( text ).X / 2 ),
									    ( gameObject.Position.Y + ( texture.Height / 2 ) ) - ( font.MeasureString( text ).Y / 2 ) );
			batch.DrawString( font, text, temp, textColor );
		}

		public override void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds ) {}

		private SpriteFont font;
		public Color textColor;
	}
}