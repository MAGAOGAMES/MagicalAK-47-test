using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using mak47.control;

namespace mak47.characters {
	public class Character : MonoBehaviour, camera.Focusable, control.Controllable {

		//	gameobjects
		GameObject body;
		Animator animator;
		Rigidbody rigidBody;

		public bool Controlled { set; get; }

		//	move
		bool isRunning = false;
		bool isWalking = false;
		[SerializeField] float moveSpeed;
		public float MoveSpeed { set { moveSpeed = value; } get { return moveSpeed; } }

		Vector3 velocity = Vector3.zero;
		Vector3 forward = Vector3.forward;

		//	vehicle
		bool usingVehicle = false;

		//	camera
		new camera.Camera camera = null;

		//	interact 
		Interactive interactive;
		InteractiveDetector interactiveDetector;

		// Use this for initialization
		void Start() {
			MoveSpeed = 5.0f;
			body = transform.GetChild(0).gameObject;
			animator = GetComponent<Animator>();

			rigidBody = body.GetComponent<Rigidbody>();

			//	TODO sk.13 Move to Player class
			Controlled = true;

			interactiveDetector = GetComponentInChildren<InteractiveDetector>();
		}

		// Update is called once per frame
		void Update( ) {
			if (!Controlled) return;
			var look = Quaternion.LookRotation(forward);
			body.transform.localRotation = Quaternion.Slerp(body.transform.localRotation, look, 0.25f) ;
			UpdateAnimation();
			transform.position = body.transform.position;
			//transform.rotation = body.transform.rotation;

			var ray = new Ray(transform.position, camera.GetDirectionInView(Vector3.forward)*5.0f);
			RaycastHit hitInfo;
			Physics.Raycast(ray, out hitInfo);

			interactiveDetector.UpdatePosition(camera);
		}

		private void FixedUpdate( ) {
			var pos = velocity * MoveSpeed * Time.fixedDeltaTime + transform.position;
			rigidBody.MovePosition(pos);
		}

		private void LateUpdate( ) {
		}

		void UpdateAnimation( ) { 
			animator.SetBool("isRunning", isRunning);
			animator.SetBool("isWalking", isWalking);
		}

		public void ControlByGamepad(Gamepad gamepad) {
			MoveByGamepad(gamepad.GetAxis(Gamepad.Axis.LeftStick));
			if(gamepad.GetButtonDown(Gamepad.PhysicalButton.Square)) {
				var interactive = interactiveDetector.InteractiveObject;
				if (interactive != null) {
					interactive.AcceptInteract(this);
				}
			}
		}

		public virtual void MoveByGamepad(Vector2 input) {
			var x = Mathf.Abs(input.x);
			var y = Mathf.Abs(input.y);
			if (x < 0.3f && y < 0.3f) {
				velocity = Vector3.zero;
				isRunning = false;
				isWalking = false;
				return;
			}
			var dir = new Vector3(input.x, 0.0f, input.y);
			if (camera != null) {
				dir = camera.GetDirectionInView(dir);
			}
			dir.y = 0.0f;

			velocity = dir;
			forward = dir != Vector3.zero ? dir : forward;
			isRunning = (velocity.x * velocity.x + velocity.z * velocity.z) > 0.5f;
			isWalking = !isRunning;
		}

		//	TODO sk.13 must be fixed to abstract. children implement this method 
		public virtual void AcceptCamera(camera.Camera camera) {
			this.camera = camera;
			camera.Offset = Vector3.up * 1.5f;
			camera.transform.parent = transform;
		}

		public camera.Camera GetCamera() {
			return camera;
		}

		public void TakeVehicle() {
			Controlled = false;
			usingVehicle = true;
		}
	}
}
