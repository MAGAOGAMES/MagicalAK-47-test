using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using mak47.control;
using mak47.camera;

namespace mak47.characters {
	public class Player : Character, Controllable {

		public bool isControlled { set; get; }

		// Use this for initialization
		void Start() {
			InitializeComponents();
		}

		// Update is called once per frame
		void Update() {
			UpdateTransform();
			UpdateAnimation();
			interactiveDetector.UpdatePosition(camera);
		}

		private void FixedUpdate() {
			ActualMove();
		}

		public void ControlByGamepad(Gamepad gamepad) {
			MoveByGamepad(gamepad.GetAxis(Gamepad.Axis.LeftStick));
			if (gamepad.GetButtonDown(Gamepad.Button.Square)) {
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

		public override void AcceptCamera(camera.Camera camera) {
			base.AcceptCamera(camera);
			camera.Offset = Vector3.up * 1.5f;
			camera.transform.parent = transform;
		}
	}
}
