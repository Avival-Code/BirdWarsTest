/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Audio component used for the play state music player.
It stores the songs used for each game round.
*********************************************/
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace BirdWarsTest.AudioComponents
{
	/// <summary>
	/// Audio component used for the play state music player. 
	/// It stores the songs used for each game round.
	/// </summary>
	public class MusicplayerAudioComponent : AudioComponent
	{
		/// <summary>
		/// Initializes the songs available per game round. Configures the
		/// mediaplayer and plays a random song.
		/// </summary>
		/// <param name="content"></param>
		public MusicplayerAudioComponent( Microsoft.Xna.Framework.Content.ContentManager content )
			: 
			base()
		{
			roundSongs = new List< Song >();
			roundSongs.Add( content.Load< Song >( "Music/AnythingButTangerines" ) );
			roundSongs.Add( content.Load< Song >( "Music/ForPetesSake" ) );
			roundSongs.Add( content.Load< Song >( "Music/WhatTheHeck" ) );
			MediaPlayer.Volume = 0.5f;
			MediaPlayer.IsRepeating = true;
			PlayRandomSong();
		}

		private void PlayRandomSong()
		{
			Random trackSelector = new Random();
			switch( trackSelector.Next( 1, 4 ) )
			{
				case 1:
					MediaPlayer.Play( roundSongs[ 0 ] );
					break;

				case 2:
					MediaPlayer.Play( roundSongs[ 1 ] );
					break;

				case 3:
					MediaPlayer.Play( roundSongs[ 2 ] );
					break;
			}
		}

		/// <summary>
		/// Stops playing the current song.
		/// </summary>
		public void Stop()
		{
			MediaPlayer.Stop();
		}

		private List< Song > roundSongs;
	}
}