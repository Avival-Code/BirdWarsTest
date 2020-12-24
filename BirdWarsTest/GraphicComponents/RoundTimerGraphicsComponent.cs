using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	public class RoundTimerGraphicsComponent : GraphicsComponent
	{
		public RoundTimerGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content )
			:
			base( null )
		{
			font = content.Load< SpriteFont >( "Fonts/BabeFont_17" );
			fontColor = Color.AliceBlue;
			minutes = "";
			seconds = "";
		}

		public override void Render( GameObject gameObject, ref SpriteBatch batch ) 
		{
			minutes = gameObject.Input.GetRemainingMinutes().ToString();
			SetSeconds( gameObject );
			string timeString = minutes + " : " + seconds;
			batch.DrawString( font, timeString, gameObject.Position, fontColor );
		}

		public override void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds ) {}

		private void SetSeconds( GameObject gameObject )
		{
			if( gameObject.Input.GetRemainingSeconds() < 10 )
			{
				seconds = "0" + gameObject.Input.GetRemainingSeconds().ToString();
			}
			else
			{
				seconds = gameObject.Input.GetRemainingSeconds().ToString();
			}
		}

		private SpriteFont font;
		private Color fontColor;
		private string minutes;
		private string seconds;
	}
}