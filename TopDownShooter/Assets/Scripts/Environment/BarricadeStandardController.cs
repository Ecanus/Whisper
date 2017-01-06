using UnityEngine;
using System.Collections;

/// <summary>
/// BarricadeStandardController class manages all behaviour of standard barricades in-game
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// </summary>
///
public class BarricadeStandardController : Barricade {


	/// <summary>
	/// Moves enemy towards target destination
	/// </summary>
	protected override void seekTarget()
	{
		Vector2 from = transform.position;
		Vector2 to = target.transform.position;


		transform.position = Vector2.Lerp (from, to, fallSpeed);;
	}

	/// <summary>
	/// Sets the target for Barricade to move towards
	/// </summary>
	protected override void setTarget()
	{
		if (transform.parent != null) 
		{
			target = GameObject.Find (transform.parent.name + "Target");
			isTargetSet = true;
		} 

		else
			isTargetSet = false;
	}
		
	/// <summary>
	/// Randomly selects barricade x axis offset from three values
	/// </summary>
	protected override void setOffset()
	{
		float offsetVal_A = -1 * (transform.GetComponent<BoxCollider>().size.x);
		float offsetVal_B = (transform.GetComponent<BoxCollider>().size.x);

		barricadeOffset = Random.Range (offsetVal_A, offsetVal_B);

	
	}

	// Use this for initialization
	void Start () {

		setTarget();
		setOffset();
		fallSpeed = 0.005f;

	}
	
	// Update is called once per frame
	void Update () {

		if (isMoving && isTargetSet) 
		{
			seekTarget();
		} 

	}
}
