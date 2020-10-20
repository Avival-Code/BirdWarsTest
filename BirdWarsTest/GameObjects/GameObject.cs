using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BirdWarsTest.InputComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace BirdWarsTest.GameObjects
{
	class GameObject
	{
		public GameObject( Identifiers id_In, float pos_xIn, float pos_yIn, InputComponent input_In )
		{
			identifier = id_In;
			position = new Vector2( pos_xIn, pos_yIn );
			input = input_In;
		}

		public void Update( KeyboardState state )
		{
			input.HandleInput( state, this );
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
		private InputComponent input;
	}
}
