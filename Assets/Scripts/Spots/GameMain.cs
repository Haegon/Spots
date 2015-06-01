using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GoogleMobileAds.Api;

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
	public GameObject m_GoldObject;
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

	public ComboSystem m_Combo;
	public int m_CycleCount;

	public int bonusTime = 10;
	public int leastTimeLimit = 5;

	public int m_MaxFever = 15;
	int m_FeverValue = 0;
	public int m_FeverTime = 3;
	public bool isFeverTime = false;
	UISprite m_FeverSprite;
	public Rainbow m_curRainbow;


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

		m_GoldObject = GameObject.Find("Button_Spot_Gold");
		m_Slider = GameObject.Find("LeftTime").GetComponent<UISlider>();
		m_SliderSprite = GameObject.Find("FG").GetComponent<UISprite>();
		m_FeverSprite = GameObject.Find("fever").GetComponent<UISprite>();
	
		m_BackGround = GameObject.Find("BackGround_Game");
		m_OptionBGM = GameObject.Find("Option_BGM").GetComponent<UIToggle>();
		m_OptionSound = GameObject.Find("Option_Sound").GetComponent<UIToggle>();

		m_OptionBGM.value = m_PlayerData.BGM.ToBool();
		m_OptionSound.value = m_PlayerData.Sound.ToBool();

		GoIntro();
//		GoHome();

