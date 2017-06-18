using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mak47.control {
	public class KeyConfig {
		public enum Action { Decide, Cancel }
		Dictionary<Action, Gamepad.PhysicalButton> config;

		static KeyConfig instance;
		public static KeyConfig Instance {
			get {
				if (instance == null) instance = new KeyConfig();
				return instance;
			}
		}

		KeyConfig() {
			config = new Dictionary<Action, Gamepad.PhysicalButton>();
			config.Add(Action.Decide, Gamepad.PhysicalButton.Circle);
			config.Add(Action.Cancel, Gamepad.PhysicalButton.Cross);
		}

		public Gamepad.PhysicalButton ToPhysicalButton(Action button) {
			return config[button];
		}

		public void Set(Action action, Gamepad.PhysicalButton button) {
			config[action] = button;
		}
	}
}
