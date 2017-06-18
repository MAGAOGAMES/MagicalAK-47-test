using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using mak47.control;

namespace mak47.characters {
	public abstract class Character : MonoBehaviour, camera.Focusable {

		//	components
		protected GameObject body;
		protected Animator animator;
		protected Rigidbody rigidBody;

		//	move
		protected bool isRunning = false;
		protected bool isWalking = false;
		[SerializeField] protected float moveSpeed;
		public float MoveSpeed { set { moveSpeed = value; } get { return moveSpeed; } }

		protected Vector3 velocity = Vector3.zero;
		protected Vector3 forward = Vector3.forward;

		//	vehicle
		protected bool usingVehicle = false;

		//	camera
		protected new camera.Camera camera = null;

		//	interact 
		protected InteractiveDetector interactiveDetector;

		// Use this for initialization
		private void Start() {
			MoveSpeed = 5.0f;
			InitializeComponents();
		}
	

		// Update is called once per frame
		void Update( ) {
			
		}

		protected virtual void InitializeComponents() {
			body = transform.GetChild(0).gameObject;
			animator = GetComponent<Animator>();
			rigidBody = body.GetComponent<Rigidbody>();
			interactiveDetector = GetComponentInChildren<InteractiveDetector>();
		}
		
		protected virtual void UpdateTransform() {
			var look = Quaternion.LookRotation(forward);
			body.transform.localRotation = Quaternion.Slerp(body.transform.localRotation, look, 0.25f);
			UpdateAnimation();
			transform.position = body.transform.position;
		}

		protected virtual void ActualMove() {
			var pos = velocity * MoveSpeed * Time.fixedDeltaTime + transform.position;
			rigidBody.MovePosition(pos);
		}

		private void FixedUpdate( ) {
			ActualMove();
		}

		protected virtual void UpdateAnimation( ) { 
			animator.SetBool("isRunning", isRunning);
			animator.SetBool("isWalking", isWalking);
		}


		//	TODO sk.13 must be fixed to abstract. children implement this method 
		public virtual void AcceptCamera(camera.Camera camera) {
			this.camera = camera;
		}

		public camera.Camera GetCamera() {
			return camera;
		}

		public virtual void TakeVehicle() { 
			usingVehicle = true;
		}
	}
}
