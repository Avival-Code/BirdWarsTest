/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Class used to get a fixed position from a list of possible
positions by using a random index. The positions are used 
for player gameobjects.
*********************************************/
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace BirdWarsTest.Utilities
{
	/// <summary>
	/// Class used to get a fixed position from a list of possible
	/// positions by using a random index.The positions are used
	/// for player gameobjects.
	/// </summary>
	public class PositionGenerator
	{
		/// <summary>
		/// Creates an instance of PositionGenerator, index
		/// generator and list of start positions. Adds all position positions
		/// </summary>
		public PositionGenerator()
		{
			indexGenerator = new Random();
			startPositions = new List< Vector2 >();
			ResetStartPositions();
			upperLimit = 8;
		}

		/// <summary>
		/// Resets upper limit to starting value.
		/// </summary>
		public void ResetLimit()
		{
			upperLimit = 8;
		}

		/// <summary>
		/// Adds all possible start positions.
		/// </summary>
		public void ResetStartPositions()
		{
			startPositions.Clear();
			startPositions.Add( new Vector2( 400, 300 ) );
			startPositions.Add( new Vector2( 1200, 300 ) );
			startPositions.Add( new Vector2( 2000, 300 ) );
			startPositions.Add( new Vector2( 400, 900 ) );
			startPositions.Add( new Vector2( 2000, 900 ) );
			startPositions.Add( new Vector2( 400, 1500 ) );
			startPositions.Add( new Vector2( 1200, 1500 ) );
			startPositions.Add( new Vector2( 2000, 1500 ) );
		}

		/// <summary>
		/// Gets a position using a random index. It's like
		/// sticking your hand into a black bag and pulling
		/// a ball of random color.
		/// </summary>
		/// <returns>A player position.</returns>
		public Vector2 GetAPosition()
		{
			int vectorIndex = indexGenerator.Next( LowerLimit, upperLimit );
			upperLimit--;
			var vector = startPositions[ vectorIndex ];
			startPositions.RemoveAt( vectorIndex );
			ResetLimitAndPositions();
			return vector;
		}

		private void ResetLimitAndPositions()
		{
			if( upperLimit == LowerLimit )
			{
				ResetStartPositions();
				ResetLimit();
			}
		}

		private readonly Random indexGenerator;
		private List< Vector2 > startPositions;
		private const int LowerLimit = 0;
		private int upperLimit;
	}
}