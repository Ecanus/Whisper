using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleManager : MonoBehaviour {

	[SerializeField]
	private Image sliderBackground;

	[SerializeField]
	private Slider slider;

	[SerializeField]
	private Text startGameText;

	[SerializeField]
	private Text exitGameText;


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

	private void screenTransitionHandle()
	{
		if (Input.GetMouseButtonUp (0) && (slider.value == slider.maxValue)) 
		{
			StartCoroutine ("LoadGame");
		}

		if (Input.GetMouseButtonUp (0) && (slider.value == slider.minValue)) 
		{
			StartCoroutine ("ExitGame");
		}
	}

	private IEnumerator LoadGame()
	{
		//loadingImage.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene(1);
	}

	private IEnumerator ExitGame()
	{
		yield return new WaitForSeconds(1f);
		UnityEditor.EditorApplication.isPlaying = false;
		Application.Quit();
	}


	// Use this for initialization
	void Start () {

		Time.timeScale = 1;
		slider.onValueChanged.AddListener(delegate {sliderValueHandle();});
	}
	
	// Update is called once per frame
	void Update () {

		screenTransitionHandle();
	}
}
