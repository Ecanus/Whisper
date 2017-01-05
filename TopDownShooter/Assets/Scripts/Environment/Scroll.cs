using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {


	public float speed = 0.2f; 

	private float currentOffsetTime;

	private void scrollImage()
	{
		Vector2 offset = new Vector2 (0f, (currentOffsetTime * speed) % 1 );
		currentOffsetTime += Time.deltaTime;
		GetComponent<Renderer>().material.mainTextureOffset = offset;
	}

	private void OnTriggerStay(Collider other)
	{

		if (other.gameObject.tag == "Player")
		{
			scrollImage();
		}

	}
		

	// Use this for initialization
	void Start () {

		currentOffsetTime = Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
