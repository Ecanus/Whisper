using UnityEngine;
using System.Collections;

/// <summary>
/// EnemyDigitController class manages Digit enemy types in-game
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// 
/// </summary>
/// 
public class EnemyDigitController : Enemy {

	/// <summary>
	/// Moves enemy towards target destination
	/// </summary>
	protected override void seekDestination()
	{
		Vector2 from = transform.position;
		Vector2 to = target.transform.position;

		transform.position = Vector2.Lerp (from, to, 0.02f);;
	}



	void Start () {

		target = GameObject.Find("Sprite_Player");
		player = GameObject.Find("Sprite_Player");

		healthValue = 1;
	}
	

	void Update () {
	
		if (isMoving) 
		{
			seekDestination();
		} 

	}

}
