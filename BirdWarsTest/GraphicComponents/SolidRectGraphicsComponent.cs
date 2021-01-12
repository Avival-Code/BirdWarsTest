/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Graphics component for background objects.
*********************************************/
using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	/// <summary>
	/// Graphics component for background objects.
	/// </summary>
	public class SolidRectGraphicsComponent : GraphicsComponent
	{
		/// <summary>
		/// Creates an instance of the solidrectgraphicscomponent.
		/// </summary>
		/// <param name="content">Game Content manager</param>
		public SolidRectGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content )
			:
			base( content.Load< Texture2D > ( "Background01" ) ) {}

		/// <summary>
		/// Draws the gameObject to the screen.
		/// </summary>
		/// <param name="gameObject"></param>
		/// <param name="batch"></param>
		public override void Render( GameObject gameObject, ref SpriteBatch batch )
		{
			batch.Draw( texture, gameObject.Position, Color.White );
		}

		/// <summary>
		/// Draws the texture to the screen if it's within
		/// the cameraBounds rectangle
		/// </summary>
		/// <param name="gameObject">Game object</param>
		/// <param name="batch">Game Spritebatch</param>
		/// <param name="cameraBounds">Current camera area rectangle</param>
		public override void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds ) {}
	}
}