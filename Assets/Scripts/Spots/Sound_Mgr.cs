using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sound_Mgr : Fibra {

	public Dictionary<string,AudioSource> soundObjects = new Dictionary<string, AudioSource>();
	public bool isInitialize =false;
	private static Sound_Mgr _instance;
	public static Sound_Mgr Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(Sound_Mgr)) as Sound_Mgr;
				if (_instance == null)
				{
					GameObject go = new GameObject("Sound_Mgr");
				}
			}
			return _instance;
		}
	}

	// Use this for initialization
	void Start () {
	}

	public void Init() {
		soundObjects.Add("home",GameObject.Find("BackGround_Home").GetComponent<AudioSource>());
		soundObjects.Add("game",GameObject.Find("BackGround_Game").GetComponent<AudioSource>());

		isInitialize = true;
	}
	
	// Update is called once per frame
	void Update () {
		if ( isInitialize ) {
			if ( GameMain.Instance.m_PlayerData.BGM ) {
				soundObjects["home"].volume = 1;
				soundObjects["game"].volume = 1;
			} else {
				soundObjects["home"].volume = 0;
				soundObjects["game"].volume = 0;
			}
		}
	}
}
