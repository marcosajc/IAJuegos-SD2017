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

		Character = character;
		wanderOffset = offset;
		wanderRadius = radius;
		wanderRate = rate;
		wanderOrientation = orientation;

	}

	public override SteeringOutput.SteeringOutput getSteering ()
	{
		wanderOrientation += base.randomBinomial () * wanderRate;
		//Debug.Log (wanderOrientation);

		float targetOrientation = wanderOrientation + Character.getOrientation ();

		float targetOrientationRadian = targetOrientation * Mathf.Deg2Rad;
		float orientationRadian = Character.getOrientation () * Mathf.Deg2Rad;
		//Debug.Log (targetOrientationRadian);
		//Debug.Log (targetOrientation);

		Vector2 target = Character.getPosition ()
			+ wanderOffset * new Vector2 (Mathf.Cos (orientationRadian), Mathf.Sin (orientationRadian)).normalized
			+ wanderRadius * new Vector2 (Mathf.Cos (targetOrientationRadian), Mathf.Sin (targetOrientationRadian)).normalized;
		//Debug.Log (Mathf.Cos (180f * Mathf.Deg2Rad));
		//Debug.Log(new Vector2 (Mathf.Cos (targetOrientationRadian), Mathf.Sin (targetOrientationRadian)));

		//Debug.Log (target);

		GameObject dummy = (GameObject) MonoBehaviour.Instantiate (Resources.Load ("Prefab/Dummy"));
		AgentMeta dummyAgent = dummy.GetComponent<AgentMeta> ();
		dummyAgent.setPosition (target);
		dummyAgent.setOrientation (wanderOrientation + Character.getOrientation());
		//dummyAgent.setOrientation ( 

		//Debug.Log (dummyAgent.getPosition ()- Character.getPosition());

		//Behaviour face = new Face (dummyAgent, Character);
		//Debug.Log (dummyAgent.getPosition () - Character.getPosition());
		//Debug.Log (dummyAgent.getOrientation ());
		//Debug.Log (Character.getOrientation ());

		Behaviour seek = new SeekWhileLooking (dummyAgent, Character);
		//Behaviour lwyg = new LWYG (Character);

		//SteeringOutput.SteeringOutput steering = seek.getSteering () + lwyg.getSteering();
		SteeringOutput.SteeringOutput steering = seek.getSteering();
		Debug.Log (steering.angular);

		//steering.linear = Character.maxAcceleration * new Vector2 (Mathf.Cos (orientationRadian), Mathf.Sin (orientationRadian));

		MonoBehaviour.Destroy (dummy);
		return steering;

	}

}

