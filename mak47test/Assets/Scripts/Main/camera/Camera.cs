using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mak47.camera {

	public class Camera : MonoBehaviour {

		new UnityEngine.Camera camera;
		Focusable focusTarget;
		public Focusable Target { set { Focus(value); } get { return focusTarget; } }
		public Vector3 Offset { set; get; }

		public float RotateSensitivity { set; get; }

		Vector2 destAngles;
		Vector2 angles;
		public Vector2 Angles { set { destAngles = value; } get { return angles; } }

		float destDistance = 1.5f;
		float distance = 5.0f;
		public float Distance { set { destDistance = value; } get { return distance; } }
	
		// Use this for initialization
		void Start( ) {
			camera = GetComponentInChildren<UnityEngine.Camera>();
			camera.farClipPlane = 2000.0f;
		}

		// Update is called once per frame
		void Update( ) {
			destDistance = Mathf.Clamp(destDistance, 1.0f, 20.0f);
			angles.x = Mathf.Clamp(angles.x, -0.45f, 0.45f);
			angles += (destAngles - angles) * 0.25f;
			distance += (destDistance - distance) * 0.25f;
			float cos = Mathf.Cos(angles.x);
			camera.transform.localPosition = new Vector3(Mathf.Sin(angles.y)* cos, Mathf.Sin(angles.x), Mathf.Cos(angles.y)* cos) * distance;
			camera.transform.LookAt(transform);

			transform.localPosition = Offset;
			focusTarget = GetComponentInParent<Focusable>();
		}

		public Vector3 GetDirectionInView(Vector3 dir ) {
			return camera.transform.TransformDirection(dir);
		}

		public void SetDistance( float distance ) {
			transform.localScale = new Vector3(distance, distance, distance);
		}

		public void Rotate( Quaternion rotation ) {
			transform.localRotation = rotation;
		}

		public void Yawing( float angle ) {
			angles.y += angle;
		}

		public void Pitch( float angle ) {
			angles.x += angle;
		}

		public void RotateFromGamepadInput( Vector2 input ) {
			Angles += new Vector2(input.y, input.x) ;
		}

		public void Focus(Focusable target) {
			focusTarget = target;
			target.AcceptCamera(this);
			transform.position = Vector3.zero;
			transform.rotation = Quaternion.identity;
		}
	}
}