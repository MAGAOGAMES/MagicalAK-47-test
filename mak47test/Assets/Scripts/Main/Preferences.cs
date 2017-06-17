using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preferences : MonoBehaviour {
	//Gameplay
	public class GameplaySetting{
		public enum Difficulty{ Easy, Normal, Hard, Extreme}
		public Difficulty difficulty{set; get;}
		public GameplaySetting(){
			this.difficulty = Difficulty.Normal;
		}
	}
	public GameplaySetting GameplaySettings{private set; get;}

	//Graphic

	//Audio
	public class AudioSettings{
		float _volumeMaster = 1.0f;
		float _volumeSE = 1.0f;
		float _volumeBGM = 1.0f;
		public float VolumeMaster{ set{_volumeMaster=Clamp(value);} get{return _volumeMaster;} }
		public float VolumeSE{ set{_volumeSE=Clamp(value);} get{return _volumeSE;} }
		public float VolumeBGM{ set{_volumeBGM=Clamp(value);} get{return _volumeBGM;} }
		public AudioSettings(){	
		}
		float Clamp( float value ){
			return Mathf.Clamp( value, 0.0f, 1.0f );
		}
	}


	private static Preferences instance;
	public static Preferences GetInstance(){
		return instance;
	}

	private Preferences(){
		this.GameplaySettings = new GameplaySetting();
	}


	//MonoBehaviour overrides
	void Awake( ){
		instance = this;
		DontDestroyOnLoad(this.gameObject);
	}
	void Start () {}
	void Update () {}
}

