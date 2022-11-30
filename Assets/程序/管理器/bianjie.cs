using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TarodevController
{
    public enum FxBj
    {
        左边界,
        右边界
    }
    public class bianjie : MonoBehaviour
    {
        public FxBj fxbj = FxBj.左边界;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        void OnTriggerEnter2D(Collider2D hit)
        {
            if (hit.transform.tag == "鱼")
            {
                if (fxbj == FxBj.左边界)
                {
                    hit.GetComponent<YV>().fx = Fangxiang.右;
                    hit.transform.position = new Vector2(hit.transform.position.x , hit.transform.position.y+Random.Range(-1, 1));
                }
                if (fxbj == FxBj.右边界)
                {
                    hit.GetComponent<YV>().fx = Fangxiang.左;
                    hit.transform.position = new Vector2(hit.transform.position.x , hit.transform.position.y+Random.Range(-1, 1));
                }
            }
        }
    }
}
