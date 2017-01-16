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
	/// Moves Barricade in downwoards direction
	/// </summary>
	protected override void fall()
	{
		transform.Translate(Vector3.down * Time.deltaTime * fallSpeed);
	}
		

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (isMoving) 
		{
			fall();
		} 

	}
}
