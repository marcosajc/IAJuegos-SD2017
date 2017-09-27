using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringOutput;

public class PathFollowing : Behaviour {

	private Path path;
	private float PathOffset;
	private float currentParam;

	public PathFollowing ( AgentMeta character, List<Vector2> nodes, float pathOffset) 
		: base("Path Following") {

		Character = character;
		path = new Path (nodes);
		PathOffset = pathOffset;
		currentParam = 0.0f;

	}

	public override SteeringOutput.SteeringOutput getSteering() {

		currentParam = path.getParam (Character.getPosition (), currentParam);

		float targetParam = currentParam + PathOffset;

		GameObject dummy = (GameObject) MonoBehaviour.Instantiate (Resources.Load ("Prefab/Dummy"));
		AgentMeta dummyAgent = dummy.GetComponent<AgentMeta> ();
		dummyAgent.setPosition (path.getPosition (targetParam));

		Behaviour seek = new Seek (dummyAgent, Character);
		SteeringOutput.SteeringOutput steering = seek.getSteering ();

		MonoBehaviour.Destroy (dummy);
		return steering; 

	}
}

