using UnityEngine;
using System.Collections;

/// <summary>
/// QuadController class handles quad scrolling and all children
/// of the Quad gameObject during runtime
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// 
/// </summary>
/// 
public class QuadController : MonoBehaviour {


	/// <summary>
	/// Speed of quad texture scrolling
	/// </summary>
	[SerializeField]
	private float scrollSpeed;

	/// <summary>
	/// Offset time for smooth scrolling resume on player reentry
	/// </summary>
	[SerializeField]
	private float currentOffsetTime;

	/// <summary>
	/// The whisper associated with this quad
	/// </summary>
	private GameObject thisWhisper;


	/// <summary>
	/// Scrolls the quad texture verticallly by scrollSpeed.
	/// </summary>
	private void scrollImage()
	{
		Vector2 offset = new Vector2 (0f, (currentOffsetTime * scrollSpeed) % 1 );
		currentOffsetTime += Time.deltaTime;
		GetComponent<Renderer>().material.mainTextureOffset = offset;
	}

	private void haltAllChildren()
	{
		foreach (Transform child in transform) 
		{
			child.GetComponent<IQuadChild>().halt();
		}
	}

	private void actuateAllChildren()
	{
		foreach (Transform child in transform) 
		{
			child.GetComponent<IQuadChild>().actuate();
		}
	}

	private void OnTriggerStay(Collider other)
	{

		if (other.gameObject.tag == "Player")// || thisWhisper.GetComponent<WhisperController>().getState())
		{
			scrollImage();
			actuateAllChildren();
		}

		if (thisWhisper.GetComponent<WhisperController>().getState()) 
		{
			scrollImage();
			actuateAllChildren();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player" || !thisWhisper.GetComponent<WhisperController>().getState()) 
		{
			haltAllChildren();
		}
	}
		
	// Use this for initialization
	void Start () {

		scrollSpeed = 0.2f;
		currentOffsetTime = Time.deltaTime;

		thisWhisper = GameObject.Find(gameObject.name + "_Whisper");
	}
	
	// Update is called once per frame
	void Update () {
	}
}
