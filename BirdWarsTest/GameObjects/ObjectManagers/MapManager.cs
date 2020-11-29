using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GameObjects.ObjectManagers
{
	class MapManager
	{
		public MapManager()
		{
			position = new Vector2( 0.0f, 0.0f );
			maxTilesHorizontal = 39;
			maxTilesVertical = 30;
			Tiles = new GameObject[ maxTilesHorizontal * maxTilesVertical ];
		}

		public Rectangle GetMapBounds()
		{
			return new Rectangle( ( int )position.X, ( int )position.Y, 
								  Tiles[ maxTilesHorizontal - 1 ].GetRectangle().Right, 
								  Tiles[ ( maxTilesHorizontal * maxTilesVertical ) - 1 ].GetRectangle().Bottom );
		}

		public void Render( ref SpriteBatch batch )
		{
			foreach( var objects in Tiles )
			{
				objects.Render( ref batch );
			}
		}

		public void Render( ref SpriteBatch batch, Rectangle cameraBounds )
		{
			foreach( var tile in Tiles )
			{
				if( cameraBounds.Contains( tile.GetRectangle() ) )
				{
					tile.Render( ref batch );
				}
			}
		}

		public GameObject [] Tiles { get; set; }
		private string [] tileValues;
		private Vector2 position;
		private int maxTilesHorizontal;
		private int maxTilesVertical;
	}
}