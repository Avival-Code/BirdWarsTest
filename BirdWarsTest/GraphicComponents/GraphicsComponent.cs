/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
The base class of all graphics components.
*********************************************/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BirdWarsTest.GameObjects;

namespace BirdWarsTest.GraphicComponents
{
	/// <summary>
	/// The base class of all graphics components.
	/// </summary>
	public abstract class GraphicsComponent
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public GraphicsComponent() {}

		/// <summary>
		/// Sets the texture to the specified texture input.
		/// </summary>
		/// <param name="texture_In"></param>
		public GraphicsComponent( Texture2D texture_In )
		{
			texture = texture_In;
		}

		/// <summary>
		/// Draws the texture to the screen at the object's position.
		/// </summary>
		/// <param name="gameObject">The game object</param>
		/// <param name="batch">Game Spritebatch</param>
		abstract public void Render( GameObject gameObject, ref SpriteBatch batch  );

		/// <summary>
		/// Draws the texture to the screen if it's within
		/// the cameraBounds rectangle.
		/// </summary>
		/// <param name="gameObject">GameObject.</param>
		/// <param name="batch">Game spritebatch</param>
		/// <param name="cameraBounds">Current camera area rectangle.</param>
		abstract public void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds );

		/// <summary>
		/// Returns the texture size.
		/// </summary>
		/// <returns></returns>
		virtual public Vector2 GetTextureSize()
		{
			return new Vector2( texture.Width, texture.Height );
		}

		/// <summary>
		/// Varies.
		/// </summary>
		/// <param name="newText"></param>
		virtual public void SetText( string newText ) {}

		/// <summary>
		/// Varies.
		/// </summary>
		virtual public void ClearText() {}

		/// <summary>
		/// Base texture variable.
		/// </summary>
		protected Texture2D texture;
	}
}