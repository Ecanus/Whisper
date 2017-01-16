using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// ImagePulse script provides basic image dimming functionality for UI elements
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// </summary>
///
public class ImagePulse : MonoBehaviour {

	[SerializeField]
	private Image sliderImage;

	private Color sliderImageColor;

	private float alphaMax;

	private float alphaMin;

	private float pulseDuration;

	private bool canLoopPulse;

	private IEnumerator pulse()
	{
		canLoopPulse = false;

		sliderImageColor = sliderImage.color;
		sliderImageColor.a = 0f;
			
		for (float f = alphaMin; f <= alphaMax; f += 0.02f) {

			sliderImageColor.a = f;
			sliderImage.color = sliderImageColor;
			yield return new WaitForSeconds(pulseDuration);
		}

		for (float f = alphaMax; f >= -0.1f; f -= 0.01f) {

			sliderImageColor.a = f;
			sliderImage.color = sliderImageColor;
			yield return new WaitForSeconds(pulseDuration);
		}

		canLoopPulse = true;

	}
	// Use this for initialization
	void Start () {
		canLoopPulse = true;

		alphaMax = 0.6f;
		alphaMin = 0f;
		pulseDuration = 0.05f; 

	}
	
	// Update is called once per frame
	void Update () {
	
		if (canLoopPulse) {
			StartCoroutine ("pulse");
		}
	}
}
