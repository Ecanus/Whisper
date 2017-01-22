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
	/// Speed at which Barricade falls towards bottom of screen
	/// </summary>
	public static float fallSpeed = 3f;


	/// <summary>
	/// Tells barricade to move in a downwards direction
	/// </summary>
	protected abstract void fall();

	/// <summary>
	/// Handles enemy behaviour when hit by bullet
	/// </summary>
	public virtual void OnTriggerStay(Collider other)
	{

		if (other.gameObject.CompareTag("Basin"))
		{
			Destroy (this.gameObject);
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
