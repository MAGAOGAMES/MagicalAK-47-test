using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mak47.camera;
using mak47.control;
using mak47.characters;

namespace mak47.test {
	public class CameraTest : MonoBehaviour {

		[SerializeField] camera.Camera testCamera;
		[SerializeField] Character character;
	
		// Use this for initialization
		void Start( ) {
			testCamera = GameObject.Find("Camera").GetComponent<camera.Camera>();
			testCamera.Focus(character);
		}

		// Update is called once per frame
		void Update( ) {
			Gamepad gamepad = Gamepad.Instance;
			testCamera.RotateFromGamepadInput(gamepad.GetAxis(Gamepad.Axis.RightStick));
		}
	}
}
