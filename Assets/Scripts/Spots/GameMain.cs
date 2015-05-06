using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GameMain : Fibra {

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

	[HideInInspector]
	public GameState m_GameState = GameState.HOME;

	[HideInInspector]
	public float m_StartTime = 0.0f;

	public float m_StaticTimeLimit;// = 30.0f;
	[HideInInspector]
	public float m_TimeLimit;// = 30.0f;

	public float m_TimeSpan;// = 1.0f;
	[HideInInspector]
	public float m_DeadLine;// = 30.0f;
	[HideInInspector]
	public int m_Count = 0;
	[HideInInspector]
	public int m_TopCount = 0;
	[HideInInspector]
	public PlayerData m_PlayerData;
	[HideInInspector]
	public bool isInitialize = false;

	int SX = Screen.width;
	int SY = Screen.height;
	GUIStyle cStyle = new GUIStyle();

	[HideInInspector]
	public Dictionary<Rainbow,GameObject> spotObjects = new Dictionary<Rainbow, GameObject>();
	[HideInInspector]
	public UISlider m_Slider;
	[HideInInspector]
	public UISprite m_SliderSprite;
	[HideInInspector]
	public GameObject m_BackGround;
	[HideInInspector]
	public UIToggle m_OptionBGM;
	[HideInInspector]
	public UIToggle m_OptionSound;

	void Awake () {
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
	}

	// Use this for initialization
	void Start () {

		Sound_Mgr.Instance.Init();
		Init();
	}

	public void Init() {
		m_PlayerData = Load();

		GameObject[] gos = GameObject.FindGameObjectsWithTag("spot");
		foreach ( GameObject go in gos ) {
			spotObjects.Add(ColorEx.GetRainbow(go.name), go);
		}

		m_Slider = GameObject.Find("LeftTime").GetComponent<UISlider>();
		m_SliderSprite = GameObject.Find("FG").GetComponent<UISprite>();
		m_BackGround = GameObject.Find("BackGround_Game");
		m_OptionBGM = GameObject.Find("Option_BGM").GetComponent<UIToggle>();
		m_OptionSound = GameObject.Find("Option_Sound").GetComponent<UIToggle>();

		m_OptionBGM.value = m_PlayerData.BGM;
		m_OptionSound.value = m_PlayerData.Sound;

		GoHome();

#if !UNITY_EDITOR
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();
		Social.localUser.Authenticate((bool success) => {
			if ( success ) {

			} else {
				PlayGamesPlatform.Instance.SignOut();
			}
		});
#endif
		cStyle.normal.textColor = Color.black;
		cStyle.alignment = TextAnchor.MiddleRight;
		cStyle.fontSize = 80*Screen.height/720;

		isInitialize = true;
	}

	// Update is called once per frame
	void Update () {
		if ( m_GameState == GameState.INGAME )
			CountDown();

		if (Application.platform == RuntimePlatform.Android)
		{	
			if (Input.GetKey(KeyCode.Escape))
			{
				switch ( m_GameState ) {
				case GameState.HOME :
					break;
				case GameState.INGAME :
					PauseGame();
					break;
				case GameState.READY :
					PauseGame();
					break;
				case GameState.PAUSE :
					ResumeGame();
					break;
				default :
					break;
				}
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
			ShowSlider();
		}
	}

	public void ShowSlider() {
		m_Slider.value = m_DeadLine / m_TimeLimit;
		m_SliderSprite.color = ColorEx.GetColor((Rainbow)(m_Count%7));
	}

	public void NewGame() {
		Time.timeScale = 1.0f;
		m_Count = 0;
		m_TimeLimit = m_StaticTimeLimit;
		m_DeadLine = m_StaticTimeLimit;

		GUI_Mgr.Instance.NewGame();
		ShowSlider();

		m_GameState = GameState.READY;
	}

	public void StartGame() {
		
		m_StartTime = Time.time;
		m_GameState = GameState.INGAME;
	}

	public void GoHome() {
		GUI_Mgr.Instance.GoHome();

		m_GameState = GameState.HOME;
	}

	public void WrongSpot() {
		GameMain.Instance.m_TimeLimit -= 1.0f;
		TweenColor.Begin3(m_BackGround,0.5f,Color.white,Color.red);
	}

	public void ShowResult() {
		GUI_Mgr.Instance.ShowResult();

		UILabel score = GameObject.Find("Label_Count").GetComponent<UILabel>();
		score.text = m_Count.ToString();
		UILabel top = GameObject.Find("Label_Top_Count").GetComponent<UILabel>();
		top.text = m_PlayerData.Top.ToString();
		m_GameState = GameState.FINISH;

		if ( m_Count > m_PlayerData.Top ) {
			m_PlayerData.Top = m_Count;
			Save();
		}
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

		if ( m_Count > 0 ) m_GameState = GameState.INGAME;
		else m_GameState = GameState.READY;
	}
	
	public void Option() {
		GUI_Mgr.Instance.Option();
		
		m_GameState = GameState.OPTION;
	}
	
	public void RePositionSpots() {
		foreach ( KeyValuePair<Rainbow,GameObject> kvp in spotObjects ) {
			kvp.Value.transform.position = new Vector3(-1000,-1000,0);
		}

		foreach ( KeyValuePair<Rainbow,GameObject> kvp in spotObjects ) {
			kvp.Value.GetComponent<Spot>().RePositionSpot();
		}
	}
	
	void ShowUI() {
		GUI.Label(new Rect(SX/2, 0, SX/2, SY/10), string.Format("{0:D}",m_Count), cStyle);
	}

	void OnGUI () {
		if ( m_GameState == GameState.INGAME || m_GameState == GameState.READY)
			ShowUI();	
	}

	PlayerData Load() {
		if ( !File.Exists(Application.persistentDataPath + "/config.gd" ) ) return new PlayerData();
		try
		{
			byte[] dc = ByteIO.FileToByteArray(Application.persistentDataPath + "/config.gd");

			if ( dc.Length > 0 ) {

				string json_str = ByteToString(dc);
				
				Dictionary<string,object> json_obj = Json.Deserialize(json_str) as Dictionary<string,object>;

				PlayerData pd = new PlayerData();

				pd.BGM = (bool)json_obj["bgm"];
				pd.Sound = (bool)json_obj["sound"];
				pd.Top = (int)json_obj["top"];

				return pd;
			}
		}
		catch ( FileNotFoundException e)
		{
				return new PlayerData();
		}
		return new PlayerData();
	}

	public void Save() {
		Dictionary<string, object> dic = new Dictionary<string, object>();
		
		dic.Add("bgm",m_PlayerData.BGM);
		dic.Add("sound",m_PlayerData.Sound);
		dic.Add("top", m_PlayerData.Top);

		// 암호화 하여 파일 쓰기에 성공했다면 listener call
		if ( ByteIO.ByteArrayToFile(Application.persistentDataPath + "/config.gd", StringToByte(Json.Serialize(dic))  ) )
		{

		}
	}

	// 바이트 배열을 String으로 변환 
	private string ByteToString(byte[] strByte) 
	{ 
		string str = Encoding.Default.GetString(strByte); 
		return str; 
	}
	// String을 바이트 배열로 변환 
	private byte[] StringToByte(string str)
	{
		byte[] StrByte = Encoding.UTF8.GetBytes(str); 
		return StrByte; 
	}

}

[SerializeField]
public class PlayerData {
	public bool BGM = true ;
	public bool Sound = true ;
	public int Top = 0;
}