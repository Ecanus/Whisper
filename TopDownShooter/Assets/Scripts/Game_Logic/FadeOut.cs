using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeOut : MonoBehaviour {

	[SerializeField]
	private Text whisperText;

	[SerializeField]
	private Text thanksTitleText;
	[SerializeField]
	private Text thanksText;

	[SerializeField]
	private Color thanksTextColor;
	[SerializeField]
	private Color thanksTitleTextColor;

	[SerializeField]
	private Color welcomeTextColor;

	[SerializeField]
	private AudioSource myAudio;

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
