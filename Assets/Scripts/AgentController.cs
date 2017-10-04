using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : AgentMeta {

	public List<Behaviour> ListBehaviours;
	public Behaviour currentBehaviour;
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
		AgentMeta[] targets = { Player };
		var Nodes = new List<Vector2> ();
		Nodes.Add (new Vector2 (-8.0f, 4.0f));
		Nodes.Add (new Vector2 (0.0f, 4.0f));
		Nodes.Add (new Vector2 (0.0f, 0.0f));
//		Nodes.Add (new Vector2 (-3.0f, -2.0f));
//		Nodes.Add (new Vector2 (0.0f, 0.0f));
		ListBehaviours.Add (new Standby (Player, this));
		//ListBehaviours.Add(new Separation ( this, targets, 1f, 0.2f ));
//		ListBehaviours.Add (new Seek(Player, this));
//		ListBehaviours.Add (new Flee (Player, this));
//		ListBehaviours.Add (new Arrive (Player, this, 5.0f, 1.0f, .1f));
//		ListBehaviours.Add (new Pursue (Player, this, 5.0f));
//		ListBehaviours.Add (new Evade (Player, this, 5.0f));
//		ListBehaviours.Add (new VelocityMatching (Player, this));
//		ListBehaviours.Add (new Align (Player, this, Mathf.PI/100, Mathf.PI/10000, .1f));
		ListBehaviours.Add (new Wander (this, .5f, .25f	, 1.0f, .0f));
//		ListBehaviours.Add (new Face (Player, this));
//		ListBehaviours.Add (new PathFollowing (this, Nodes, 10f));
//		ListBehaviours.Add (new PredictivePathFollowing (this, Nodes, 1f, 0.1f));
//		ListBehaviours.Add (new SeekWhileLooking( Player, this));
//		ListBehaviours.Add (new BlendedSteering( this, new List<Behaviour> { 

		currentBehaviour = ListBehaviours [0];
	
	}

	// Update is called once per frame
	void Update () {

		SteeringOutput.SteeringOutput steering = new SteeringOutput.SteeringOutput ();

		currentBehaviour = ListBehaviours [Player.getCurrentBehaviour () % ListBehaviours.Count];
		print (currentBehaviour.getBehaviourName ());
		//print (this.position);
		steering = currentBehaviour.getSteering ();

		this.linear = steering.linear;
		this.angular = steering.angular;

	}
}
