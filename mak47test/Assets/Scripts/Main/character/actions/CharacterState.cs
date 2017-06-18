using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using mak47.control;

namespace mak47.characters.actions {
	public abstract class CharacterState {
		public abstract void Move(Gamepad gamepad);
		public abstract void Jump();
	}
}
