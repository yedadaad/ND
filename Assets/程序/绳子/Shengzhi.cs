using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shengzhi : MonoBehaviour
{
    public GameObject shengzhi;
    public int jieshu;
    public float jiange;
    public GameObject A;
    public GameObject B;
    GameObject _shengzhi;
    GameObject _shengzhi1;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=jieshu;i>0;i--)
        {
            _shengzhi = Instantiate(shengzhi,transform.parent);
            _shengzhi.transform.position+=new Vector3(0,jiange*i,0);
            if(_shengzhi1!=null)
            _shengzhi.GetComponent<HingeJoint2D>().connectedBody=_shengzhi1.GetComponent<Rigidbody2D>();
            _shengzhi1=_shengzhi;
            if(i==1)
            {
                _shengzhi.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.FreezeAll;
                //_shengzhi.GetComponent<HingeJoint2D>().connectedBody=B.GetComponent<Rigidbody2D>();
            }
            if(i==jieshu)
            {
                _shengzhi.GetComponent<HingeJoint2D>().enabled=false;
                //_shengzhi.GetComponent<HingeJoint2D>().connectedBody=A.GetComponent<Rigidbody2D>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
