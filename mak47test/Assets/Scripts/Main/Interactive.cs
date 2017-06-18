using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mak47 {
	public interface Interactive {
		string GetActionName( );
		void AcceptInteract(characters.Character character);
	}
}