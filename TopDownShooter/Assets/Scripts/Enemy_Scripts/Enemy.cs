using UnityEngine;
using System.Collections;


/// <summary>
/// EnemyController parent class manages all generalised methods and attributes
/// for enemy subclasses
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// </summary>
/// 
abstract public class Enemy : MonoBehaviour, IQuadChild {

	/// <summary>
	/// Points awarded to player when this enemy is defeated
	/// </summary>
	public static float enemyValue = 1f;

	/// <summary>
	/// Player gameobject
	/// </summary>
	[SerializeField]
	protected GameObject player;

	/// <summary>
	/// Target that enemy moves towards
	/// </summary>
	[SerializeField]
	protected GameObject target;

	/// <summary>
	/// Health value of enemy
	/// </summary>
	[SerializeField]
	protected int healthValue;

	/// <summary>
	/// Speed at which Enemy falls towards bottom of screen
	/// </summary>
	[SerializeField]
	protected float fallSpeed;

	/// <summary>
	/// Enemy state of being in motion
	/// </summary>
	[SerializeField]
	protected bool isMoving;

	[SerializeField]
	protected bool isKilled;

	/// <summary>
	/// Moves enemy towards target destination
	/// </summary>
	protected abstract void seekTarget();

	/// <summary>
	/// Handles enemy behaviour when hit by bullet
	/// </summary>
	public virtual void isShot()
	{
		/* If hit, decrease health */
		healthValue--;

		if ((healthValue <= 0))
		{
			player.gameObject.GetComponent<PlayerController>().increaseScore(enemyValue);
			isKilled = true;
		}
	}
		

	/// <summary>
	/// Fades the out enemy upon defeat. Destroys object once fade is complete
	/// </summary>
	protected void fadeOut()
	{
		//gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Enemy_Images/Enemy_Digit_Defated");
		Color enemyColor = gameObject.GetComponent<SpriteRenderer> ().color;
		enemyColor = new Color (1f, 1f, 1f, Mathf.SmoothStep (1f, 0f, 0.5f));
		gameObject.GetComponent<SpriteRenderer> ().color = enemyColor;
		StartCoroutine ("destroyEnemy");

	}


	private IEnumerator destroyEnemy()
	{
		yield return new WaitForSeconds (0.060f);
		Destroy (this.gameObject);
	}
		

	/// <summary>
	/// IQuadChild Method for halting motion
	/// </summary>
	public void halt()
	{
		isMoving = false;
	}

	/// <summary>
	/// IQuadChild Methord for actuating motion
	/// </summary>
	public void actuate()
	{
		isMoving = true;
	}
}
