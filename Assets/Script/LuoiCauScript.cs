using UnityEngine;
using System.Collections;

public class LuoiCauScript : MonoBehaviour {
	public float speed;
	public float speedMin;
	public float speedBegin;
	public Vector2 velocity;
	public float maxY;
	public Transform target;
	Vector3 prePosition;
	public DayCau day_cau;

	public int type;

	public bool isUpSpeed;
	public float timeUpSpeed;

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
	

	void Update () {
		checkKeoCauXong();
//		if(CGameManager.Instance.gameState == EnumStateGame.Play) 
		{
			checkTouchScene();

			checkMoveOutCameraView();
		}
	}
	void FixedUpdate() {
//		if(CGameManager.Instance.gameState == EnumStateGame.Play) 
		if(day_cau.typeAction == TypeAction.ThaCau || day_cau.typeAction == TypeAction.KeoCau)
				GetComponent<Rigidbody2D>().velocity = velocity * speed;
	}


	bool checkPositionOutBound() {
		return  gameObject.GetComponent<Renderer>().isVisible ;
	}

	void checkTouchScene() {
		bool istouch = Input.GetMouseButtonDown(0);
		
		if (istouch && day_cau.typeAction == TypeAction.Nghi && day_cau.my_turn)
		{
			day_cau.typeAction = TypeAction.ThaCau;
			velocity = new Vector2(transform.position.x - target.position.x, 
			                       transform.position.y - target.position.y);
			velocity.Normalize();
			speed = speedBegin;
		}
	}
	//kiem tra khi luoi cau ra ngoai tam nhin cua camera
	void checkMoveOutCameraView() {
		if(day_cau.typeAction == TypeAction.ThaCau && !checkPositionOutBound())
		{
			day_cau.typeAction = TypeAction.KeoCau;
			velocity = -velocity;
		}
	}

	//kiem tra khi luoi ca keo len mat nuoc
	void checkKeoCauXong() {
		if(transform.localPosition.y > maxY && day_cau.typeAction == TypeAction.KeoCau) {
			Debug.Log("keo cau xong");
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			day_cau.ResetDayCau();
			transform.localPosition = prePosition;
            
		}
	}
}