#if !UNITY_EDITOR
//		PlayGamesPlatform.DebugLogEnabled = true;
//		PlayGamesPlatform.Activate();
//		Social.localUser.Authenticate((bool success) => {
//			if ( success ) {
//
//			} else {
//				PlayGamesPlatform.Instance.SignOut();
//			}
//		});
#endif

		// Create a 320x50 banner at the top of the screen.
		BannerView bannerView = new BannerView(
			"ca-app-pub-4566735050109706/3877683272", AdSize.Banner, AdPosition.Bottom);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the banner with the request.
		bannerView.LoadAd(request);

		cStyle.normal.textColor = Color.black;
		cStyle.alignment = TextAnchor.MiddleRight;
		cStyle.fontSize = 80*Screen.height/720;

		m_Combo = this.gameObject.AddComponent<ComboSystem>();

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
//		if ( Time.time - m_StartTime > m_TimeLimit ) { 
		if( m_DeadLine < 0) {
			m_DeadLine = 0;
			m_Slider.value = 0;
			m_FeverSprite.fillAmount = 1;
			ShowResult();		
		} else {
//			m_DeadLine = m_TimeLimit - (Time.time - m_StartTime);
			m_DeadLine -= Time.deltaTime;
			ShowSlider();
		}
	}

	public void ShowSlider() {
		m_Slider.value = m_DeadLine / m_TimeLimit;
//		m_Fever.fillAmount = m_DeadLine / m_TimeLimit;

		if(isFeverTime)
		{
			m_SliderSprite.color = ColorEx.GetColor((Rainbow)(Time.time * 10 % 7));
			m_FeverSprite.fillAmount -= Time.deltaTime / (float)m_FeverTime;
		}
		else
			m_SliderSprite.color = ColorEx.GetColor(m_curRainbow);
	}

	public void NewGame() {
		GUI_Mgr.Instance.NewGame();

		Time.timeScale = 1.0f;
		m_Count = 0;
		m_TimeLimit = m_StaticTimeLimit;
		m_DeadLine = m_StaticTimeLimit;
		m_CycleCount = 0;
		m_FeverValue = 0;
		m_FeverSprite.fillAmount = 0;

		m_curRainbow = Rainbow.RED;

		ShowSlider();

		m_GameState = GameState.READY;
	}

	public void StartGame() {
		
		m_StartTime = Time.time;
		m_GameState = GameState.INGAME;
	}

	public void GoIntro() {
		GUI_Mgr.Instance.GoIntro();

		m_GameState = GameState.INTRO;
	}

	public void GoHome() {
		GUI_Mgr.Instance.GoHome();

		m_GameState = GameState.HOME;
	}

	public void Spot(GameObject target)
	{
		if(!isFeverTime)
			m_curRainbow = ColorEx.Next(m_curRainbow);

		m_Count++;
		m_Combo.ComboCheck(target);
	}

	public void Fever()
	{
		if(!isFeverTime)
		{
			m_FeverValue ++;
			
			m_FeverSprite.fillAmount = 1.0f/(float)m_MaxFever * (float)m_FeverValue;
			if(m_FeverValue == m_MaxFever)
				StartFever();
		}
	}

	void StartFever()
	{
		isFeverTime = true;
		foreach(var pair in spotObjects)
		{
			pair.Value.GetComponent<Spot>().StartCoroutine("Fever");
		}

		StartCoroutine(FeverTime());
	}

	IEnumerator FeverTime()
	{
		yield return new WaitForSeconds(m_FeverTime);

		foreach(var pair in spotObjects)
		{
			pair.Value.GetComponent<Spot>().EndFever();
		}

		RePositionSpots();

		m_FeverValue = 0;
		isFeverTime = false;
	}

	public void WrongSpot() {
		ShowResult();
		RePositionSpots();
		m_GameState = GameState.FINISH;
		//GameMain.Instance.m_TimeLimit -= 1.0f;
		//TweenColor.Begin3(m_BackGround,0.5f,Color.white,Color.red);
		//m_Combo.Miss();
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
		m_GameState = GameState.INGAME;
	}
	
	public void Option() {
		GUI_Mgr.Instance.Option();
		
		m_GameState = GameState.OPTION;
	}

	public void OneCycle()
	{
		m_DeadLine += bonusTime;
		m_CycleCount += 1;
		
		if(m_TimeLimit > leastTimeLimit)
		{
			m_TimeLimit -= 1.0f;

			if(m_DeadLine > m_TimeLimit)
			{
				m_DeadLine = m_TimeLimit;
			}
		}
	}

	public void RePositionSpots() {
		foreach ( KeyValuePair<Rainbow,GameObject> kvp in spotObjects ) {
			kvp.Value.transform.position = new Vector3(-1000,-1000,0);
		}
		
		foreach ( KeyValuePair<Rainbow,GameObject> kvp in spotObjects ) {
			kvp.Value.GetComponent<Spot>().RePositionSpot();
		}
	}
	
	public void GetGold() {
		m_PlayerData.Gold ++;
		m_GoldObject.GetComponent<Gold>().RePositionGold();
	}
	
	void ShowUI() {
		GUI.Label(new Rect(SX/2, 0, SX/2, SY/10), string.Format("{0:D}",m_Count), cStyle);
	}

	void OnGUI () {
		if ( m_GameState == GameState.INGAME || m_GameState == GameState.READY)
			ShowUI();	
	}

	PlayerData Load() {
	
		PlayerData pd = new PlayerData();

		pd.BGM = PlayerPrefs.GetInt("bgm");
		pd.Sound = PlayerPrefs.GetInt("sound");
		pd.Top = PlayerPrefs.GetInt("top");
		pd.Gold = PlayerPrefs.GetInt("gold");
		return pd;
	}

	public void Save() {
	
		PlayerPrefs.SetInt("bgm",m_PlayerData.BGM);
		PlayerPrefs.SetInt("sound",m_PlayerData.Sound);
		PlayerPrefs.SetInt("top",m_PlayerData.Top);
		PlayerPrefs.SetInt("gold",m_PlayerData.Gold);
	}
}

[SerializeField]
public class PlayerData {
	public int BGM = 1 ; // 1ㅇㅣㅁㅕㄴ ㅈㅐㅅㅐㅇ 0ㅇㅣㅁㅕㄴ ㅇㅏㄴㅈㅐㅅㅐㅇ
	public int Sound = 1 ;
	public int Top = 0;
	public int Gold = 0;
}