﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.GraphicComponents
{
	abstract class GraphicsComponent
	{
		public GraphicsComponent( Texture2D texture_In )
		{
			texture = texture_In;
		}

		abstract public void Render( ref SpriteBatch batch );

		protected Texture2D texture;
	}
}
