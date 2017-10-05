using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LWYG : Behaviour {

	public LWYG () : base ("Look where you're going") {}
	public LWYG ( AgentMeta character ) : base( "Look where you're going" ) {

		Character = character;

	}

	public override SteeringOutput.SteeringOutput getSteering()
	{
		SteeringOutput.SteeringOutput steering = new SteeringOutput.SteeringOutput();
		Vector2 velocity = Character.getVelocity ();

		if (velocity.magnitude == 0) { return steering; }

		GameObject dummy = (GameObject) MonoBehaviour.Instantiate (Resources.Load ("Prefab/Dummy"));
		AgentMeta aux = dummy.GetComponent<AgentMeta> ();
		aux.setPosition (new Vector2 (0.0f, 0.0f));
		aux.setOrientation((Mathf.Atan2(-velocity.x, velocity.y) * Mathf.Rad2Deg)%360.0f);
		Behaviour alinear = new Align (aux, Character, Mathf.PI/100, Mathf.PI/10, .1f);
		//steering.linear = steering.linear.normalized * Character.maxAcceleration;
		steering = alinear.getSteering();
		MonoBehaviour.Destroy (dummy);

		return steering;
	}
}
