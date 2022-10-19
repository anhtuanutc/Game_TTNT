using UnityEngine;
using System.Collections;

public enum TypeAction { Nghi, ThaCau, KeoCau };
public class DayCau : MonoBehaviour {
	public Transform luoiCau;
	public Vector3 angles;
	public float speed = 5;
	public float angleMax = 70;
	public TypeAction typeAction = TypeAction.Nghi;
	public bool my_turn;
	private Vector3 initAngles;

	LineRenderer rope;

	// Use this for initialization
	void Start () {
		initAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
		rope = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (my_turn)
		{
			rope.SetPosition(0, transform.position);
			rope.SetPosition(1, luoiCau.position);
		}
	}

	void FixedUpdate() {
		if(my_turn && speed > 0 && typeAction == TypeAction.Nghi)
			transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * speed) * angleMax);
	}

	public void ChangeTurn(bool check)
    {
		my_turn = check;
        if (!check)
        {
			transform.rotation = Quaternion.Euler(initAngles);
		}
    }
}
