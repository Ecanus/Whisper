using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// TitleScreenFadeOut class manages text display on the main opening screen of game
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// 
/// </summary>
///
public class TitleScreenFadeOut : MonoBehaviour {

	/// <summary>
	/// Text displaying <Whisper>
	/// </summary>
	[SerializeField]
	private Text whisperText;

	/// <summary>
	/// The thanks title text.
	/// </summary>
	[SerializeField]
	private Text thanksTitleText;

	/// <summary>
	/// Text containing written thanks
	/// </summary>
	[SerializeField]
	private Text thanksText;

	/// <summary>
	/// Text colours.
	/// </summary>
	[SerializeField]
	private Color thanksTextColor;
	[SerializeField]
	private Color thanksTitleTextColor;
	[SerializeField]
	private Color welcomeTextColor;

	/// <summary>
	/// BGM AudioSource
	/// </summary>
	[SerializeField]
	private AudioSource myAudio;

	/// <summary>
	/// Fades out thanks text.
	/// </summary>
	/// <returns>Timer between fade increments.</returns>
	private IEnumerator fadeOutThanksText()
	{
		thanksTextColor = thanksText.color;
		thanksTitleTextColor = thanksTitleText.color;

		yield return new WaitForSeconds (1.5f);

		for (float f = 1; f >= -0.1f; f -= 0.015f) {

			thanksTextColor.a = f;
			thanksTitleTextColor.a = f;

			thanksTitleText.color = thanksTitleTextColor;
			thanksText.color = thanksTextColor;

			yield return new WaitForSeconds(0.03f);
		}

		StartCoroutine("fadeOutWhisperText");
	}

	/// <summary>
	/// Fades out Whisper Text
	/// </summary>
	/// <returns>Timer between fade increments.</returns>
	private IEnumerator fadeOutWhisperText()
	{
		welcomeTextColor = whisperText.color;

		for (float f = 0; f <= 1; f += 0.02f) {

			welcomeTextColor.a = f;
			whisperText.color = welcomeTextColor;
			yield return new WaitForSeconds(0.05f);
		}

		for (float f = 1; f >= -0.1f; f -= 0.02f) {

			welcomeTextColor.a = f;
			whisperText.color = welcomeTextColor;
			yield return new WaitForSeconds(0.05f);
		}

		myAudio.volume = 0.7f;
		myAudio.Play();
		SceneManager.LoadScene(1);
	}

	// Use this for initialization
	void Start () {
		welcomeTextColor = whisperText.color;
		StartCoroutine ("fadeOutThanksText");

	}
}
