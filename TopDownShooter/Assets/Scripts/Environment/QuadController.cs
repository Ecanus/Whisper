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
	private float scrollSpeed;

	/// <summary>
	/// Offset time for smooth scrolling resume on player reentry
	/// </summary>
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

	private void haltAllChildren()
	{
		foreach (Transform child in transform) 
		{
			if (child.transform.tag == "Enemy") 
			{
				child.GetComponent<Enemy> ().halt ();
			}

			if (child.transform.tag == "SpawnPoint") 
			{
				child.GetComponent<SpawnPointController>().halt ();
			}
		}
	}

	private void actuateAllChildren()
	{
		foreach (Transform child in transform) 
		{
			if (child.transform.tag == "Enemy") 
			{
				child.GetComponent<Enemy>().actuate();
			}

			if (child.transform.tag == "SpawnPoint") 
			{
				child.GetComponent<SpawnPointController> ().actuate();
			}
		}
	}

	private void OnTriggerStay(Collider other)
	{

		if (other.gameObject.tag == "Player")
		{
			scrollImage();
			actuateAllChildren();
		}

	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player") 
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
