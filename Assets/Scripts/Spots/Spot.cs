using UnityEngine;
using System.Collections;

public class Spot : MonoBehaviour {
	
	private Color startColor;
	private float timeNow;
	GameObject child;
	int offSet = 5;
	//magic.
	int ingameWidth = 720; 
	int ingameHeight = 1280;

	UISprite sprite;
	ParticleSystem particle;

	void Start () {
		child = gameObject.GetComponentInChildren<UISprite>().gameObject;
		particle = gameObject.GetComponentInChildren<ParticleSystem>();
		particle.startColor = ColorEx.SpotColor(gameObject.name);
		sprite = child.GetComponent<UISprite>();
		sprite.color = ColorEx.SpotColor(gameObject.name);
		startColor = ColorEx.SpotColor(gameObject.name);
		RePositionSpot();
	}

	public IEnumerator Fever()
	{
		Rainbow rainbow = ColorEx.GetRainbow(this.gameObject.name);

		while(GameMain.Instance.isFeverTime)
		{
			rainbow = ColorEx.Next(rainbow);

			sprite.color = ColorEx.GetColor(rainbow);
			particle.startColor = ColorEx.GetColor(rainbow);
			yield return new WaitForSeconds(0.1f);
		}
	}

	public void EndFever()
	{
		sprite.color = ColorEx.SpotColor(gameObject.name);
		particle.startColor = ColorEx.SpotColor(gameObject.name);
	}
		
	void Update () {
		if ( Time.time > timeNow + GameMain.Instance.m_TimeSpan && GameMain.Instance.m_GameState == GameState.INGAME ) {
			RePositionSpot();
		}

//		UIButton btn = gameObject.GetComponent<UIButton>();
//		btn.defaultColor = child.GetComponent<UISprite>().color;
//		btn.hover = btn.defaultColor;
//		btn.pressed = btn.defaultColor;
//		btn.disabledColor = btn.defaultColor;
	}

	public void RePositionSpot() {
		timeNow = Time.time;
		
		Random.seed = Utils.GetRandomNumber(1,100000);

		GameObject[] gos = GameObject.FindGameObjectsWithTag("spot");
		for (;;) {
			float x = ingameWidth/2 - sprite.width/2;
			float y = ingameHeight/2 - sprite.height/2;
			
			Vector3 v = new Vector3(Random.Range(-x, x),Random.Range(-y, y - 100),0);
			if ( checkFar(gos,v) ) {
				transform.localPosition = v;
				break;	
			}
		}
	}
	
	bool checkFar(GameObject[] gos, Vector3 v) {
		foreach ( GameObject go in gos ) {
			if ( go.name == gameObject.name ) continue;
			if ( Vector3.Distance(v,go.transform.localPosition) < sprite.width + offSet ) {
				return false;
			}
		}
		GameObject gold = GameObject.FindGameObjectWithTag("gold");
		if ( Vector3.Distance(v,gold.transform.localPosition) < sprite.width + offSet ) {
			return false;
		}
		return true;
	}
}


//using UnityEngine;
//using System.Collections;
//
//public class Spot : MonoBehaviour {
//	
//	private Color startColor;
//	private float timeNow;
//	GameObject child;
//	
//	void Start () {
//		child = gameObject.GetComponentInChildren<UISprite>().gameObject;
//		Init();
//	}
//	
//	
//	void Update () {
//		if ( Time.time > timeNow + GameMain.Instance.m_TimeSpan && GameMain.Instance.m_GameState == GameState.INGAME ) {
//			Init();
//		}
//		
//		UIButton btn = gameObject.GetComponent<UIButton>();
//		btn.defaultColor = child.GetComponent<UISprite>().color;
//		btn.hover = btn.defaultColor;
//		btn.pressed = btn.defaultColor;
//		btn.disabledColor = btn.defaultColor;
//	}
//	
//	public void Init() {
//		timeNow = Time.time;
//		
//		Random.seed = Utils.GetRandomNumber(1,100000);
//		startColor = new Color(Random.Range(0f,200f)/256f,Random.Range(0f,200f)/256f,Random.Range(0f,200f)/256f);
//		int r = Random.Range(1,4);
//		
//		switch (r) {
//		case 1:
//			startColor.r = 200f/256f;
//			if ( startColor.g > startColor.b ) startColor.g = 0;
//			else startColor.b= 0;
//			break;
//		case 2:
//			startColor.g = 200f/256f;
//			if ( startColor.b > startColor.r ) startColor.b = 0;
//			else startColor.r= 0;
//			break;
//		case 3:
//			startColor.b = 200f/256f;
//			if ( startColor.r > startColor.g ) startColor.r = 0;
//			else startColor.g= 0;
//			break;
//		}
//		
//		GameObject[] gos = GameObject.FindGameObjectsWithTag("spot");
//		for (;;) {
//			Vector3 v = new Vector3(Random.Range(-0.4f,0.4f),Random.Range(-0.7f,0.7f),0);
//			if ( checkFar(gos,v) ) {
//				transform.position = v;
//				break;	
//			}
//		}
//		TweenColor.Begin2(child,1.0f,startColor,Color.white);
//	}
//	
//	bool checkFar(GameObject[] gos, Vector3 v) {
//		foreach ( GameObject go in gos ) {
//			if ( go.name == gameObject.name ) continue;
//			//if ( Vector3.Distance(v,go.transform.position) < 0.2f ) {
//			if ( Vector3.Distance(v,go.transform.position) < 0.1f ) {
//				return false;
//			}
//		}
//		return true;
//	}
//}
//

