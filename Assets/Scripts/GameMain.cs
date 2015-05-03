using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

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
	public float m_TimeLimit;// = 30.0f;
	public float m_TimeSpan;// = 1.0f;
	public float m_DeadLine;// = 30.0f;
	public int m_Count = 0;
	public UISlider m_Slider;
	public UISprite m_SliderSprite;

	int SX = Screen.width;
	int SY = Screen.height;
	GUIStyle tStyle = new GUIStyle();
	GUIStyle cStyle = new GUIStyle();

	public Dictionary<Rainbow,GameObject> spotObjects = new Dictionary<Rainbow, GameObject>();

	// Use this for initialization
	void Start () {

	}

	public void Init() {
		
		GameObject[] gos = GameObject.FindGameObjectsWithTag("spot");
		foreach ( GameObject go in gos ) {
			spotObjects.Add(ColorEx.GetRainbow(go.name), go);
		}

#if UNITY_EDITOR
		GoHome();
#else
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();
		Social.localUser.Authenticate((bool success) => {
			if ( success )
				GoHome();
			else {
				Debug.Log("!success");
				PlayGamesPlatform.Instance.SignOut();
			}
		});
#endif
		cStyle.normal.textColor = Color.black;
		cStyle.alignment = TextAnchor.MiddleRight;
		cStyle.fontSize = 80*Screen.height/720;
	}

	// Update is called once per frame
	void Update () {
		if ( m_GameState == GameState.INGAME )
			CountDown();

		if (Application.platform == RuntimePlatform.Android)
		{	
			if (Input.GetKey(KeyCode.Escape))
			{
				PauseGame();
			}
		}  
	}

	void CountDown() {
		if ( Time.time - m_StartTime > m_TimeLimit ) { 
			m_DeadLine = 0;
			m_Slider.value = 0;
			ShowResult();		
		} else {
			m_DeadLine = m_TimeLimit - (Time.time - m_StartTime);
			m_Slider.value = m_DeadLine / m_TimeLimit;
			m_SliderSprite.color = ColorEx.GetColor((Rainbow)(m_Count%7));
		}
	}

	public void NewGame() {
		GUI_Mgr.Instance.NewGame();

		m_Slider = GameObject.Find("LeftTime").GetComponent<UISlider>();
		m_SliderSprite = GameObject.Find("FG").GetComponent<UISprite>();

		m_StartTime = Time.time;
		m_Count = 0;
		m_GameState = GameState.INGAME;
	}

	public void GoHome() {
		GUI_Mgr.Instance.GoHome();

		m_GameState = GameState.HOME;
	}

	public void ShowResult() {
		GUI_Mgr.Instance.ShowResult();

		UILabel score = GameObject.Find("Label_Count").GetComponent<UILabel>();
		score.text = m_Count.ToString();
		m_GameState = GameState.FINISH;

#if !UNITY_EDITOR
		Social.ReportScore(m_Count, "CgkItIOIzdQOEAIQAQ", (bool success) => {
			// handle success or failure
		});
#endif
	}

	public void PauseGame() {
		GUI_Mgr.Instance.PauseGame();
		
		Time.timeScale = 0;
		m_GameState = GameState.PAUSE;
	}
	
	public void ResumeGame() {
		GUI_Mgr.Instance.ResumeGame();
		
		Time.timeScale = 1;
		m_GameState = GameState.INGAME;
	}
	
	public void RePositionSpots() {
		foreach ( KeyValuePair<Rainbow,GameObject> kvp in spotObjects ) {
			kvp.Value.transform.position = new Vector3(-1000,-1000,0);
		}

		foreach ( KeyValuePair<Rainbow,GameObject> kvp in spotObjects ) {
			kvp.Value.GetComponent<Spot>().Init();
		}
	}
	
	void ShowUI() {
		GUI.Label(new Rect(SX/2, 0, SX/2, SY/10), string.Format("{0:D}",m_Count), cStyle);
	}

	void OnGUI () {
		if ( m_GameState == GameState.INGAME )
			ShowUI();	
	}
}
