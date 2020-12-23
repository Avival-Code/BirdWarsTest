using BirdWarsTest.GraphicComponents;
using BirdWarsTest.HealthComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BirdWarsTest.GameObjects.ObjectManagers
{
	public class HeadsUpDisplayManager
	{
		public HeadsUpDisplayManager()
		{
			gameObjects = new List< GameObject >();
		}

		public void InitializeInterfaceComponents( Microsoft.Xna.Framework.Content.ContentManager content )
		{
			gameObjects.Add( new GameObject( new LifeBarGraphicsComponent( content ), null, 
											 Identifiers.LifeBar, new Vector2( 15.0f, 15.0f ) ) );
		}

		public void Update()
		{

		}

		public void Render( ref SpriteBatch batch, HealthComponent localPlayerHealth )
		{
			gameObjects[ 0 ].Render( ref batch, localPlayerHealth );
		}

		private List< GameObject > gameObjects;
	}
}