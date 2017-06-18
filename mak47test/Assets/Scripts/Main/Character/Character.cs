using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using mak47.characters.actions;

namespace mak47.characters {
	public abstract class Character : MonoBehaviour, camera.Focusable {

		//	components
		protected GameObject body;
		public new Rigidbody rigidbody { protected set; get; }
		public Animator animator { protected set; get; }
		
		//	move
		protected bool isRunning = false;
		protected bool isWalking = false;
		[SerializeField] protected float speed;
		public float moveSpeed { set { speed = value; } get { return speed; } }
		public float flySpeed { set; get; }

		protected Vector3 velocity = Vector3.zero;
		protected Vector3 forward = Vector3.forward;

		//	vehicle
		protected bool usingVehicle = false;

		//	camera
		protected new camera.Camera camera = null;

		//	interact 
		protected InteractiveDetector interactiveDetector;

		//	actions
		protected CharacterAction currentAction;
		public CharacterAction action {
			set {
				value.OnDisable();
				currentAction = value;
				currentAction.OnEnable();
			}
			get { return currentAction; }
		}
		public NeutralAction neutral { set; get; }
		public MoveAction move { set; get; }
		

		// Use this for initialization
		private void Start() {
			flySpeed = moveSpeed;
			moveSpeed = 5.0f;
			Initialize();
		}
	

		// Update is called once per frame
		void Update( ) {
			currentAction.Update();
		}

		private void FixedUpdate() {
			currentAction.FixedUpdate();
			//ActualMove();
		}

		protected virtual void Initialize() {
			body = transform.GetChild(0).gameObject;
			animator = GetComponent<Animator>();
			rigidbody = body.GetComponent<Rigidbody>();
			interactiveDetector = GetComponentInChildren<InteractiveDetector>();

			neutral = new NeutralAction(this);
			move = new MoveAction(this);
			action = neutral;
		}
		
		protected virtual void UpdateTransform() {
			var look = Quaternion.LookRotation(forward);
			body.transform.localRotation = Quaternion.Slerp(body.transform.localRotation, look, 0.25f);
			UpdateAnimation();
			transform.position = body.transform.position;
		}

		protected virtual void ActualMove() {
			var pos = velocity * moveSpeed * Time.fixedDeltaTime + transform.position;
			rigidbody.MovePosition(pos);
		}



		protected virtual void UpdateAnimation( ) {
			//animator.SetBool("isRunning", isRunning);
			//animator.SetBool("isWalking", isWalking);
			animator.SetBool("isRunning", move.isRunning);
			animator.SetBool("isWalking", move.isWalking);
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
