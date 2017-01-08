using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WhisperController : MonoBehaviour {


	private bool whisperState;

	public void activateWhisper()
	{
		StartCoroutine (whisperStart() );
	}

	public IEnumerator whisperStart()
	{
		
		Image whisperImage = gameObject.GetComponent<Image>();
		Color whisperColor = whisperImage.color;
		whisperColor.a = 1f;
		whisperImage.color = whisperColor;

		gameObject.GetComponent<BoxCollider> ().enabled = true;
		whisperState = true;

		for (float f = 1f; f >= -0.1f; f -= 0.03f) {

			whisperColor.a = f;
			whisperImage.color = whisperColor;
			yield return new WaitForSeconds(0.05f);
		}

		gameObject.GetComponent<BoxCollider> ().enabled = false;
		whisperState = false;
	}

	public bool getState()
	{
		return whisperState;
	}


	// Use this for initialization
	void Start () {

		whisperState = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
