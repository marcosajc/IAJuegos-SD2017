using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class Pursue : Behaviour {

	public Pursue () : base ("Pursue") {}
	public Pursue ( AgentMeta target, AgentMeta character, float MaxPrediction ) 
		: base ( "Pursue", target, character, MaxPrediction) {}

	public override SteeringOutput.SteeringOutput getSteering(){

		Vector2 direction = Target.getPosition () - Character.getPosition ();
		float distance = direction.magnitude;

		float speed = Character.getVelocity ().magnitude;

		float prediction;
		if (speed <= distance / maxPrediction)
			prediction = maxPrediction;
		else
			prediction = distance / maxPrediction;

		GameObject dummy = (GameObject) MonoBehaviour.Instantiate (Resources.Load ("Prefab/Dummy"));
		AgentMeta dummyAgent = dummy.GetComponent<AgentMeta> ();
		dummyAgent.setPosition (new Vector2 (0.0f, 0.0f) + Target.getVelocity () * prediction);

		Behaviour seek = new Seek( dummyAgent, Character);

		SteeringOutput.SteeringOutput steering = seek.getSteering();

		MonoBehaviour.Destroy (dummy);		
		return steering;

	}

}
