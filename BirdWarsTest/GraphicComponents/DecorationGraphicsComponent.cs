using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.GraphicComponents
{
	class DecorationGraphicsComponent : GraphicsComponent
	{
		public DecorationGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content,
										    string textureName )
			:
			base( content.Load< Texture2D >( textureName ) )
		{}

		public override void Render( GameObject gameObject, ref SpriteBatch batch )
		{
			batch.Draw( texture, gameObject.Position, Color.White );
		}
	}
}
