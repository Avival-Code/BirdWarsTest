using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.GameObjects
{
	class GameObject
	{
		public GameObject( Identifiers id_In, float pos_xIn, float pos_yIn )
		{
			identifier = id_In;
			position = new Vector2( pos_xIn, pos_yIn );
		}

		public void Update()
		{

		}

		public void Render( ref SpriteBatch batch )
		{

		}

		public void Move( Vector2 offset )
		{
			position += offset;
		}

		private Identifiers identifier;
		private Vector2 position;
	}
}
