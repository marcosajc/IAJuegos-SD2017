﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forms {

	protected Vector2 center;
	protected float radius;

	protected List<Vector2> vertex;
	protected int countVertex;
	protected List<Vector2> sides;
	protected static string[] type = new string[] { "circle", "dot", "line", "triangle", "rectangle" };

	public Forms( List<Vector2> Vertex ){

		vertex = Vertex;
		countVertex = vertex.Count;

	}
	
	public Forms( Vector2 circleCenter, float circleRadius ){

		countVertex = 0;
		center = circleCenter;
		radius = circleRadius;

	}

	public bool belongsTo( Vector2 point ){

		if (countVertex == 0) { // Es un circulo.

			Vector2 newPoint = point - center;

			return (Mathf.Pow (newPoint.x, 2) + Mathf.Pow (newPoint.y, 2) == Mathf.Pow (radius, 2));
		} else {

			Forms insideTriangle = new Forms ( new List<Vector2> { vertex[countVertex-1], point, vertex[0]});

			float areaWithPoint = insideTriangle.getArea ();

			for (int i = 0; i < countVertex-1; i++){

				insideTriangle.setVertex (new List<Vector2> { vertex[i], point, vertex[i+1] });
				areaWithPoint += insideTriangle.getArea ();

			}

			return !(areaWithPoint > getArea ());


		}
	}

	private float getArea(){

		float area = (vertex [0].x - vertex [countVertex - 1].x) * (vertex [0].y + vertex [countVertex - 1].y) / 2;

		for (int i = 0; i < countVertex-1; i++)
			area += (vertex[i+1].x - vertex[i].x) * (vertex[i+1].y - vertex[i].y) / 2;

		return Mathf.Abs(area);

	}

	public void setVertex( List<Vector2> newVertex ){

		vertex = newVertex;
		countVertex = vertex.Count;

	}
}
