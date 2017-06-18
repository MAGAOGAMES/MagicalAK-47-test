using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mak47.control {
	public class KeyConfig {
		public enum Action { Decide, Cancel }
		Dictionary<Action, Gamepad.Button> config;

		static KeyConfig instance;
		public static KeyConfig Instance {
			get {
				if (instance == null) instance = new KeyConfig();
				return instance;
			}
		}

		KeyConfig() {
			config = new Dictionary<Action, Gamepad.Button>();
			config.Add(Action.Decide, Gamepad.Button.Circle);
			config.Add(Action.Cancel, Gamepad.Button.Cross);
		}

		public Gamepad.Button ToPhysicalButton(Action button) {
			return config[button];
		}

		public void Set(Action action, Gamepad.Button button) {
			config[action] = button;
		}
	}
}
