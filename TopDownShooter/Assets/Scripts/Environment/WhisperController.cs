﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// WhisperController class manages whisper functionality
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// 
/// </summary>
///
public class WhisperController : MonoBehaviour {


	private PlayerController player;

	/// <summary>
	/// Activates the whisper via coroutine
	/// </summary>
	public void activateWhisper()
	{
		if (player.getWhisperPlaceable ()) 
		{
			StartCoroutine (whisperStart ());
		}
	}

	/// <summary>
	/// Whisper becomes functioning for a period of time
	/// </summary>
	/// <returns>The start.</returns>
	private IEnumerator whisperStart()
	{


		Image whisperImage = gameObject.GetComponent<Image>();
		Color whisperColor = whisperImage.color;
		whisperColor.a = 1f;
		whisperImage.color = whisperColor;

		gameObject.GetComponent<BoxCollider>().center = new Vector3(0f,0f,0f);

		for (float f = 1f; f >= -0.1f; f -= 0.03f) {

			whisperColor.a = f;
			whisperImage.color = whisperColor;
			yield return new WaitForSeconds(0.05f);
		}

		gameObject.GetComponent<BoxCollider> ().center = new Vector3(0f,0f,100f);
		player.setWhisperPlaceable (true);

	}
		


	// Use this for initialization
	void Start () {
		player = GameObject.Find("Sprite_Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}