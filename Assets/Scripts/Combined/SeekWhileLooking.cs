using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class SeekWhileLooking : Behaviour {

	public SeekWhileLooking () : base ("Seek while looking") {}
	public SeekWhileLooking( AgentMeta target, AgentMeta character ) : base( "Seek while looking", target, character ) {}

	public override SteeringOutput.SteeringOutput getSteering(){

		SteeringOutput.SteeringOutput steering = new SteeringOutput.SteeringOutput();

		steering.linear = Target.getPosition () - Character.getPosition ();

		steering.linear = steering.linear.normalized * Character.maxAcceleration;

		steering.angular = 0.0f;

		Behaviour lwyg = new LWYG (Character);
		steering += lwyg.getSteering ();

		return steering;

	}

}
