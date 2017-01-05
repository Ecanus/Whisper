using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {


	public float speed = 0.2f; 

	private float currentTime;

	private void scrollImage()
	{
		Vector2 offset = new Vector2 (0, currentTime * speed);
		currentTime = Time.time;
		GetComponent<Renderer>().material.mainTextureOffset = offset;
	}

	private void OnTriggerStay(Collider other)
	{

		if (other.gameObject.tag == "Player")
		{
			scrollImage();
		}

	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log ("currentTime Enter is: " + currentTime);
	}

	private void OnTriggerExit(Collider other)
	{
		Debug.Log("CurrentTime Exit is: " + currentTime);
	}
		

	// Use this for initialization
	void Start () {

		//Camera cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
