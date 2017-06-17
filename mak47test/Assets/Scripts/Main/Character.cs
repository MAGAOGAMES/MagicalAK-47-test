using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mak47 {
	public class Character : MonoBehaviour, camera.Focusable {
		Animator animator;

		GameObject body;
		bool isMoving = false;
		Vector3 forward = Vector3.forward;

		Rigidbody rigidBody;

		[SerializeField] float moveSpeed;
		public float MoveSpeed { set { moveSpeed = value; } get { return moveSpeed; } }
		Vector3 velocity = Vector3.zero;

		new camera.Camera camera = null;
		float groundHeight = 0.0f;

		// Use this for initialization
		void Start( ) {
			MoveSpeed = 5.0f;
			body = transform.GetChild(0).gameObject;
			animator = GetComponent<Animator>();

			rigidBody = body.GetComponent<Rigidbody>();
		}

		// Update is called once per frame
		void Update( ) {
			var look = Quaternion.LookRotation(forward);
			body.transform.localRotation = Quaternion.Slerp(body.transform.localRotation, look, 0.25f) ;
			UpdateAnimation();
			transform.position = body.transform.position;
			//transform.rotation = body.transform.rotation;
		}

		private void FixedUpdate( ) {
			var ray = new Ray(transform.position + Vector3.up * 20.0f, Vector3.up*-1.0f);
			var hit = Physics.RaycastAll(ray, 20000.0f);
			foreach( var obj in hit) {
				if( obj.transform.gameObject.name.Contains("Ground")) {
					groundHeight = obj.point.y;
				}
			}
			var pos = velocity * MoveSpeed * Time.fixedDeltaTime + transform.position;
			pos.y = groundHeight;
			Debug.Log(groundHeight);
			rigidBody.MovePosition(pos);
		}

		private void LateUpdate( ) {
		}

		void UpdateAnimation( ) {
			animator.SetBool("isRunning", isMoving);
		}

		public void MoveFromGamepad( Vector2 input ) {
			Vector3 dir = new Vector3(input.x, 0.0f, input.y);
			if(camera != null) {
				dir = camera.GetDirectionInView(dir);
			}
			dir.y = 0.0f;
			velocity = dir;
			forward = dir != Vector3.zero ? dir : forward;
			isMoving = Mathf.Abs(dir.x) > 0.0f || Mathf.Abs(dir.z) > 0.0f;
		}

		public void AcceptCamera( camera.Camera camera ) {
			this.camera = camera;
			camera.Offset = Vector3.up * 1.5f;
			camera.transform.parent = transform;
		}
	}
}
