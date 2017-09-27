using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class Evade : Behaviour {

	public Evade () : base ("Evade") {}
	public Evade ( AgentMeta target, AgentMeta character, float MaxPrediction ) 
		: base ( "Evade", target, character, MaxPrediction) {}

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
		dummyAgent.setPosition (Target.getPosition() + Target.getVelocity () * prediction);

		Behaviour flee = new Flee( dummyAgent, Character);

		SteeringOutput.SteeringOutput steering = flee.getSteering();

		MonoBehaviour.Destroy (dummy);		
		return steering;

	}

}
