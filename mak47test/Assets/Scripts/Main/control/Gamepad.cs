using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mak47.control {
	/// <summary>
	/// Game pad input.
	/// </summary>
	public class Gamepad {
		public enum PhysicalButton { Circle, Cross, Triangle, Square, R1, R2, L1, L2 }


		public abstract class Axis {
			protected Axis( ) { }
			public abstract string GetName( );
			public class RightStickAxis : Axis { public override string GetName( ) { return "GP_R"; } }
			public class LeftStickAxis : Axis { public override string GetName( ) { return "GP_L"; } }
			public class DirectionalPadAxis : Axis { public override string GetName( ) { return "GP_D"; } }

			private static RightStickAxis rightStick = new RightStickAxis();
			private static LeftStickAxis leftStick = new LeftStickAxis();
			private static DirectionalPadAxis directional = new DirectionalPadAxis();
			public static RightStickAxis RightStick { get { return rightStick; } }
			public static LeftStickAxis LeftStick { get { return leftStick; } }
			public static DirectionalPadAxis DirectionalPad { get { return directional; } }
		}
		private static Dictionary<PhysicalButton, KeyCode> physicalKeyCodeMap;

		private static Gamepad instance;
		public static Gamepad Instance { get { if (instance == null) instance = new Gamepad(); return instance; } }
		private Gamepad( ) {
			physicalKeyCodeMap = new Dictionary<PhysicalButton, KeyCode>();
			physicalKeyCodeMap.Add(PhysicalButton.Circle, KeyCode.JoystickButton2);
			physicalKeyCodeMap.Add(PhysicalButton.Cross, KeyCode.JoystickButton1);
			physicalKeyCodeMap.Add(PhysicalButton.Triangle, KeyCode.JoystickButton3);
			physicalKeyCodeMap.Add(PhysicalButton.Square, KeyCode.JoystickButton0);
			physicalKeyCodeMap.Add(PhysicalButton.R1, KeyCode.JoystickButton5);
			physicalKeyCodeMap.Add(PhysicalButton.R2, KeyCode.JoystickButton7);
			physicalKeyCodeMap.Add(PhysicalButton.L1, KeyCode.JoystickButton4);
			physicalKeyCodeMap.Add(PhysicalButton.L2, KeyCode.JoystickButton6);
		}

		public bool GetButtonDown( PhysicalButton button ) {
			return Input.GetKeyDown(physicalKeyCodeMap[button]);
		}

		public bool GetButton( PhysicalButton button ) {
			return Input.GetKey(physicalKeyCodeMap[button]);
		}

		public bool GetButtonUp( PhysicalButton button ) {
			return true;
		}

		public Vector2 GetAxis( Axis axis ) {
			var x = Input.GetAxis(axis.GetName() + "X");
			var y = Input.GetAxis(axis.GetName() + "Y");
			return new Vector2(x, y);
		}

		public float GetL2() {
			return Input.GetAxis("GP_L2") * 0.5f + 0.5f;
		}

		public float GetR2() {
			return Input.GetAxis("GP_R2") * 0.5f + 0.5f;
		}
	}
}
