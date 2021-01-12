/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Graphics component for a decoration object.
*********************************************/
using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.GraphicComponents
{
	/// <summary>
	/// Graphics component for a decoration object.
	/// </summary>
	public class DecorationGraphicsComponent : GraphicsComponent
	{
		/// <summary>
		/// Creates an isntance of the graphics component with the specified
		/// texture.
		/// </summary>
		/// <param name="content">Game content manager.</param>
		/// <param name="textureName">The texture to load.</param>
		public DecorationGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content,
										    string textureName )
			:
			base( content.Load< Texture2D >( textureName ) )
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
