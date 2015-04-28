using UnityEngine;
using System.Collections;

public class Spot : MonoBehaviour {

	private Color startColor;
	private float timeNow;

	void Start () {
		timeNow = Time.time;
		Random.seed = Utils.GetRandomNumber(1,100000);

		startColor = new Color(Random.Range(0f,200f)/256f,Random.Range(0f,200f)/256f,Random.Range(0f,200f)/256f);
		int r = Random.Range(1,4);

		switch (r) {
		case 1:
			startColor.r = 200f/256f;
			if ( startColor.g > startColor.b ) startColor.g = 0;
			else startColor.b= 0;
			break;
		case 2:
			startColor.g = 200f/256f;
			if ( startColor.b > startColor.r ) startColor.b = 0;
			else startColor.r= 0;
			break;
		case 3:
			startColor.b = 200f/256f;
			if ( startColor.r > startColor.g ) startColor.r = 0;
			else startColor.g= 0;
			break;
		}
		startColor = new Color(Random.Range(0f,200f)/256f,Random.Range(0f,200f)/256f,Random.Range(0f,200f)/256f);

		GameObject[] gos = GameObject.FindGameObjectsWithTag("spot");

		for (;;) {
			Vector3 v = new Vector3(Random.Range(-2.3f,2.3f),Random.Range(-3.5f,3.5f),0);
			if ( checkFar(gos,v) ) {
				transform.position = v;
				break;			
			}
		}

		//transform.position = new Vector3(Random.Range(-2.3f,2.3f),Random.Range(-3.5f,3.5f),0);
	}

	bool checkFar(GameObject[] gos, Vector3 v) {
		foreach ( GameObject go in gos ) {
			Debug.Log(go.transform.position.ToString());
			if ( Vector3.Distance(v,go.transform.position) < 2.0f ) {
				Debug.Log("neer "+Vector3.Distance(v,go.transform.position));
				return false;
			}
			Debug.Log("far "+Vector3.Distance(v,go.transform.position));
		}
		return true;
	}


	void Update () {
		SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
		sprite.color = Color.Lerp(startColor, Color.white, (Time.time - timeNow) / GameMain.Instance.mTimeSpan);

		if ( Time.time > timeNow + GameMain.Instance.mTimeSpan && !GameMain.Instance.isFinish ) {
			GameObject go = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Spot"));
			Destroy(this.gameObject);
		}

//		if (Input.touchCount == 1)
//		{
//			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
//			Vector2 touchPos = new Vector2(wp.x, wp.y);
//			if (collider2D == Physics2D.OverlapPoint(touchPos))
//			{
////				GameMain.Instance.mCount ++;
////				GameObject go = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Spot"));
////				Destroy(this.gameObject);
//			}
//		}
	}

	void OnMouseUp() {
		if ( !GameMain.Instance.isFinish ) {
			GameMain.Instance.mCount ++;
			GameObject go = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Spot"));
			Destroy(this.gameObject);
		}
	}
}

