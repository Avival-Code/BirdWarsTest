/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Graphics component for login box object.
*********************************************/
using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	/// <summary>
	/// Graphics component for login box object.
	/// </summary>
	public class LoginBoxGraphicsComponent : GraphicsComponent
	{
		/// <summary>
		/// Creates an isntance of the graphics component.
		/// </summary>
		/// <param name="content">Game content manager.</param>
		public LoginBoxGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content )
			:
			base( content.Load< Texture2D >( "Board_V1" ) )
		{}

		/// <summary>
		/// Draws the texture to the screen at the object's position.
		/// </summary>
		/// <param name="gameObject">The game object</param>
		/// <param name="batch">Game Spritebatch</param>
		public override void Render( GameObject gameObject, ref SpriteBatch batch )
		{
			batch.Draw( texture, gameObject.Position, Color.White );
		}

		/// <summary>
		/// Draws the texture to the screen if it's within
		/// the cameraBounds rectangle.
		/// </summary>
		/// <param name="gameObject">GameObject.</param>
		/// <param name="batch">Game spritebatch</param>
		/// <param name="cameraBounds">Current camera area rectangle.</param>
		public override void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds ) {}
	}
}