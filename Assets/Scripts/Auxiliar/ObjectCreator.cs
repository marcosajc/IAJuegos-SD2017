using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator{
	
	public static void createProjectile(Vector3 vel, Vector3 pos){
		GameObject dummy = (GameObject)ProyectilController.Instantiate (Resources.Load ("Prefab/Ball2"));
		AgentMeta am = (AgentMeta)dummy.GetComponent<AgentMeta> ();
		am.setVelocity(vel);
		am.setPosition (pos);
	}

	public static void createAgent(Vector3 pos)
	{
		GameObject dummy = (GameObject)ProyectilController.Instantiate (Resources.Load ("Prefab/Agent_1"));
		AgentController am = (AgentController)dummy.GetComponent<AgentController> ();
		am.currentBehaviour = am.ListBehaviours[3];
	}
}
