using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// TitleManager handles title screen UI functionality and state management
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// </summary>
///
public class TitleManager : MonoBehaviour {

	/// <summary>
	/// The slider background.
	/// </summary>
	[SerializeField]
	private Image sliderBackground;

	/// <summary>
	/// The slider.
	/// </summary>
	[SerializeField]
	private Slider slider;

	/// <summary>
	/// The start game text.
	/// </summary>
	[SerializeField]
	private Text startGameText;

	/// <summary>
	/// The exit game text.
	/// </summary>
	[SerializeField]
	private Text exitGameText;

	/// <summary>
	/// The background music
	/// </summary>
	[SerializeField]
	private AudioSource BGM;

	/// <summary>
	/// Reveals slider background on mouse hover
	/// </summary>
	/// <param name="activate">If set to <c>true</c> activate.</param>
	public void sliderBackgroundHandle(bool activate)
	{
		Color backgroundColor = sliderBackground.color;
		if (activate) {
			backgroundColor.a = 0.1f;
			sliderBackground.color = backgroundColor;
		} else {
			backgroundColor.a = 0f;
			sliderBackground.color = backgroundColor;
		}
	}


	/// <summary>
	/// Handles text display based on slider value
	/// </summary>
	public void sliderValueHandle()
	{
	
		if (slider.value == slider.maxValue) 
		{
			startGameText.gameObject.SetActive (true);
		} 
		else 
		{
			StopAllCoroutines ();
			startGameText.gameObject.SetActive (false);
		}


		if (slider.value == slider.minValue) 
		{
			exitGameText.gameObject.SetActive (true);
		} 
		else 
		{
			StopAllCoroutines ();
			exitGameText.gameObject.SetActive (false);
		}

	}

	/// <summary>
	/// Handles Game state based on slider value
	/// </summary>
	private void screenTransitionHandle()
	{
		if (Input.GetMouseButtonUp (0) && (slider.value == slider.maxValue)) 
		{
			StartCoroutine ("fadeMusic");
			StartCoroutine ("LoadGame");
		}

		if (Input.GetMouseButtonUp (0) && (slider.value == slider.minValue)) 
		{
			StartCoroutine ("ExitGame");
		}
	}


	private IEnumerator fadeMusic()
	{
		for (float f = 0.5f; f >= 0.05f; f -= 0.05f) {

			BGM.volume = f;
			yield return new WaitForSeconds(0.05f);
		}
	}

	/// <summary>
	/// Loads actual game after some time
	/// </summary>
	/// <returns>The game.</returns>
	private IEnumerator LoadGame()
	{
		//loadingImage.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene(2);
	}

	/// <summary>
	/// Exits entire game after some time
	/// </summary>
	/// <returns>The game.</returns>
	private IEnumerator ExitGame()
	{
		yield return new WaitForSeconds(1f);
		//UnityEditor.EditorApplication.isPlaying = false;
		Application.Quit();
	}

	// Use this for initialization
	void Start () {

		Time.timeScale = 1;
		slider.onValueChanged.AddListener(delegate {sliderValueHandle();});

		BGM = GameObject.Find ("BackgroundMusic_Audio Source").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

		screenTransitionHandle();
	}
}
