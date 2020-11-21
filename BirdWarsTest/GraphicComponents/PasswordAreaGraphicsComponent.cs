using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	class PasswordAreaGraphicsComponent : GraphicsComponent
	{
		public PasswordAreaGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content )
			:
			base( content.Load< Texture2D >( "TextArea1" ) )
		{
			font = content.Load<SpriteFont>( "Fonts/MainFont_S10" );
			textColor = Color.Black;
		}

		public override void Render( GameObject gameObject, ref SpriteBatch batch )
		{
			batch.Draw( texture, gameObject.Position, Color.White );
			string text = HideText( gameObject.input.GetText() );
			Vector2 temp = new Vector2( ( gameObject.Position.X + ( texture.Width / 2 ) ) - ( font.MeasureString( text ).X / 2 ),
										( gameObject.Position.Y + ( texture.Height / 2 ) ) - (font.MeasureString( text ).Y / 2 ) );
			batch.DrawString(font, text, temp, textColor);
		}

		private string HideText( string password )
		{
			string temp = "";
			for (int i = 0; i < password.Length; i++)
				temp += '*';
			return temp;
		}

		private SpriteFont font;
		public Color textColor;
	}
}