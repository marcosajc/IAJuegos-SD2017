using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class Wander : Behaviour {

	private float wanderOffset;
	private float wanderRadius;

	private float wanderRate;
	private float wanderOrientation;

	public Wander () : base ("Wander") {}
	public Wander ( AgentMeta character, float offset, float radius, float rate, float orientation ) 
		: base ("Wander") {

		Character = Character;
		wanderOffset = offset;
		wanderRadius = radius;
		wanderRate = rate;
		wanderOrientation = orientation;

	}

	public override SteeringOutput.SteeringOutput getSteering ()
	{
		wanderOrientation += base.randomBinomial () * wanderRate;

		float targetOrientation = wanderOrientation + Character.getOrientation ();

		float targetOrientationRadian = targetOrientation * Mathf.Deg2Rad;
		float orientationRadian = Character.getOrientation () * Mathf.Deg2Rad;

		Vector2 target = Character.getPosition ()
		                 + wanderOffset * new Vector2 (Mathf.Cos (orientationRadian), Mathf.Sin (orientationRadian))
		                 + wanderRadius * new Vector2 (Mathf.Cos (targetOrientationRadian), Mathf.Sin (targetOrientationRadian));

		GameObject dummy = (GameObject) MonoBehaviour.Instantiate (Resources.Load ("Prefab/Dummy"));
		AgentMeta dummyAgent = dummy.GetComponent<AgentMeta> ();
		dummyAgent.setPosition (target);

		Behaviour face = new Face (dummyAgent, Character);

		SteeringOutput.SteeringOutput steering = face.getSteering ();

		steering.linear = Character.maxAcceleration * new Vector2 (Mathf.Cos (orientationRadian), Mathf.Sin (orientationRadian));

		MonoBehaviour.Destroy (dummy);
		return steering;

	}

}

