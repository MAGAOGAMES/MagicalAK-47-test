using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mak47.control;
using mak47.characters;

namespace mak47.vehicles {
	public class Vehicle : MonoBehaviour, Controllable, Interactive, camera.Focusable{
		float accelalation;
		Character driver;
		
		// Use this for initialization
		void Start( ) {

		}

		// Update is called once per frame
		void Update( ) {

		}

		public void ControlByGamepad(Gamepad gamepad) {

		}

		public void Move(Gamepad gamepad) {
		}

		public string GetActionName( ) {
			return "乗る";
		}

		public void AcceptInteract( Character character ) {
			Debug.Log("Interact " + name);
			driver = character;
			driver.TakeVehicle();
			var camera = driver.GetCamera();
			if( camera != null ) character.GetCamera().Focus(this);
		}

		public void AcceptCamera( camera.Camera camera ) {
			
		}
	}
}
