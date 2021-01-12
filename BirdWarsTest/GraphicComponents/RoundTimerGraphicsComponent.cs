/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Graphics component for the round timer component.
*********************************************/
using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	/// <summary>
	/// Graphics component for the round timer component.
	/// </summary>
	public class RoundTimerGraphicsComponent : GraphicsComponent
	{
		/// <summary>
		/// Creates an instance of the graphics component.
		/// </summary>
		/// <param name="content"></param>
		public RoundTimerGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content )
			:
			base( null )
		{
			font = content.Load< SpriteFont >( "Fonts/BabeFont_17" );
			fontColor = Color.AliceBlue;
			minutes = "";
			seconds = "";
		}

		/// <summary>
		/// Draws the texture to the screen.
		/// </summary>
		/// <param name="gameObject">gameObject</param>
		/// <param name="batch">Game spritebatch</param>
		public override void Render( GameObject gameObject, ref SpriteBatch batch ) 
		{
			minutes = gameObject.Input.GetRemainingMinutes().ToString();
			SetSeconds( gameObject );
			string timeString = minutes + " : " + seconds;
			batch.DrawString( font, timeString, gameObject.Position, fontColor );
		}

		/// <summary>
		/// Draws the texture to the screen if it's within
		/// the cameraBounds rectangle.
		/// </summary>
		/// <param name="gameObject">GameObject.</param>
		/// <param name="batch">Game spritebatch</param>
		/// <param name="cameraBounds">Current camera area rectangle.</param>
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

		private readonly SpriteFont font;
		private Color fontColor;
		private string minutes;
		private string seconds;
	}
}