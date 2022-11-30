using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TarodevController
{
    public class Aim : MonoBehaviour
    {
        public bool isZhanli;
        public YDL dyl;
        Animator anim;
        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isZhanli)
            {
                anim.SetBool("站立钓鱼", true);
                //关闭坐着钓鱼
            }
            else
            {
                anim.SetBool("站立钓鱼", false);
                //关闭坐着钓鱼
            }
            if(dyl.dyl==DYL.拔河)
            {
                anim.SetBool("上钩",true);
            }
            else
            {
                anim.SetBool("上钩",false);
            }
            if(dyl.dyl==DYL.下水)
            {
                anim.SetBool("落水",true);
            }
            else
            {
                anim.SetBool("落水",false);
            }
            if(dyl.dyl==DYL.庆祝)
            {
                anim.SetBool("拉回鱼",true);
            }
            else
            {
                anim.SetBool("拉回鱼",false);
            }
        }
    }
}