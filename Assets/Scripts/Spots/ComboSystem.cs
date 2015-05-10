using UnityEngine;
using System.Collections;

public class ComboSystem : MonoBehaviour {

	//현재 콤버.
	int m_curCombo;
	//콤버 오브젝트.
	Object m_comboObject;
	//마지막 콤버 시간.
	float m_prevComboTime;
	//몇초동안 콤버 지속.
	int m_comboDuration = 2;
	//콤버 이펙트를 몇부터 보여줄까.
	int m_effectStartNum = 2;

	public void Start()
	{
		m_comboObject = Resources.Load("Prefabs/Combo");
	}

	public void ComboCheck(GameObject target, int count = 1)
	{
		Debug.Log("cur : " + Time.time);
		Debug.Log("pre : " + m_prevComboTime);

		if(m_curCombo == 0 || Time.time - m_prevComboTime <= m_comboDuration)
			Combo(target, count);
		else
			Miss();
	}

	//콤버 성공했을때.
	void Combo(GameObject target, int count = 1)
	{
		m_curCombo += count;
		m_prevComboTime = Time.time;

		if(m_curCombo < m_effectStartNum)
			return;

		GameObject combo = GameObject.Instantiate(m_comboObject) as GameObject;

		combo.transform.parent = target.transform.parent;
		combo.transform.position = target.transform.position;
		combo.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);

		combo.transform.FindChild("label").GetComponent<UILabel>().text = "x " + m_curCombo.ToString();

		//우선은 0.5초후에 파괴.
		Destroy(combo, 0.5f);
	}

	//콤버 끊김.
	public void Miss()
	{
		m_curCombo = 0;
	}

}
