using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrioritySteering : Behaviour {

	private List<BlendedSteering> groups;
	private float epsilon;

	public PrioritySteering ( List<BlendedSteering> Groups, float Epsilon) : base ( "Priority Steering" ) {

		groups = Groups;
		epsilon = Mathf.Abs(Epsilon);

	}

	public override SteeringOutput.SteeringOutput getSteering ()
	{
		SteeringOutput.SteeringOutput steering;

		for (int i = 0; i < groups.Count; i++) {

			steering = groups [i].getSteering ();

			if (steering.linear.magnitude > epsilon || Mathf.Abs (steering.angular) > epsilon)
				return steering;

		}

		return steering;
	}
}
