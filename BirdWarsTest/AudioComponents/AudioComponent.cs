﻿using Microsoft.Xna.Framework.Audio;

namespace BirdWarsTest.AudioComponents
{
	/// <summary>
	/// Stores, controls and modifies sound for a game object.
	/// </summary>
	public class AudioComponent
	{
		/// <summary>
		/// Default constructor. Sets sound to null.
		/// </summary>
		public AudioComponent()
		{
			objectSound = null;
		}

		/// <summary>
		/// Sets the sound to the audio file that matches the string input value.
		/// </summary>
		/// <param name="content">Game content manager.</param>
		/// <param name="audioName">Audio file name.</param>
		public AudioComponent( Microsoft.Xna.Framework.Content.ContentManager content,
							   string audioName )
		{
			objectSound = content.Load< SoundEffect >( audioName );
		}

		/// <summary>
		/// Plays the loaded audio file.
		/// </summary>
		public virtual void Play()
		{
			objectSound.Play();
		}

		private SoundEffect objectSound;
	}
}