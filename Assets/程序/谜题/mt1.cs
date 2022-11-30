using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mt1 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag == "解密道具")
        {
            //hit.GetComponent<BoxCollider2D>().enabled=false;
            //Destroy(hit.GetComponent<Rigidbody2D>());
            Destroy(transform.gameObject);
        }
    }
}
