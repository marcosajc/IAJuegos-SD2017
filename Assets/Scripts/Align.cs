using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class Align : Behaviour {

	public Align () : base ("Align") {}
	public Align( AgentMeta target, AgentMeta character, float SlowRadius, float TargetRadius, float TimeToTarget) 
		: base( "Align", target, character, SlowRadius, TargetRadius, TimeToTarget ) {}

	public override SteeringOutput.SteeringOutput getSteering(){

		Character.fullStop ();

		SteeringOutput.SteeringOutput steering = new SteeringOutput.SteeringOutput( new Vector2( 0.0f, 0.0f), 0.0f );

		float rotation = Character.getOrientation () - Target.getOrientation ();	// Podriamos utilizar Deg mejor ? (?)
		rotation *= Mathf.Deg2Rad - Mathf.PI;

		float rotationSize = Mathf.Abs (rotation);

		if (rotationSize < targetRadius)
			return steering;

		float targetRotation = Character.maxRotation;

		if (rotationSize <= slowRadius)
			targetRotation *= rotationSize / slowRadius;

		targetRotation *= rotation / rotationSize;

		steering.angular = targetRotation - Character.getRotation ();
		steering.angular /= timeToTarget;

		steering.linear = new Vector2( 0.0f, 0.0f);
		return steering;

	}

}
