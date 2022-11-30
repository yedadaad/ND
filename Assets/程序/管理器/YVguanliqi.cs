using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TarodevController
{
    public class YVguanliqi : MonoBehaviour
    {
        public int YVsl;
        public float JGTime = 1f;
        public float Bianjie = 50f;
        public float MaxH = -5;
        public float MinH = -20;
        public List<GameObject> YV = new List<GameObject>();
        List<GameObject> YVshuliang = new List<GameObject>();
        GameObject _YV;
        int ZY;
        int _ZY;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(shengcheng(JGTime));
        }

        // Update is called once per frame
        void Update()
        {

        }
        IEnumerator shengcheng(float Time)
        {
            for (int i = YVsl; i >= 0; i--)
            {
                //创建一条鱼
                if (Random.Range(-1f, 1f) > 0)
                {
                    ZY = -1;
                }
                else
                {
                    ZY = 1;
                }
                _YV = Instantiate(YV[Random.Range(1, YV.Count)], new Vector2(Bianjie * ZY, Random.Range(MinH, MaxH)), new Quaternion(0, 0, 0, 0));
                _YV.GetComponent<YV>().Speed *= Random.Range(0.8f, 2f);
                //确定生成位置
                if (ZY == -1)
                {
                    _YV.GetComponent<YV>().fx = Fangxiang.右;
                }
                else
                {
                    _YV.GetComponent<YV>().fx = Fangxiang.左;
                }
                //确定方向
                YVshuliang.Add(_YV);
                yield return new WaitForSeconds(Time);
            }
        }
        IEnumerator Jiancha(float Time)
        {
            yield return new WaitForSeconds(Time);
        }
    }
}