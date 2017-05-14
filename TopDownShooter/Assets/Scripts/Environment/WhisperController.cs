using UnityEngine;
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


	/// <summary>
	/// The player controller component of the player gameobject
	/// </summary>
	[SerializeField]
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
	/// <returns>Time between fade increments.</returns>
	private IEnumerator whisperStart()
	{

		SpriteRenderer whisperImage = gameObject.GetComponent<SpriteRenderer>();
		Color whisperColor = whisperImage.color;
		whisperColor.a = 1f;
		whisperImage.color = whisperColor;

		gameObject.GetComponent<BoxCollider>().center = new Vector3(0f,0f,0f);

		for (float f = 1f; f >= -0.1f; f -= 0.03f) {

			whisperColor.a = f;
			whisperImage.color = whisperColor;
			yield return new WaitForSeconds(0.06f);
		}

		gameObject.GetComponent<BoxCollider> ().center = new Vector3(0f,0f,100f);
		player.setWhisperPlaceable (true);

	}
}
