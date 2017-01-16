using UnityEngine;
using System.Collections;

/// <summary>
/// GameManager class manages functionality not specific to existing scripts or gameobjects
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// 
/// </summary>
/// 
public class GameManager : MonoBehaviour {

	private IEnumerator lowerMusic()
	{
		for (float f = 0.3f; f > 0.1f; f -= 0.015f) {

			yield return new WaitForSeconds(0.03f);
		}
		gameObject.GetComponent<GameManager> ().enabled = false;
	}
	// Use this for initialization
	void Start () {
		StartCoroutine ("lowerMusic");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
