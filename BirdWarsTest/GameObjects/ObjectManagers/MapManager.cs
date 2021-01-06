using BirdWarsTest.GraphicComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace BirdWarsTest.GameObjects.ObjectManagers
{
	public class MapManager
	{
		public MapManager()
		{
			tileValues = new List< string >();
			position = new Vector2( 0.0f, 0.0f );
			maxTilesHorizontal = 39;
			maxTilesVertical = 30;
			tiles = new GameObject[ maxTilesVertical * maxTilesHorizontal ];
			tileWidth = 64.0f;
			tileHeight = 64.0f;
		}

		public void InitializeMapTiles( Microsoft.Xna.Framework.Content.ContentManager content )
		{
			string tileTextureName = "";
			LoadTileValues();

			for( int y = 0; y < maxTilesVertical; y++ )
			{
				for( int x = 0; x < maxTilesHorizontal; x++ )
				{
					switch( tileValues[ y * maxTilesHorizontal + x ][ 0 ] )
					{
						case '1':
							tileTextureName = "Floors/StoneFloor1";
							break;
					}

					tiles[ y * maxTilesHorizontal + x ] = new GameObject( new FloorGraphicsComponent( content, tileTextureName ), null,
																	  Identifiers.Floor, new Vector2( x * tileWidth, y * tileHeight ) );
				}
			}

		}

		private void LoadTileValues()
		{
			try
			{
				string fileName = @"TileValues.txt";
				string filePath = Path.Combine( Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location ), fileName );
				string [] tempStrings = File.ReadAllLines( filePath );
				foreach( var line in tempStrings )
				{
					tileValues.Add( line );
				}
			}
			catch( FileNotFoundException e )
			{
				Console.WriteLine( e.Message );
			}
		}

		public Rectangle GetMapBounds()
		{
			return new Rectangle( ( int )position.X, ( int )position.Y, 
								  tiles[ maxTilesHorizontal - 1 ].GetRectangle().Right, 
								  tiles[ ( maxTilesHorizontal * maxTilesVertical ) - 1 ].GetRectangle().Bottom );
		}

		public void Render( ref SpriteBatch batch )
		{
			foreach( GameObject tile in tiles )
			{
				tile.Render( ref batch );
			}
		}

		public void Render( ref SpriteBatch batch, Rectangle cameraRenderBounds, Rectangle cameraBounds )
		{
			for( int y = 0; y < maxTilesVertical; y++ )
			{
				for( int x = 0; x < maxTilesHorizontal; x++ )
				{
					if( cameraRenderBounds.Intersects( tiles[ y * maxTilesHorizontal + x ].GetRectangle() ) )
					{
						tiles[ y * maxTilesHorizontal + x ].Render( ref batch, cameraBounds );
					}
				}
			}
		}

		public void ClearAllTiles()
		{
			tileValues.Clear();
			tiles = new GameObject[ maxTilesVertical * maxTilesHorizontal ];
		}

		private GameObject [] tiles;
		private List< string > tileValues;
		private Vector2 position;
		private int maxTilesHorizontal;
		private int maxTilesVertical;
		private float tileWidth;
		private float tileHeight;
	}
}