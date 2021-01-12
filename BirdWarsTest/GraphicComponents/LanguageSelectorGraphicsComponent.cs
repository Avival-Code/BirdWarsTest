/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Graphics component for a language selector object.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	/// <summary>
	/// Graphics component for a language selector object.
	/// </summary>
	public class LanguageSelectorGraphicsComponent : GraphicsComponent
	{
		/// <summary>
		/// Creates an isntance of the graphics component.
		/// </summary>
		/// <param name="content">Game content manager</param>
		/// <param name="stringManager">Game string manager</param>
		/// <param name="currentLanguage">Current settings language.</param>
		/// <param name="positionIn">Object position</param>
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

		/// <summary>
		/// Draws the texture to the screen at the object's position.
		/// </summary>
		/// <param name="gameObject">The game object</param>
		/// <param name="batch">Game Spritebatch</param>
		public override void Render( GameObject gameObject, ref SpriteBatch batch ) 
		{
			batch.Draw( texture, gameObject.Position, Color.White );
			batch.DrawString( font, Text, new Vector2( gameObject.Position.X + 35, gameObject.Position.Y ), Color.Black );
			batch.Draw( rightArrowTexture, new Vector2( gameObject.Position.X + 135, gameObject.Position.Y ), Color.White );
		}

		/// <summary>
		/// Draws the texture to the screen if it's within
		/// the cameraBounds rectangle.
		/// </summary>
		/// <param name="gameObject">GameObject.</param>
		/// <param name="batch">Game spritebatch</param>
		/// <param name="cameraBounds">Current camera area rectangle.</param>
		public override void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds ) {}

		/// <summary>
		/// Returns the left arrow texture size.
		/// </summary>
		/// <returns>Returns the left arrow texture size.</returns>
		public Rectangle GetLeftArrowBounds()
		{
			return new Rectangle( ( int )position.X, ( int )position.Y, texture.Width, texture.Height );
		}

		/// <summary>
		/// Returns the right arrow texture size.
		/// </summary>
		/// <returns>Returns the right arrow texture size.</returns>
		public Rectangle GetRightArrowBounds()
		{
			return new Rectangle( ( int )position.X + 135, ( int )position.Y, rightArrowTexture.Width, rightArrowTexture.Height );
		}

		private Texture2D rightArrowTexture;
		private SpriteFont font;
		private Vector2 position;

		/// <value>Current selected language</value>
		public Languages SelectedLanguage { get; set; }

		/// <value>Object text</value>
		public string Text { get; set; }
	}
}