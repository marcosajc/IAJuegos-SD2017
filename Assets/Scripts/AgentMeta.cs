using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AgentMeta : MonoBehaviour {
	#region Variables
	protected Sprite[] shipSprite;	// Sprites de las naves para la presentación

	/* Variables del movimiento */ 
	protected Vector3 position; 	// Posición del objeto.
	protected float orientation;	// Orientación del objeto.
	protected Vector3 velocity; 		// Velocidad lineal del objeto.
	protected float rotation;			// Velocidad de rotación del objeto.
	protected Vector3 linear;			// Aceleración lineal.
	protected float angular;		// Aceleración angular.

	/* Restricciones de movimiento */ 
	public float maxSpeed;					// Maxima velocidad.
	public float maxAcceleration;			// Maxima aceleración.
	public float maxRotation;				// Máxima velocidad angular.
	public float maxAngularAcceleration;	// Máxima aceleración angular.
	public float jumpVelocity;				// Velocidad maxima para iniciar saltos.

	/* Restricciones de vision */
	public float viewRadius;		// Radio de vision del agente.
	public float viewAngle;			// Angulos de vision (mid-plane).

	/* Herramientas para dibujar cosas cheveres */
	LineRenderer lineRender;		// Dibujador de lineas.
	#endregion


	void Start(){
		/* Inicializacion cinematica */
		position = transform.position;
		orientation = transform.eulerAngles.z;

		/* Inicializacion del Renderer de lineas 
		lineRender = gameObject.AddComponent<LineRenderer> ();
		lineRender.positionCount = 2;
		lineRender.startWidth = 0.1f;
		lineRender.endWidth = 0.5f;
		lineRender.startColor = Color.green;
		lineRender.endColor = Color.green;
		Material mat = new Material (Shader.Find ("Unlit/Texture"));	// Solo lo usamos para poder mostrar el fcking color.
		lineRender.material = mat;
		*/
		fullStop();
	}

	void OnGUI(){
		Vector2 size = new Vector2 (Random.Range(20, 200), Random.Range(20, 200));
		Vector2 center = new Vector2 (Random.Range(0,Screen.width), Random.Range(0,Screen.height)); 
		Texture2D tx = Texture2D.blackTexture;
		tx.Resize (Mathf.RoundToInt(size.x), Mathf.RoundToInt(size.y));
		Rect r1 = new Rect (center.x, center.y, size.x, size.y);
		GUI.Box (r1, tx);
	}

	void FixedUpdate(){
		linear.z = -0.98f;	// Gravedad

		// Determina la posición/orientación de acuerdo a la velocidad en el momento.
		position    += velocity * Time.deltaTime;
		orientation += rotation * Time.deltaTime;
		if (position.z < 0) { 
			position.z = 0;
		}
		// Determina la velocidad linear/angular de acuerdo a la aceleración.
		velocity += linear * Time.deltaTime;
		rotation += angular * Time.deltaTime;
		// Determino el angulo de la orientación.
		orientation = mod(orientation,360f);

		// Si de acuerdo al calculo supero la máxima velocidad, normalizo a la máxima velocidad
		if( velocity.magnitude > maxSpeed )
			velocity = velocity.normalized * maxSpeed;
		if (rotation > maxRotation)
			rotation = (rotation / Mathf.Abs (rotation)) * maxRotation;
		if (angular > maxAngularAcceleration) 
		{
			angular = (angular / Mathf.Abs (angular)) * maxAngularAcceleration;
		}
		transform.rotation = Quaternion.AngleAxis(orientation, Vector3.forward);
		transform.position = position;
		transform.localScale = new Vector3 (position.z + 1, position.z + 1);
		/*
		LineRenderer lineRenderer = GetComponent<LineRenderer> ();
		Vector3[] linePositions = { position, position + velocity };
		lineRenderer.SetPositions (linePositions);
		*/
	}

	public void stop(){
		velocity = new Vector3 (0.0f, 0.0f, 0f);
		rotation = 0.0f;
	}

	public void fullStop(){
		linear = new Vector3 (0.0f, 0.0f,0f);
		angular = 0.0f;
	}

	// Sets de las variables.

	public void setPosition( Vector2 newPosition ){

		position = newPosition;

	}

	public void setVelocity( Vector2 newVelocity ){

		velocity = newVelocity;

	}

//	public void setLinear( Vector2 newLinear ){
//
//		linear = newLinear;
//
//	}

	public void setOrientation( float newOrientation ){
		orientation = mod (orientation, 360f);
	}

	public void setRotation( float newRotation ){

		rotation = newRotation;

	}

//	public void setAngular( float newAngular ){
//
//		angular = newAngular;
//
//	}

	// Gets de las variables.

	public Vector2 getPosition(){

		return position;

	}

	public Vector2 getVelocity(){

		return velocity;

	}

	public Vector2 getLinear(){

		return linear;

	}

	public float getOrientation(){

		return orientation;

	}

	public float getRotation(){

		return rotation;

	}

	public float setAngular(){

		return angular;

	}

	private void OnDestroy()
	{
		//Destroy (lineRender.material);
	}

	private float mod( float m, float x){

		return (m % x + m) % x;

	}
		
}