using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	public class DecorationBoxGraphicsComponent : GraphicsComponent
	{
		public DecorationBoxGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content, 
											   string textureName )
			:
			base( content.Load< Texture2D >( textureName ) )
		{}

		public override void Render(GameObject gameObject, ref SpriteBatch batch)
		{
			batch.Draw( texture, gameObject.Position, Color.White );
		}
	}
}