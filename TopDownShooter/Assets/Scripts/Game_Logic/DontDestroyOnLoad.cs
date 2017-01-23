using UnityEngine;
using System.Collections;
/// <summary>
/// Script managing GameObject persitence between scene loads
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// 
/// </summary>
///
public class DontDestroyOnLoad : MonoBehaviour {

	// Use this for initialization
	void Awake () {

		DontDestroyOnLoad(gameObject);
	}
}