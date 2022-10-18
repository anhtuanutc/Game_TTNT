using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClick : MonoBehaviour
{
    public void CloseParent()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void ChangeActive(GameObject obj)
    {
        obj.SetActive(!obj.activeInHierarchy);
    }

}
