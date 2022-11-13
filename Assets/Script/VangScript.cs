using UnityEngine;
using System.Collections;
using DG.Tweening;

public class VangScript : MonoBehaviour
{
    public bool isMoveFollow = false;
    public float maxY;
    public int score;
    public float speed;
    // Use this for initialization

    Transform t_luoi_cau;
    DayCau day_cau;

    void Start()
    {
        isMoveFollow = false;
    }

    void Update()
    {
        MoveFllowTarget();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LuoiCau") && !isMoveFollow)
        {
            t_luoi_cau = other.transform;
            day_cau = other.transform.parent.GetComponent<DayCau>();
            LuoiCauScript luoi_cau = other.GetComponent<LuoiCauScript>();
            isMoveFollow = true;
            day_cau.typeAction = TypeAction.KeoCau;
            luoi_cau.velocity = -luoi_cau.velocity;
            luoi_cau.speed -= speed;
            GamePlayController.instance.AniPull(false);
        }
    }

    void MoveFllowTarget()
    {
        if (isMoveFollow)
        {
            GamePlayController game_control = GamePlayController.instance;
            Transform target = t_luoi_cau;
            if (!day_cau.my_turn)
            {
                target.GetComponent<LuoiCauScript>().ResetSpeed();
                isMoveFollow = false;
                return;
            }
            Quaternion tg = Quaternion.Euler(target.parent.transform.rotation.x, target.parent.transform.rotation.y,
                                             90 + target.parent.transform.rotation.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, tg, 0.5f);
            transform.position = new Vector3(target.position.x,
                                             target.position.y - gameObject.GetComponent<Collider2D>().bounds.size.y / 2,
                                             transform.position.z);
            if (day_cau.typeAction == TypeAction.Nghi)
            {
                day_cau.ReceivePoint(score);
                game_control.StopAni();
                Destroy(gameObject);
                DOVirtual.DelayedCall(.2f, () => game_control.CheckResource());
            }
        }
    }
}
