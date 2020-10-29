using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	class LoginLogoGraphicsComponent : GraphicsComponent
	{
		public LoginLogoGraphicsComponent(Microsoft.Xna.Framework.Content.ContentManager content)
			:
			base( content.Load< Texture2D >( "BirdWarsLogo_V1" ) )
		{}

		public override void Render( GameObject gameObject, ref SpriteBatch batch )
		{
			batch.Draw( texture, gameObject.position, Color.White );
		}
	}
}
