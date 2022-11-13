using UnityEngine;
using System.Collections;

public class LuoiCauScript : MonoBehaviour
{
	public float speed;
	public float speedMin;
	public float speedBegin;
	public Vector2 velocity;
	public float maxY;
	public Transform target;
	public DayCau day_cau;

	public int type;

	public bool isUpSpeed;
	public float timeUpSpeed;

	Vector3 prePosition;

	void Start () {
		isUpSpeed = false;
		prePosition = transform.localPosition; 
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
			
		CheckTouchScene();

		CheckMoveOutCameraView();
	}
	void FixedUpdate() {
		if(day_cau.typeAction == TypeAction.ThaCau || day_cau.typeAction == TypeAction.KeoCau)
				GetComponent<Rigidbody2D>().velocity = velocity * speed * .5f;
	}


	bool CheckPosOutBound() {
		return  gameObject.GetComponent<Renderer>().isVisible ;
	}

	void CheckTouchScene() {
		bool istouch = Input.GetMouseButtonDown(0);
		if (istouch && day_cau.typeAction == TypeAction.Nghi && day_cau.my_turn)
		{
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
		}
	}

}
