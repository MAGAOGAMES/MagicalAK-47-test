using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mak47;
using mak47.control;

namespace mak47.test {


	public class CharacterTest : MonoBehaviour {

		[SerializeField] Character testChar;

		// Use this for initialization
		void Start( ) {

		}

		// Update is called once per frame
		void Update( ) {
			var gamepad = Gamepad.Instance;

			var leftstickInput = gamepad.GetAxis(Gamepad.Axis.LeftStick);
			testChar.MoveFromGamepad(leftstickInput);
		}
	}
}