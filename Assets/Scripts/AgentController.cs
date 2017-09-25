using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : AgentMeta {

	private List<Behaviour> ListBehaviours;
	private Behaviour currentBehaviour;
	private PlayerController Player;

	void Awake(){

		shipSprite = Resources.LoadAll<Sprite> ("Sprites/Spaceship");	// Cargamos las imagenes para ser usadas
		var player = GameObject.Find("Player");
		
		Player = player.GetComponent<PlayerController> ();

		maxSpeed = Player.maxSpeed;								
		maxAcceleration = Player.maxAcceleration;				
		maxRotation = Player.maxRotation;						
		maxAngularAcceleration = Player.maxAngularAcceleration;

		// Inicializamos los comportamientos.
		ListBehaviours = new List<Behaviour>();

		ListBehaviours.Add (new Standby (Player, this));
		ListBehaviours.Add (new Seek(Player, this));
		ListBehaviours.Add (new Flee (Player, this));
		ListBehaviours.Add (new Arrive (Player, this, 5.0f, 1.0f, .1f));
		ListBehaviours.Add (new Pursue (Player, this, 5.0f)); 

		currentBehaviour = ListBehaviours [0];
	
	}

	// Update is called once per frame
	void Update () {

		SteeringOutput.SteeringOutput steering = new SteeringOutput.SteeringOutput ();

		currentBehaviour = ListBehaviours [Player.getCurrentBehaviour () % ListBehaviours.Count];
		print (currentBehaviour.getBehaviourName ());
		steering = currentBehaviour.getSteering ();

		this.linear = steering.linear;
		this.angular = steering.angular;

	}
}
