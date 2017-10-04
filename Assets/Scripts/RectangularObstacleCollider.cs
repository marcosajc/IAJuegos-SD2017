using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangularObstacleCollider : ObstacleColliders {

	public float height;
	public float width;
	public bool drawBox;
	private Vector2 center;

	//public RectangleObstacleCollider ( float Height, float Wide, Vector2 Center ) 
		//: base( new List<Vector2> { new Vector2( Center.x - wide/2, Center.y 

	void Start() {

		center = transform.position;
		MonoBehaviour.print (center);

		vertex = new List<Vector2> { new Vector2 (center.x - width / 2, center.y - height / 2),
			new Vector2 (center.x + width / 2, center.y - height / 2),
			new Vector2 (center.x + width / 2, center.y + height / 2),
			new Vector2 (center.x - width / 2, center.y + height / 2)
		};

		objCollider = new BoundingBox (vertex);

		//if (drawBox) {
			//Texture2D tx = Texture2D.blackTexture;
			//tx.Resize (Mathf.RoundToInt (size.x), Mathf.RoundToInt (size.y));
			//tx.Resize( 200, 200);
			//Rect r1 = new Rect (vertex[0].x, vertex[0].y, width, height);
			//GUI.Box (r1, tx);

			//objCollider.drawForm (Color.green);

		//}

	}

}
