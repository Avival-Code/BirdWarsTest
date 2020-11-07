using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	class Button1GraphicsComponent : GraphicsComponent
	{
		public Button1GraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content,
										string buttonText )
			:
			base( content.Load< Texture2D >( "Button" ) ) 
		{
			font = content.Load< SpriteFont >( "Fonts/MainFont" );
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

		private SpriteFont font;
		public Color buttonColor;
		public Color textColor;
		public string text;
	}
}
