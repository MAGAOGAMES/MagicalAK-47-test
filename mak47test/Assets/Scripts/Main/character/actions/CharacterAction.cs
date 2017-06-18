using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using mak47.control;
using System;

namespace mak47.characters.actions {
	public abstract class CharacterAction {
		protected Character character;
		protected CharacterAction(Character character) {
			this.character = character;
		}
		public virtual void Update() { }
		public virtual void FixedUpdate() { }
		public abstract void Move(Vector3 dir);
		public abstract void Fly();
		
		public virtual void OnEnable() { }
		public virtual void OnDisable() { }
	}

	public class NeutralAction : CharacterAction {
		public NeutralAction(Character character) : base(character) {
		}

		public override void FixedUpdate() {
			var rigidbody = character.rigidbody;
			var pos = character.transform.position;
			rigidbody.MovePosition(pos);
		}
		public override void Move(Vector3 dir) {
			character.action = character.move;
			character.move.Move(dir);
		}
		public override void Fly() { }
	}


	public class MoveAction : CharacterAction {
		public bool isRunning { get { return (velocity.x * velocity.x + velocity.z * velocity.z) > 0.75f; } }
		public bool isWalking {
			get {
				if (velocity.Equals(Vector3.zero)) return false;
				return !isRunning;
			}
		}

		Vector3 velocity;
		public MoveAction(Character character) : base(character) {
		}

		public override void Move(Vector3 velocity) {
			if (velocity.Equals(Vector3.zero)) {
				character.action = character.neutral;
				return;
			}

			var camera = character.GetCamera();
			if( camera != null) {
				velocity = camera.GetDirectionInView(velocity);
			}
			velocity.y = 0.0f;
			this.velocity = velocity.normalized;
		}

		public override void FixedUpdate() {
			var rigidbody = character.rigidbody;
			var pos = velocity * character.moveSpeed * Time.fixedDeltaTime + character.transform.position;
			rigidbody.MovePosition(pos);
		}

		public override void Fly() {

		}

		public override void OnDisable() {
			velocity = Vector3.zero;
		}
	}

	public class FlyAction : CharacterAction {
		Vector3 velocity;
		public bool isFlying { set; get; }
		public bool isFalling { set; get; }

		public FlyAction(Character character) : base(character) { }

		public override void Move(Vector3 velocity) {
			if (velocity.Equals(Vector3.zero)) {
				character.action = character.neutral;
				return;
			}

			var camera = character.GetCamera();
			if (camera != null) {
				velocity = camera.GetDirectionInView(velocity);
			}
			velocity.y = 0.0f;
			this.velocity = velocity.normalized;
		}

		public override void Fly() {
			
		}

		public override void FixedUpdate() {
			//	if flying
			var rigidbody = character.rigidbody;
			rigidbody.useGravity = false;
			var pos = velocity * character.flySpeed * Time.fixedDeltaTime + character.transform.position;
			rigidbody.MovePosition(pos);

			//	if falling
			rigidbody.useGravity = true;
			rigidbody.AddForce(Physics.gravity);
		}

		public override void OnDisable() {
			isFlying = false;
			isFalling = false;
		}
	}
}
