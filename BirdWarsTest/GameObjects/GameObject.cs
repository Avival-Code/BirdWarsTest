using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BirdWarsTest.InputComponents;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.GraphicComponents;

namespace BirdWarsTest.GameObjects
{
	class GameObject
	{
		public GameObject( GraphicsComponent graphics_In, InputComponent input_In,
						   Identifiers id_In, Vector2 position_in )
		{
			graphics = graphics_In;
			input = input_In;
			identifier = id_In;
			position = position_in;
		}

		public GameObject( GraphicsComponent graphics_In, InputComponent input_In, 
						   Identifiers id_In, float screenWidth, float screenHeight )
		{
			graphics = graphics_In;
			input = input_In;
			identifier = id_In;
			if( graphics != null )
				position = new Vector2( CenterXWidth( screenWidth, graphics.getTextureSize().X ),
										( screenHeight - screenHeight + 30 ) );
			else { position = new Vector2( 0.0f, 0.0f ); }
		}

		public void Update( KeyboardState state )
		{
			if( input != null )
			input.HandleInput( this, state );
		}

		public void Render( ref SpriteBatch batch )
		{
			graphics.Render( this, ref batch );
		}

		public void Move( Vector2 offset )
		{
			position += offset;
		}

		public float CenterXWidth( float screenWidth, float textureWidth )
		{
			return ( screenWidth / 2 ) - ( textureWidth / 2 );
		}

		private GraphicsComponent graphics = null;
		private InputComponent input = null;
		private Identifiers identifier;
		public Vector2 position;
	}
}