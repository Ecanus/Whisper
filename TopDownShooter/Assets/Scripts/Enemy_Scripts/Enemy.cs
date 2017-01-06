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
	/// Moves enemy towards target destination
	/// </summary>
	protected abstract void seekDestination ();

	/// <summary>
	/// Name of quad that enemy was spawned in
	/// </summary>
	[SerializeField]
	protected string quadName;

	/// <summary>
	/// Enemy state of being in motion
	/// </summary>
	protected bool isMoving;

	/// <summary>
	/// Handles enemy behaviour when hit by bullet
	/// </summary>
	public virtual void isShot()
	{
		healthValue--;

		if ((healthValue <= 0))
		{
			player.gameObject.GetComponent<PlayerController>().increaseScore();
			Destroy (this.gameObject);
		}
	}

	public void halt()
	{
		isMoving = false;
	}

	public void actuate()
	{
		isMoving = true;
	}
}
