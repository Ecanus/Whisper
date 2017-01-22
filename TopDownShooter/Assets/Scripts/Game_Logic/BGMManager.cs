using UnityEngine;
using System.Collections;

public class BGMManager : MonoBehaviour {

	// Use this for initialization
	void Awake () {

		DontDestroyOnLoad(gameObject);
	}
}