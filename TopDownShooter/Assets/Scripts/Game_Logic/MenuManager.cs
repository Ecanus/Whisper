using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// MenuManager handles in game UI functionality and state management
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// </summary>
///
public class MenuManager : MonoBehaviour {

	/// <summary>
	/// The player.
	/// </summary>
	[SerializeField]
	private GameObject player;
	 
	/// <summary>
	/// The pause panel.
	/// </summary>
	[SerializeField]
	private GameObject pause_Panel;

	/// <summary>
	/// The defeat panel.
	/// </summary>
	[SerializeField]
	private GameObject defeat_Panel;

	/// <summary>
	/// The pause animation.
	/// </summary>
	[SerializeField]
	private Animator pauseAnim;

	/// <summary>
	/// The defeat animation.
	/// </summary>
	[SerializeField]
	private Animator defeatAnim;

	/// <summary>
	/// The slider background.
	/// </summary>
	[SerializeField]
	private Image sliderBackground;

	/// <summary>
	/// The defeat slider.
	/// </summary>
	[SerializeField]
	private Slider defeatSlider;

	/// <summary>
	/// The reload game text.
	/// </summary>
	[SerializeField]
	private Text reloadGameText;

	/// <summary>
	/// The exit game text.
	/// </summary>
	[SerializeField]
	private Text exitGameText;

	/// <summary>
	/// State of having been paused
	/// </summary>
	[SerializeField]
	private bool isPaused;

	/// <summary>
	/// State of player having been defeated
	/// </summary>
	[SerializeField]
	private bool isDefeated;

	/// <summary>
	/// Reveals slider background on mouse hover
	/// </summary>
	/// <param name="activate">If set to <c>true</c> activate.</param>
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

	/// <summary>
	/// Handles text display based on slider value
	/// </summary>
	public void sliderValueHandle()
	{

		if (defeatSlider.value == defeatSlider.maxValue) 
		{
			reloadGameText.gameObject.SetActive (true);
		} 
		else 
		{
			reloadGameText.gameObject.SetActive (false);
		}


		if (defeatSlider.value == defeatSlider.minValue) 
		{
			exitGameText.gameObject.SetActive (true);
		} 
		else 
		{
			exitGameText.gameObject.SetActive (false);
		}
	}

	/// <summary>
	/// Handles player input
	/// </summary>
	private void handleInput()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !isDefeated) 
		{
			pauseGame();
		}

		if (Input.GetMouseButtonUp (0) && (defeatSlider.value == defeatSlider.maxValue)) 
		{
			loadScene(0);
		}

		if (Input.GetMouseButtonUp (0) && (defeatSlider.value == defeatSlider.minValue)) 
		{
			exitGame();
		}
	}

	/// <summary>
	/// Handles game logic when paused
	/// </summary>
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

	/// <summary>
	/// Handles game logic when player makes contact with enemy
	/// </summary>
	public void lostGame()
	{
		player.GetComponent<PlayerController>().setIsPaused (true);
		isDefeated = true;

		pause_Panel.SetActive (false);
		defeat_Panel.SetActive (true);

		defeatAnim.enabled = true;
		defeatAnim.Play ("DefeatMenuActivate");

		Time.timeScale = 0.001f;
	}

	/// <summary>
	/// Handles exiting of game
	/// </summary>
	private void exitGame()
	{
		//UnityEditor.EditorApplication.isPlaying = false;
		Application.Quit();
	}

	/// <summary>
	/// Handles game scene navigation
	/// </summary>
	/// <param name="sceneIndex">Scene index.</param>
	public void loadScene(int sceneIndex)
	{
		//loadingImage.SetActive(true);
		SceneManager.LoadScene(sceneIndex);
	}
		
	// Use this for initialization
	void Start () {

		isDefeated = false;
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
