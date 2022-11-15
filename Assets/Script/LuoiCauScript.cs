using UnityEngine;
using System.Collections;
using DG.Tweening;

public class LuoiCauScript : MonoBehaviour
{
	public float speed;
	public float speedMin;
	public float speedBegin;
	public Vector2 velocity;
	public float maxY;
	public Transform target;
	public DayCau day_cau;
	public Transform point;

	public int type;

	public bool isUpSpeed;
	public float timeUpSpeed;

	public Transform best_resource = null;
	float best_dis = float.MaxValue;

	Vector3 prePosition;
	int number_play;
	bool is_drop = false, selected;

	void Start () {
		isUpSpeed = false;
		prePosition = transform.localPosition;
		number_play = Prefs.NumberPlayer;
	}

	public IEnumerator TimeUpSpeed ()
	{
		while(true){
			if(isUpSpeed)
			{
				timeUpSpeed = timeUpSpeed - 1;
				if(timeUpSpeed <= 0)
					isUpSpeed = false;
			}
			yield return new WaitForSeconds (1);
		}
	}
	
	public void ResetSpeed()
	{
		speed = speedBegin;
	}

	void Update () {
		CheckKeoCauXong();
			
		CheckDrop();

		CheckMoveOutCameraView();
	}
	void FixedUpdate() {
		if(day_cau.typeAction == TypeAction.ThaCau || day_cau.typeAction == TypeAction.KeoCau)
				GetComponent<Rigidbody2D>().velocity = velocity * speed * .5f;
	}


	bool CheckPosOutBound() {
		return  gameObject.GetComponent<Renderer>().isVisible ;
	}

	void CheckDrop() {
        if (day_cau.computer)
        {
			AIComputer();
        }
        else
        {
			switch (number_play)
			{
				case 1:
					is_drop = Input.GetKeyDown(KeyCode.Space);
					break;
				case 2:
					is_drop = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
					break;
				default:
					is_drop = Input.GetMouseButton(0);
					break;
			}
		}
		if (is_drop && day_cau.typeAction == TypeAction.Nghi && day_cau.my_turn)
		{
			is_drop = false;
			day_cau.typeAction = TypeAction.ThaCau;
			velocity = new Vector2(transform.position.x - target.position.x, 
			                       transform.position.y - target.position.y);
			velocity.Normalize();
			ResetSpeed();
		}
	}
	//kiem tra khi luoi cau ra ngoai tam nhin cua camera
	void CheckMoveOutCameraView() {
		if(day_cau.typeAction == TypeAction.ThaCau && !CheckPosOutBound())
		{
			day_cau.typeAction = TypeAction.KeoCau;
			velocity = -velocity;
			GamePlayController.instance.AniPull();
		}
	}

	//kiem tra khi luoi ca keo len mat nuoc
	void CheckKeoCauXong() {
		if(transform.localPosition.y > maxY && day_cau.typeAction == TypeAction.KeoCau) {
			Debug.Log("keo cau xong");
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			day_cau.ResetDayCau();
			transform.localPosition = prePosition;
			selected = false;
			best_resource = null;
		}
	}

	void AIComputer()
	{
        if (!day_cau.my_turn)
        {
			return;
        }
		if (GamePlayController.instance.GetRound() > 6 && !selected)
        {
			selected = true;
			DOVirtual.DelayedCall(Random.Range(1, 3), () =>
			{
				is_drop = true;
			});
			return;
		}
        if (selected)
		{
			RaycastHit2D hit = Physics2D.Raycast(transform.position, point.position - transform.position, 15, LayerMask.GetMask("resource"));
			if (hit && hit.transform == best_resource && day_cau.typeAction == TypeAction.Nghi)
            {
				is_drop = true;
            }
        }
        if (best_resource)
        {
			return;
        }
		selected = true;
        foreach (Transform item in GamePlayController.instance.parent_resource)
		{
			if (Vector2.Distance(transform.position, item.position) < best_dis)
			{
				best_dis = Vector2.Distance(transform.position, item.position);
				best_resource = item;
			}
			//RaycastHit2D hit = Physics2D.Raycast(transform.position, item.position - transform.position, 10);
			//if (hit.collider.CompareTag("resource"))
   //         {
   //         }
        }
		best_dis = float.MaxValue;
    }
}
