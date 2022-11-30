using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TarodevController
{
    public class YvCha : MonoBehaviour
    {
        public BOSSAI boss;
        void OnTriggerEnter2D(Collider2D hit)
        {
            if (hit.tag == "Player")
            {
                Debug.Log("命中玩家");
                transform.GetComponent<BoxCollider2D>().enabled=false;
                boss._yvcha=true;
            }
        }
    }
}
