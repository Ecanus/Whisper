using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour {

	[SerializeField]
	private GameObject player;
	 
	[SerializeField]
	private GameObject pause_Panel;

	[SerializeField]
	private GameObject defeat_Panel;

	[SerializeField]
	private Animator pauseAnim;

	[SerializeField]
	private Animator defeatAnim;

	[SerializeField]
	private Image sliderBackground;

	[SerializeField]
	private Slider defeatSlider;

	[SerializeField]
	private Text reloadGameText;

	[SerializeField]
	private Text exitGameText;

	private bool isPaused;


	public void sliderBackgroundHandle(bool activate)
	{
		Color backgroundColor = sliderBackground.color;
		if (activate) {
			backgroundColor.a = 0.3f;
			sliderBackground.color = backgroundColor;
		} else {
			backgroundColor.a = 0f;
			sliderBackground.color = backgroundColor;
		}
	}

	public void sliderValueHandle()
	{

		if (defeatSlider.value == defeatSlider.maxValue) 
		{
			reloadGameText.gameObject.SetActive (true);
		} 
		else 
		{
			//StopAllCoroutines ();
			reloadGameText.gameObject.SetActive (false);
		}


		if (defeatSlider.value == defeatSlider.minValue) 
		{
			exitGameText.gameObject.SetActive (true);
		} 
		else 
		{
			//StopAllCoroutines ();
			exitGameText.gameObject.SetActive (false);
		}
	}

	private void handleInput()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			pauseGame();
		}

		if (Input.GetMouseButtonUp (0) && (defeatSlider.value == defeatSlider.maxValue)) 
		{
			loadScene(1);
		}

		if (Input.GetMouseButtonUp (0) && (defeatSlider.value == defeatSlider.minValue)) 
		{
			exitGame();
		}
	}

	public void pauseGame()
	{
		isPaused = !isPaused;
		pause_Panel.SetActive (isPaused);

		if (isPaused) {
			pauseAnim.enabled = true;
			pauseAnim.Play ("PauseMenuActivate");
			player.GetComponent<PlayerController>().setIsPaused (true);

			Time.timeScale = 0;

		} else {
			player.GetComponent<PlayerController>().setIsPaused (false);

			Time.timeScale = 1;
		}
	}

	public void lostGame()
	{
		player.GetComponent<PlayerController>().setIsPaused (true);

		pause_Panel.SetActive (false);
		defeat_Panel.SetActive (true);

		defeatAnim.enabled = true;
		defeatAnim.Play ("DefeatMenuActivate");

		Time.timeScale = 0;
	}


	private void exitGame()
	{
		UnityEditor.EditorApplication.isPlaying = false;
		Application.Quit();
	}


	public void loadScene(int sceneIndex)
	{
		//loadingImage.SetActive(true);
		SceneManager.LoadScene(sceneIndex);
	}



	// Use this for initialization
	void Start () {

		defeatSlider.onValueChanged.AddListener(delegate {sliderValueHandle();});
		player.GetComponent<PlayerController>().setIsPaused (false);

		Time.timeScale = 1f;
		isPaused = false;
		pauseAnim.enabled = false;
		defeatAnim.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		handleInput();
	}
}
