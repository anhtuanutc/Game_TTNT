using UnityEngine;
using System.Collections;

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

    void FixedUpdate()
    {
        moveFllowTarget();
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
        }
    }

    void moveFllowTarget()
    {
        if (isMoveFollow)
        {
            if (!day_cau.my_turn)
            {
                isMoveFollow = false;
                return;
            }
            Transform target = t_luoi_cau;
            Quaternion tg = Quaternion.Euler(target.parent.transform.rotation.x, target.parent.transform.rotation.y,
                                             90 + target.parent.transform.rotation.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, tg, 0.5f);
            transform.position = new Vector3(target.position.x,
                                             target.position.y - gameObject.GetComponent<Collider2D>().bounds.size.y / 2,
                                             transform.position.z);
            if (day_cau.typeAction == TypeAction.Nghi)
            {
                day_cau.ReceivePoint(score);
                Destroy(gameObject);
            }
        }
    }
}
