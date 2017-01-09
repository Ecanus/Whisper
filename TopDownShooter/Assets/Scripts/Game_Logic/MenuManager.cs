using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour {

	private GameObject player;
	 
	public GameObject loadingImage;

	private bool isPaused;

	private void handleInput()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			pauseGame();
		}
	}

	public void pauseGame()
	{
		isPaused = !isPaused;
		transform.GetChild (0).gameObject.SetActive (isPaused);
		transform.GetChild (1).gameObject.SetActive (isPaused);

		if (isPaused) {
			Time.timeScale = 0;
			player.GetComponent<PlayerController>().setIsPaused (true);
		} else {
			Time.timeScale = 1;
			player.GetComponent<PlayerController>().setIsPaused (false);
		}
	}

	public void endGame()
	{
		transform.GetChild (0).gameObject.SetActive (true);
		transform.GetChild (1).gameObject.SetActive (false);
		transform.GetChild (2).gameObject.SetActive (true);

		Time.timeScale = 0;
	}



	public void LoadScene(int sceneIndex)
	{
		//loadingImage.SetActive(true);
		SceneManager.LoadScene(sceneIndex);
	}



	// Use this for initialization
	void Start () {

		Time.timeScale = 1f;
		player = GameObject.Find ("Sprite_Player");
		isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {

		handleInput();
	}
}
