using UnityEngine;
using System.Collections;

/// <summary>
/// Barricade parent class manages all generalised methods and attributes
/// for barricade subclasses
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// </summary>
///
abstract public class Barricade : MonoBehaviour, IQuadChild {


	/// <summary>
	/// Barricade state of being in motion
	/// </summary>
	protected bool isMoving;

	/// <summary>
	/// The spawn origin.
	/// </summary>
	protected GameObject spawnOrigin;

	/// <summary>
	/// State of being allowed to move from spawnPoint
	/// </summary>
	protected bool isLaunched;

	/// <summary>
	/// Speed at which Barricade falls towards bottom of screen
	/// </summary>
	public static float fallSpeed = 3f;


	/// <summary>
	/// Tells barricade to move in a downwards direction
	/// </summary>
	protected abstract void fall();

	/// <summary>
	/// Sets the spawn point of the Barricade
	/// </summary>
	/// <param name="spawnPoint">Spawn point.</param>
	public virtual void setSpawnPoint(GameObject spawnPoint)
	{
		spawnOrigin = spawnPoint;
	}

	/// <summary>
	/// Launch this instance of Barricade from the spawnPoint
	/// </summary>
	public virtual void launch()
	{
		isLaunched = true;
	}

	/// <summary>
	/// Handles enemy behaviour when hit by bullet
	/// </summary>
	public virtual void OnTriggerStay(Collider other)
	{

		if (other.gameObject.CompareTag("Basin"))
		{
			transform.position = spawnOrigin.transform.position;
			isLaunched = false;
		}
	}

	/// <summary>
	/// IQuadChild Method for halting motion
	/// </summary>
	public void halt()
	{
		isMoving = false;
	}

	/// <summary>
	/// IQuadChild Methord fo actuating motion
	/// </summary>
	public void actuate()
	{
		isMoving = true;
	}

}
