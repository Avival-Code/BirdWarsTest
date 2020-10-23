using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BirdWarsTest.InputComponents;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.GraphicComponents;

namespace BirdWarsTest.GameObjects
{
	class GameObject
	{
		public GameObject( Identifiers id_In, float pos_xIn, float pos_yIn, InputComponent input_In,
						   GraphicsComponent graphics_In )
		{
			identifier = id_In;
			position = new Vector2( pos_xIn, pos_yIn );
			input = input_In;
			graphics = graphics_In;
		}

		public void Update( KeyboardState state )
		{
			input.HandleInput( state, this );
		}

		public void Render( ref SpriteBatch batch )
		{
			graphics.Render( ref batch, position );
		}

		public void Move( Vector2 offset )
		{
			position += offset;
		}

		private GraphicsComponent graphics = null;
		private InputComponent input = null;
		private Identifiers identifier;
		private Vector2 position;
	}
}