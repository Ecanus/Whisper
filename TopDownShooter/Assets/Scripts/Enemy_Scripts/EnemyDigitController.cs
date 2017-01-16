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
	protected override void seekTarget()
	{
		Vector2 from = transform.position;
		Vector2 to = target.transform.position;

		transform.position = Vector2.Lerp (from, to, (Time.deltaTime * fallSpeed));

	}
		

	void Start () {

		player = GameObject.Find("Sprite_Player");
		target = player;

		fallSpeed = 1.2f;
		healthValue = 1;
	}
	

	void Update () {
	
		if (isMoving) {
			seekTarget ();
		} 

		if (isKilled) 
		{
			gameObject.GetComponent<SpriteRenderer> ().sprite = SpawnPointController.getDigitDefeatedSprite ();
			fadeOut();
		}
	}

}
