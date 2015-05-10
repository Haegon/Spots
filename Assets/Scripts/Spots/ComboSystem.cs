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
		//TODO: pause 상태일때 콤버가 끊길듯. TimeManager를 만들어서 해결해야 할듯.
		if(m_curCombo == 0 || Time.realtimeSinceStartup - m_prevComboTime <= m_comboDuration)
			Combo(target, count);
		else
			Miss();
	}

	//콤버 성공했을때.
	void Combo(GameObject target, int count = 1)
	{
		m_curCombo += count;

		if(m_curCombo < m_effectStartNum)
			return;

		m_prevComboTime = Time.realtimeSinceStartup;
		GameObject combo = GameObject.Instantiate(m_comboObject) as GameObject;

		combo.transform.parent = target.transform.parent;
		combo.transform.position = target.transform.position;
		combo.transform.localScale = Vector3.one;

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
