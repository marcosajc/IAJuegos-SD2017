﻿using System;
using UnityEngine;

namespace SteeringOutput
{
	public struct SteeringOutput
	{
		public Vector2 linear;
		public float angular;

		public SteeringOutput( Vector2 Linear, float Angular){

			linear = Linear;
			angular = Angular;

		}
	}
}

