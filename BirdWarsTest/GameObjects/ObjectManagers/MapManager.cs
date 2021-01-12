/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Handles the creation, and display of the game map.
*********************************************/
using BirdWarsTest.GraphicComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace BirdWarsTest.GameObjects.ObjectManagers
{
	/// <summary>
	/// Handles the creation, and display of the game map.
	/// </summary>
	public class MapManager
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
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

		/// <summary>
		/// Creates the map from the list of tile textures.
		/// </summary>
		/// <param name="content">game contentManager.</param>
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

		/// <summary>
		/// Calculates the map area rectangle with the game Tiles.
		/// </summary>
		/// <returns>A rectangle of the map area.</returns>
		public Rectangle GetMapBounds()
		{
			return new Rectangle( ( int )position.X, ( int )position.Y, 
								  tiles[ maxTilesHorizontal - 1 ].GetRectangle().Right, 
								  tiles[ ( maxTilesHorizontal * maxTilesVertical ) - 1 ].GetRectangle().Bottom );
		}

		/// <summary>
		/// Draws the game tiles to the screen.
		/// </summary>
		/// <param name="batch">Game spritebatch.</param>
		public void Render( ref SpriteBatch batch )
		{
			foreach( GameObject tile in tiles )
			{
				tile.Render( ref batch );
			}
		}

		/// <summary>
		/// Draws the objects that are withing the camera render
		/// bounds to the screen.
		/// </summary>
		/// <param name="batch"></param>
		/// <param name="cameraRenderBounds"></param>
		/// <param name="cameraBounds"></param>
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

		/// <summary>
		/// Removes al gameObjects from tile list and resets tile values.
		/// </summary>
		public void ClearAllTiles()
		{
			tileValues.Clear();
			tiles = new GameObject[ maxTilesVertical * maxTilesHorizontal ];
		}

		private GameObject [] tiles;
		private List< string > tileValues;
		private Vector2 position;
		private readonly int maxTilesHorizontal;
		private readonly int maxTilesVertical;
		private readonly float tileWidth;
		private readonly float tileHeight;
	}
}