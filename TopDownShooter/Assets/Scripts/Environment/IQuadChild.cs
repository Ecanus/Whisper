using UnityEngine;
using System.Collections;

/// <summary>
/// IQuadChild interface manages all shared methods of objects childed to a Quad
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// </summary>
///
public interface IQuadChild {

	/// <summary>
	/// Halt this instance.
	/// </summary>
	void halt();

	/// <summary>
	/// Actuate this instance.
	/// </summary>
	void actuate();
}
