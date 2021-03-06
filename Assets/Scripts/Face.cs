﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class Face : Behaviour {

	public Face () : base ("Face") {}
	public Face ( AgentMeta target, AgentMeta character ) : base( "Face", target, character ) {}

	public override SteeringOutput.SteeringOutput getSteering(){
		SteeringOutput.SteeringOutput steering = new SteeringOutput.SteeringOutput( new Vector2(.0f, .0f), .0f);
		Vector2 dir = Target.getPosition () - Character.getPosition ();
		if (dir.magnitude == 0) 
		{
			return steering;
		}
		Debug.Log (dir);

		GameObject dummy = (GameObject) MonoBehaviour.Instantiate (Resources.Load ("Prefab/Dummy"));
		AgentMeta aux = dummy.GetComponent<AgentMeta> ();
		aux.setPosition (Target.getPosition ());
		aux.setOrientation(Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg);
		Debug.Log (Mathf.Atan2 (-dir.x, dir.y) * Mathf.Rad2Deg);
		Behaviour alinear = new Align (aux, Character, Mathf.PI/10, Mathf.PI/100, .1f);
		//steering.linear = steering.linear.normalized * Character.maxAcceleration;
		steering = alinear.getSteering();
		Debug.Log (steering.angular);
		MonoBehaviour.Destroy (dummy);
		return steering;

	}

}
