using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TarodevController
{
    public class JY : MonoBehaviour
    {
        public GameObject JY1;
        public GameObject JY2;
        public GameObject Z;
        public GameObject boss;
        // Start is called before the first frame update
        void Start()
        {
            JY1.SetActive(true);
            JY2.SetActive(false);
            Z.SetActive(true);
        }
        void OnTriggerEnter2D(Collider2D hit)
        {
            if (hit.tag == "捕鱼船")
            {
                boss.GetComponent<BOSSAI>().ai=BOSSai.死亡;
                boss.transform.DORotate(new Vector3(0,0,10000f),30F,RotateMode.FastBeyond360);
                boss.transform.DOMove(50*(boss.transform.position-transform.position),10f);
                //播放胜利
            }
            if (hit.tag == "鱼叉")
            {
                Destroy(hit.transform.gameObject);
                transform.DOMove(boss.transform.position,1f);
                JY1.SetActive(false);
                JY2.SetActive(true);
                Z.SetActive(false);
                StartCoroutine(SJ());
            }
        }
        IEnumerator SJ()
        {
            yield return new WaitForSeconds(5F);
            JY1.SetActive(true);
            JY2.SetActive(false);
            Z.SetActive(true);
        }
    }
}
