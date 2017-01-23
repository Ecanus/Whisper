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
		int offset = Random.Range (0, 6);

		switch (offset) 
		{
		case 0:
			transform.Translate (Vector2.left * 1.5f);
			break;
		case 1:
			break;
		case 2:
			transform.Translate (Vector2.right * 1.0f);
			break;
		case 3:
			transform.Translate (Vector2.left * 1.0f);
			break;
		case 4:
			transform.Translate (Vector2.right * 0.5f);
			break;
		case 5:
			transform.Translate (Vector2.left * 0.5f);
			break;
		}

	}


	// Use this for initialization
	void Start () {

		setOffset();

	}

	// Update is called once per frame
	void Update () {

		if (isLaunched) {
			if (isMoving) {
				fall ();
			} 
		}

	}
}
