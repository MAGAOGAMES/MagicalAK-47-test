using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mak47.control;

namespace mak47.test {
	public class GamepadTest : MonoBehaviour {

		// Use this for initialization
		void Start( ) {

		}

		// Update is called once per frame
		void Update( ) {
			var gamepad = Gamepad.Instance;

			foreach (var button in Enum.GetValues(typeof(Gamepad.PhysicalButton))) {
				if (gamepad.GetButtonDown((Gamepad.PhysicalButton)button)) {
					Debug.Log(button);
				}
			}
		}
	}
}
