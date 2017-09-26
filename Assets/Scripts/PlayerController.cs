using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerController : AgentMeta {

	private int AgentCurrentBehaviour;

	void Awake(){

		shipSprite = Resources.LoadAll<Sprite> ("Sprites/Spaceship");	// Cargamos las imagenes para ser usadas
		AgentCurrentBehaviour = 0;

	}

	//FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
	void Update()
	{
		//Store the current horizontal input in the float moveHorizontal.
		float moveHorizontal = Input.GetAxis ("Horizontal");

		//Store the current vertical input in the float moveVertical.
		float moveVertical = Input.GetAxis ("Vertical");

		//Use the two store floats to create a new Vector2 variable movement.
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		//Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
		this.linear = (movement.normalized * maxAcceleration);

		if (Input.GetKeyDown ("f"))
			AgentCurrentBehaviour++;
		if (Input.GetKeyDown ("q"))
			angular = maxAngularAcceleration;
		if (Input.GetKeyDown ("e"))
			angular = -maxAngularAcceleration;
		if (Input.GetKeyDown ("z"))
			stop ();
		if (Input.GetKeyDown ("x"))
			fullStop ();
		
	}

	// Renderización de frames para poder identificar la velocidad del objeto.
	void LateUpdate(){

		if (getVelocity ().magnitude < .5 * maxSpeed)
			this.GetComponent<SpriteRenderer> ().sprite = shipSprite [3];

		else if (getVelocity ().magnitude >= .5 * maxSpeed && getVelocity ().magnitude < maxSpeed)
			this.GetComponent<SpriteRenderer> ().sprite = shipSprite [1];

		else
			this.GetComponent<SpriteRenderer> ().sprite = shipSprite [0];

	}

	public int getCurrentBehaviour(){

		return AgentCurrentBehaviour;

	}

}