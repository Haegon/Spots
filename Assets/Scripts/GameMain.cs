using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMain : MonoBehaviour {

	private static GameMain _instance;
	public static GameMain Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(GameMain)) as GameMain;
				if (_instance == null)
				{
					GameObject go = new GameObject("GameMain");
				}
			}
			return _instance;
		}
	}

	public GameState m_GameState = GameState.HOME;

	public float m_StartTime = 0.0f;
	public float m_TimeLimit = 30.0f;
	public float m_TimeSpan = 1.0f;
	public float m_DeadLine = 30.0f;
	public int m_Count = 0;
	public UISlider m_Slider;

	int SX = Screen.width;
	int SY = Screen.height;
	GUIStyle tStyle = new GUIStyle();
	GUIStyle cStyle = new GUIStyle();

	public Dictionary<GameState,GameObject> gameObjects = new Dictionary<GameState, GameObject>();

	// Use this for initialization
	void Start () {

		DontDestroyOnLoad(gameObject);

		gameObjects.Add(GameState.HOME,GameObject.Find("HomeUI"));
		gameObjects.Add(GameState.INGAME,GameObject.Find("GameUI"));
		gameObjects.Add(GameState.FINISH,GameObject.Find("ResultUI"));

		m_Slider = GameObject.Find("LeftTime").GetComponent<UISlider>();

		GoHome();

		cStyle.normal.textColor = Color.black;
		cStyle.alignment = TextAnchor.MiddleRight;
		cStyle.fontSize = 80*Screen.height/720;
	}
	
	// Update is called once per frame
	void Update () {
		if ( m_GameState == GameState.INGAME )
			CountDown();
	}

//	void OnLevelWasLoaded(int level) {
//		if (level == 1) {
//			if ( !gameObjects.ContainsKey("ResultUI") )
//				gameObjects.Add("ResultUI",GameObject.Find("ResultUI"));
//			if ( !gameObjects.ContainsKey("GameUI") )
//				gameObjects.Add("GameUI",GameObject.Find("GameUI"));
//			m_Slider = GameObject.Find("LeftTime").GetComponent<UISlider>();
//			gameObjects["ResultUI"].SetActive(false);			                        
//		}
//	}

	void CountDown() {
		if ( Time.time - m_StartTime > m_TimeLimit ) { 
			m_DeadLine = 0;
			m_Slider.value = 0;
			ShowResult();		
		} else {
			m_DeadLine = m_TimeLimit - (Time.time - m_StartTime);
			m_Slider.value = m_DeadLine / m_TimeLimit;
		}
	}

	public void NewGame() {
		gameObjects[GameState.HOME].SetActive(false);	
		gameObjects[GameState.INGAME].SetActive(true);	
		gameObjects[GameState.FINISH].SetActive(false);	

		m_StartTime = Time.time;
		m_Count = 0;
		m_GameState = GameState.INGAME;
	}

	public void GoHome() {
		gameObjects[GameState.HOME].SetActive(true);	
		gameObjects[GameState.INGAME].SetActive(false);	
		gameObjects[GameState.FINISH].SetActive(false);	

		m_GameState = GameState.HOME;
	}

	public void ShowResult() {
		gameObjects[GameState.HOME].SetActive(false);	
		gameObjects[GameState.INGAME].SetActive(true);	
		gameObjects[GameState.FINISH].SetActive(true);	
		
		m_GameState = GameState.FINISH;
	}

	void ShowUI() {
		GUI.Label(new Rect(SX/2, 0, SX/2, SY/10), string.Format("{0:D}",m_Count), cStyle);
	}

	void OnGUI () {
		if ( m_GameState == GameState.INGAME )
			ShowUI();	
	}
}
