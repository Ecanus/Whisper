using UnityEngine;
using System.Collections;

/// <summary>
/// BlockController class manages all behaviour of standard barricades in-game
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// </summary>
///
public class BlockController : Barricade {


	/// <summary>
	/// Moves barricade in downwards direction
	/// </summary>
	protected override void fall()
	{
		transform.Translate(Vector3.down * Time.deltaTime * fallSpeed);
	}
		

	/// <summary>
	/// Randomly selects block x axis offset from range of values
	/// </summary>
	private void setOffset()
	{
		if (transform.parent != null) 
		{
			barricadeOffset = 1f;

			Vector3 pos = transform.position;
			Debug.Log ("Second Pos is: " + pos);
			//pos.x += barricadeOffset;
			//transform.Translate (pos);
		
		}


	}

	// Use this for initialization
	void Start () {

		Debug.Log ("First Pos is: " + transform.position);
		setOffset();
		fallSpeed = 2f;

	}

	// Update is called once per frame
	void Update () {

		if (isMoving) 
		{
			fall();
		} 

	}
}
