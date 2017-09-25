using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class Face : Behaviour {

	public Face () : base ("Face") {}
	public Face ( AgentMeta target, AgentMeta character ) : base( "Face", target, character ) {}

	public override SteeringOutput.SteeringOutput getSteering(){

		SteeringOutput.SteeringOutput steering = new SteeringOutput.SteeringOutput();

		Vector2 direction = Target.getPosition () - Character.getPosition ();

		//if 

		//steering.linear = steering.linear.normalized * Character.maxAcceleration;

		steering.angular = 0.0f;
		return steering;

	}

}
