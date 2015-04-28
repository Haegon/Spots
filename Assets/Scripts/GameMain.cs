using UnityEngine;
using System.Collections;

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

	public float mTimeLimit = 30.0f;
	public float mTimeSpan = 1.0f;
	public float mDeadLine = 30.0f;
	public int mCount = 0;
	public bool isFinish = false;

	int SX = Screen.width;
	int SY = Screen.height;
	GUIStyle tStyle = new GUIStyle();
	GUIStyle cStyle = new GUIStyle();

	// Use this for initialization
	void Start () {
		tStyle.normal.textColor = Color.black;
		tStyle.alignment = TextAnchor.MiddleCenter;
		tStyle.fontSize = 80*Screen.height/720;
		cStyle.normal.textColor = Color.black;
		cStyle.alignment = TextAnchor.MiddleCenter;
		cStyle.fontSize = 80*Screen.height/720;
	}
	
	// Update is called once per frame
	void Update () {
		if ( Time.time > mTimeLimit ) { 
			isFinish = true;
			mDeadLine = 0;
		} else {
			mDeadLine = mTimeLimit - Time.time;
		}
	}

	void OnGUI () {
		GUI.Label(new Rect(0, 0, SX/2, SY/10), string.Format("{0:F2}",mDeadLine), tStyle);
		GUI.Label(new Rect(SX/2, 0, SX/2, SY/10), string.Format("{0:D}",mCount), cStyle);

	}
}
