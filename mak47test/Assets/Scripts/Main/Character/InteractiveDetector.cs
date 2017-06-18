using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace mak47.characters {
	public class InteractiveDetector : MonoBehaviour {

		Interactive interactive;
		public Interactive InteractiveObject { get { return interactive; } }

		// Use this for initialization
		void Start() {
		}

		// Update is called once per frame
		void Update() {
			
		}

		private void LateUpdate() {
			interactive = null; 
		}

		public void UpdatePosition(camera.Camera camera) {
			var pos = camera.GetDirectionInView(Vector3.forward);
			pos.y = 0.0f;
			transform.localPosition = pos.normalized;
		}

		private void OnTriggerStay(Collider other) {
			var interactive = other.gameObject.GetComponent<Interactive>();
			if (interactive == null) return;
			this.interactive = interactive;
		}
	}
}
