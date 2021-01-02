using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	public class PigeonMilkGraphicsComponent : GraphicsComponent
	{
		public PigeonMilkGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content )
			:
			base( content.Load< Texture2D >( "Items/ItemFiller" ) )
		{}

		public override void Render( GameObject gameObject, ref SpriteBatch batch ) {}

		public override void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds )
		{
			batch.Draw( texture, new Vector2( gameObject.Position.X - cameraBounds.Left, gameObject.Position.Y - cameraBounds.Top ),
						Color.White );
		}
	}
}