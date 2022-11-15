using UnityEngine;
using System.Collections;

public enum TypeAction { Nghi, ThaCau, KeoCau };
public class DayCau : MonoBehaviour {
	public Transform luoiCau;
	public Vector3 angles;
	public float speed;
	public float angleMax = 70;
	public TypeAction typeAction = TypeAction.Nghi;
	public bool my_turn;
	public Vector3 initAngles;
	public int score;
	public bool computer;

	LineRenderer rope;

	// Use this for initialization
	void Start () {
		speed = 3;
		initAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
		rope = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		rope.SetPosition(0, transform.position);
		rope.SetPosition(1, luoiCau.position);
	}

	void FixedUpdate() {
		if(my_turn && speed > 0 && typeAction == TypeAction.Nghi)
			transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * speed) * angleMax);
	}

	public void ChangeTurn(bool check)
    {
		my_turn = check;
    }

	public void ResetDayCau()
    {
		typeAction = TypeAction.Nghi;
		GamePlayController.instance.StopAni();
		if (!my_turn)
		{
			transform.rotation = Quaternion.Euler(initAngles);
		}
	}

	public void ReceivePoint(int point)
    {
		score += point;
		GamePlayController.instance.SetScoretTxt(score);
    }
}
