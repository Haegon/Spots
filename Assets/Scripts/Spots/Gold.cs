using UnityEngine;
using System.Collections;

public class Gold : MonoBehaviour {

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
		//particle = gameObject.GetComponentInChildren<ParticleSystem>();
		//particle.startColor = ColorEx.SpotColor(gameObject.name);
		sprite = child.GetComponent<UISprite>();
		//sprite.color = ColorEx.SpotColor(gameObject.name);
		//startColor = ColorEx.SpotColor(gameObject.name);
		RePositionGold();
	}

	public void RePositionGold() {
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
		return true;
	}
}
