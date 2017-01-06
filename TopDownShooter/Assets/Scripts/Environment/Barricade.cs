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
	/// Target that barricade moves towards
	/// </summary>
	[SerializeField]
	protected GameObject target;

	/// <summary>
	/// Barricade state of being in motion
	/// </summary>
	protected bool isMoving;

	/// <summary>
	/// Barricade state of having a target to seek
	/// </summary>
	protected bool isTargetSet;

	/// <summary>
	/// Speed at which Barricade falls towards bottom of screen
	/// </summary>
	protected float fallSpeed;

	/// <summary>
	/// Randomised x axis offset of barricade
	/// </summary>
	protected float barricadeOffset;

	/// <summary>
	/// Moves barricade towards target destination
	/// </summary>
	protected abstract void seekTarget();

	/// <summary>
	/// Sets the target for Barricade to move towards
	/// </summary>
	protected abstract void setTarget();

	/// <summary>
	/// Sets the x axis offset of barricade
	/// </summary>
	protected abstract void setOffset();

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
