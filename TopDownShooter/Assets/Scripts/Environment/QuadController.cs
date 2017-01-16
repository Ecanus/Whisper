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
	/// Scrolls the quad texture verticallly by scrollSpeed.
	/// </summary>
	private void scrollImage()
	{
		Vector2 offset = new Vector2 (0f, (currentOffsetTime * scrollSpeed) % 1 );
		currentOffsetTime += Time.deltaTime;
		GetComponent<Renderer>().material.mainTextureOffset = offset;
	}

	/// <summary>
	/// Calls the halt() method of all children of this Quad
	/// </summary>
	private void haltAllChildren()
	{
		foreach (Transform child in transform) 
		{
			child.GetComponent<IQuadChild>().halt();
		}
	}

	/// <summary>
	/// Calls the actuate() method of all children of this Quad
	/// </summary>
	private void actuateAllChildren()
	{
		foreach (Transform child in transform) 
		{
			child.GetComponent<IQuadChild>().actuate();
		}
	}

	/// <summary>
	/// Calls the actuate() method of only non-enemy children of this Quad
	/// </summary>
	private void actuateAllBarricades()
	{
		foreach (Transform child in transform) 
		{
			if (!child.CompareTag("Enemy")) 
			{
				child.GetComponent<IQuadChild> ().actuate ();
			}
		}
	}

	private void OnTriggerStay(Collider other)
	{

		if (other.gameObject.CompareTag ("Player"))
		{
			scrollImage();
			actuateAllChildren();
		}

		if (other.gameObject.CompareTag("Whisper"))
		{
			actuateAllBarricades();
		}
	}



	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Whisper")) 
		{
			haltAllChildren();
		}
	}
		
	// Use this for initialization
	void Start () {

		scrollSpeed = 0.2f;
		currentOffsetTime = Time.deltaTime;
	
	}
	
	// Update is called once per frame
	void Update () {
	}
}
