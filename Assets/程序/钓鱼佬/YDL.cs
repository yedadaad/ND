using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TarodevController
{
    public enum DYL
    {
        钓鱼,
        上钩,
        拔河,
        下水,
        庆祝
    }
    public class YDL : MonoBehaviour
    {
        public GameObject yvgou;
        public DYL dyl = DYL.钓鱼;
        public float XiagouSpeed;
        public float Max;
        public float Min;
        public float ShanggouSpeed;
        float _ShanggouSpeed;
        public float TuoZhuai;
        public float _TuoZhuai;
        public Transform yvgouweizhi;
        public GameObject _yvgou;
        float shengdu;
        bool Xiagou;
        bool Shanggou;
        GameObject _yv;
        float _y;
        float _x;
        bool IE = false;
        // Start is called before the first frame update
        void Start()
        {
            _ShanggouSpeed = ShanggouSpeed;
        }


        // Update is called once per frame
        void Update()
        {
            switch (dyl)
            {
                case DYL.钓鱼:
                    ShuaiGou();
                    dyl = DYL.上钩;
                    shengdu = Random.Range(Min, Max);
                    Xiagou = true;
                    IE=false;
                    break;
                case DYL.上钩:
                    if (Xiagou == true)
                    {
                        _yvgou.transform.position = Vector2.MoveTowards(_yvgou.transform.position, new Vector2(yvgouweizhi.transform.position.x, shengdu+yvgouweizhi.transform.position.y), XiagouSpeed * Time.deltaTime);
                        if (_yvgou.transform.position.y <= shengdu + Mathf.Epsilon)
                        {
                            Xiagou = false;
                        }
                    }
                    break;
                case DYL.拔河:
                    //将鱼拖拽过来
                    //如果是玩家，记录玩家的操作，产生拖拽值
                    if (Shanggou == true)
                    {
                        if(_yv==null)
                        {
                            Destroy(transform.gameObject);
                        }
                        if (_yv.transform.tag == "Player")
                        {

                            _yv.transform.position = Vector2.MoveTowards(_yv.transform.position, yvgouweizhi.position, _ShanggouSpeed * Time.deltaTime);
                            //记录玩家操作，产生拖拽值
                            _TuoZhuai += Mathf.Abs(_yv.GetComponent<PlayerController>().Input.X);
                            _TuoZhuai += Mathf.Abs(_yv.GetComponent<PlayerController>().Input.Y);
                            _TuoZhuai += Mathf.Abs(_x - _yvgou.transform.position.x) * 50;
                            _TuoZhuai += Mathf.Abs(_y - _yvgou.transform.position.y) * 50;
                            _x = _yvgou.transform.position.x;
                            _y = _yvgou.transform.position.y;
                            if (_TuoZhuai >= TuoZhuai)
                            {
                                dyl = DYL.下水;
                            }
                            if (_ShanggouSpeed < ShanggouSpeed * 2)
                            {
                                _ShanggouSpeed += Mathf.Abs(_yv.GetComponent<PlayerController>().Input.X / 100);
                                if (ShanggouSpeed < _ShanggouSpeed)
                                {
                                    _ShanggouSpeed -= Mathf.Abs(_yv.GetComponent<PlayerController>().Input.Y / 200);
                                }

                            }
                        }
                        if (_yv.transform.tag == "鱼")
                        {
                            if (_yv.GetComponent<YV>().fx == Fangxiang.跟随)
                            {
                                //钩子回去，人重新甩钩子
                                _yvgou.transform.parent = yvgouweizhi;
                                _yvgou.transform.position = Vector2.MoveTowards(_yvgou.transform.position, yvgouweizhi.transform.position, _ShanggouSpeed * Time.deltaTime);
                                if ((_yvgou.transform.position - yvgouweizhi.position).sqrMagnitude <= 0.1f)
                                {
                                    dyl = DYL.庆祝;
                                    _yv = null;
                                    return;
                                }
                            }
                            else
                            {
                                _yv.transform.position = Vector2.MoveTowards(_yv.transform.position, yvgouweizhi.transform.position, _ShanggouSpeed * Time.deltaTime);
                            }
                        }
                        if ((_yv.transform.position - yvgouweizhi.position).sqrMagnitude <= 0.1f)
                        {
                            Shanggou = false;
                            Debug.Log("钓到鱼了");
                            //游戏结束
                            if (_yv.transform.tag == "鱼")
                            {
                                Destroy(_yv.gameObject);
                                dyl = DYL.庆祝;
                            }
                        }
                    }
                    break;
                case DYL.下水:
                    //播放钓鱼佬掉下水
                    //解除鱼钩
                    Shanggou = false;
                    _yvgou.transform.parent = transform;
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, -20), _ShanggouSpeed/5 * Time.deltaTime);
                    Destroy(this.gameObject, 3f);
                    break;
                case DYL.庆祝:
                    if (IE == false)
                    {
                        StartCoroutine(qz());
                    }
                    break;
            }
        }
        void ShuaiGou()
        {
            if (_yvgou == null)
            {
                _yvgou = Instantiate(yvgou);
                 _yvgou.transform.parent = transform;
            _yvgou.transform.position =  yvgouweizhi.transform.position;
            _yvgou.GetComponent<Line>().targetpoing = yvgouweizhi;
            }
            //StartCoroutine(sg());
            //      _yvgou.transform.rotation=new Quaternion(0,0,0,0);
            _yvgou.GetComponent<CapsuleCollider2D>().enabled = true;
        }
        void OnTriggerEnter2D(Collider2D hit)
        {
            if (dyl == DYL.拔河)
                return;
            if (hit.transform.tag == "Player")
            {
                _yv = hit.gameObject;
                _yv.GetComponent<PlayerController>().DYL = this;
                //拔河
                //将鱼钩挂到鱼的嘴上
                dyl = DYL.拔河;
                _yvgou.transform.parent = _yv.transform.GetChild(0).GetChild(0);
                _yvgou.transform.localPosition = new Vector2(0, 0);
                Shanggou = true;
                _x = _yvgou.transform.position.x;
                _y = _yvgou.transform.position.y;
                _ShanggouSpeed = ShanggouSpeed;
                _yvgou.GetComponent<CapsuleCollider2D>().enabled = false;
            }
            if (hit.transform.tag == "鱼")
            {
                _yv = hit.gameObject;
                _yvgou.transform.parent = _yv.transform.GetChild(0).GetChild(0);
                _yvgou.transform.localPosition = new Vector2(0, 0);
                Shanggou = true;
                _yv.GetComponent<YV>().fx = Fangxiang.被钓;
                dyl = DYL.拔河;
                _ShanggouSpeed = ShanggouSpeed / 5;
                _yvgou.GetComponent<CapsuleCollider2D>().enabled = false;
            }
        }
        IEnumerator qz()
        {
            IE = true;
            yield return new WaitForSeconds(1.45f);
            StopAllCoroutines();
            dyl = DYL.钓鱼;
        }
        IEnumerator sg()
        {
            yield return new WaitForSeconds(0.5f);
            StopAllCoroutines();

        }
    }
}