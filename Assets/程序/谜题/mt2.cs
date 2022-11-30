using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mt2 : MonoBehaviour
{
    public GameObject men;
    void OnTriggerExit2D(Collider2D hit)
    {
        if (hit.tag == "解密道具")
        {
            Destroy(men);
            Destroy(transform.gameObject);
        }
    }
}
