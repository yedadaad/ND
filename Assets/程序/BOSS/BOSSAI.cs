using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TarodevController
{
    public enum BOSSai
    {
        发呆,
        左,
        右,
        发射捕鱼网,
        回收捕鱼网,
        撒网,
        收网,
        探测,
        发射鱼叉,
        收回鱼叉,
        漏水,
        死亡
    }
    public class BOSSAI : MonoBehaviour
    {
        public BOSSai ai = BOSSai.发呆;
        public float MoveSpeed;
        public float MaxTime;
        public float MinTime;
        public float SheSpeed;
        public float SheTime;
        public float LaTime;
        public float BuyvqianyaoTime;
        public GameObject byw1;
        public GameObject byw2;
        public GameObject byw3;
        public GameObject _BYW;
        public GameObject Leida;
        public GameObject Yvcha;
        public GameObject Bowen;
        public GameObject Miaozun;
        public GameObject Jieshu;
        public GameObject shengli;
        float _YidonTime;
        bool IE = false;
        bool IE2 = false;
        int dir = 1;
        float axisZ = 0;
        Coroutine _ie;
        GameObject _player;
        public bool _yvcha = false;
        GameObject _mubiao;
        GameObject _yvchafashe;
        bool _byw = false;
        List<GameObject> _bywyv = new List<GameObject>();
        // Start is called before the first frame update
        void Start()
        {
            //_BYW=byw1.transform.parent.gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            Ai();
        }
        void Ai()
        {
            switch (ai)
            {
                case BOSSai.发呆:
                    StopAllCoroutines();
                    IE2 = false;
                    IE = false;
                    break;
                case BOSSai.左:
                    MoveZuo();
                    //随机走一段距离
                    //碰到边界掉头
                    //走完一定时间开始捕鱼
                    break;
                case BOSSai.右:
                    MoveYou();
                    break;
                case BOSSai.发射捕鱼网:
                    if (IE == false)
                    {
                        _byw = true;
                        axisZ = 0;
                        _ie = StartCoroutine(Fasheyaohuang());
                    }
                    if (IE2 == false)
                    {
                        Quaternion quaternion = new Quaternion(0, 0, 0, 0);
                        axisZ += 70f * Time.deltaTime * dir;
                        if (axisZ >= 45f)
                        {
                            dir = -1;
                        }
                        if (axisZ <= -45f)
                        {
                            dir = 1;
                        }
                        axisZ = ClampAngle(axisZ, -45f, 45f);
                        quaternion = Quaternion.Euler(_BYW.transform.localEulerAngles.x, _BYW.transform.localEulerAngles.y, axisZ);
                        _BYW.transform.localRotation = quaternion;
                    }
                    else
                    {
                        _BYW.transform.Translate(new Vector3(0, -1, 0) * SheSpeed * Time.deltaTime);
                        //改变捕鱼网形态
                    }
                    //渔网来回晃荡一段时间
                    //捕鱼网发射
                    //如果没捕捉到鱼，迅速返回
                    //如果捕捉到鱼，按鱼的数量改变返回速度
                    break;
                case BOSSai.回收捕鱼网:
                    if (IE == false)
                    {
                        _byw = false;
                        StartCoroutine(yvwanghuishou());
                    }
                    break;
                case BOSSai.撒网:
                    //撒网后船会移动一段距离
                    //然后断开网
                    //网会减速，待的时间过长会缠绕
                    //在渔船第二次回到渔网的范围会收网，网住所有的鱼并且切换到捕鱼状态
                    break;
                case BOSSai.收网:
                    break;
                case BOSSai.探测:
                    if (IE == false)
                    {
                        StartCoroutine(Lieda());
                    }
                    //伸出雷达
                    //探测到鱼就发射鱼叉
                    //发射一圈波纹
                    break;
                case BOSSai.发射鱼叉:
                    //击中目标将其光速拉回
                    if (IE == false)
                    {
                        StartCoroutine(FasheYvcha());
                    }
                    if (_yvcha == true)
                    {
                        ai = BOSSai.收回鱼叉;
                    }
                    break;
                case BOSSai.收回鱼叉:
                    IE = true;
                    _yvcha = false;
                    StopAllCoroutines();
                    IE2 = true;
                    if (IE2 == true)
                    {
                        StartCoroutine(HuishouYvcha());
                    }
                    break;
                case BOSSai.漏水:
                    break;
                case BOSSai.死亡:
                shengli.SetActive(true);
                    break;
            }
        }
        void MoveZuo()
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(300f, transform.position.y), MoveSpeed * Time.deltaTime);
            transform.rotation = new Quaternion(0, 0, 0, 0);
            if (IE == false)
            {
                StartCoroutine(Yanshi());
            }
        }
        IEnumerator Yanshi()
        {
            IE = true;
            _YidonTime = Random.Range(MinTime, MaxTime);
            yield return new WaitForSeconds(_YidonTime);
            RandomZhuangtai();
            IE = false;
            StopAllCoroutines();
            //随机三选一
            // if (Random.Range(-1f, 1f) > 0)
            // {
            // }
            // else
            // {
            //     ai = BOSSai.探测;
            // }
        }

        void MoveYou()
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-300f, transform.position.y), MoveSpeed * Time.deltaTime);
            transform.rotation = new Quaternion(0, 180, 0, 0);
            if (IE == false)
            {
                StartCoroutine(Yanshi());
            }
        }
        float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360)
            {
                angle += 360;
            }

            if (angle > 360)
            {
                angle -= 360;
            }
            return Mathf.Clamp(angle, min, max);
        }
        IEnumerator Fasheyaohuang()
        {
            IE = true;
            float _shetime = Random.Range(SheTime * 0.8f, SheTime * 1.2f);
            Debug.Log("摇晃");
            yield return new WaitForSeconds(_shetime);
            StartCoroutine(Fasheyvwang());
            StopCoroutine(_ie);
            IE2 = true;
        }
        IEnumerator Fasheyvwang()
        {
            Debug.Log("发射");
            byw1.SetActive(false);
            byw2.SetActive(true);
            //_BYW.transform.DOMoveY(-100f,SheSpeed,false);
            yield return new WaitForSeconds(SheTime);
            ai = BOSSai.回收捕鱼网;
            StopAllCoroutines();
            IE2 = false;
            IE = false;
        }
        IEnumerator yvwanghuishou()
        {
            IE = true;
            byw2.SetActive(false);
            byw3.SetActive(true);
            //删除鱼
            //重新游荡
            _BYW.transform.DOMove(_BYW.transform.parent.transform.position, 2f, false);
            _BYW.transform.DORotate(new Vector3(0, 0, 0), 2f, RotateMode.Fast);
            yield return new WaitForSeconds(2f);
            Debug.Log("回收完成");
            ai = BOSSai.发呆;
            StopAllCoroutines();
            IE = false;
            byw3.SetActive(false);
            byw1.SetActive(true);
            for (int i = 0; i < _bywyv.Count; i++)
            {
                Destroy(_bywyv[i]);
            }
            //_bywyv=null;
            //随机切换
            RandomZhuoyou();
        }
        IEnumerator Lieda()
        {
            IE = true;
            Leida.transform.DOLocalMove(new Vector3(0.34f, -0.58f, 0), 0.5f);
            yield return new WaitForSeconds(0.5f);
            //发射雷达
            Bowen.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            Leida.transform.DOLocalMove(new Vector3(0.34f, 1f, 0), 0.5f);
            yield return new WaitForSeconds(1.4f);
            RandomZhuoyou();
            Bowen.SetActive(false);
            IE = false;
        }
        void OnTriggerEnter2D(Collider2D hit)
        {

            if (_byw == true)
            {
                if (hit.tag == "鱼")
                {
                    //将鱼绑到中心
                    hit.GetComponent<YV>().fx = Fangxiang.被钓;
                    //拖回船上后删除
                    _bywyv.Add(hit.gameObject);
                    hit.transform.parent = _BYW.transform;
                    hit.transform.DOLocalMove(new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-1.5f, -0.5f), 0), 0.1f);
                    return;
                }
                if (hit.tag == "Player")
                {
                    Debug.Log("发现玩家");
                    //捕鱼网判定
                    //将玩家绑到中心，并且立刻返回
                    StopAllCoroutines();
                    _player = hit.gameObject;
                    IE2 = false;
                    IE = false;
                    StartCoroutine(Daibu());
                    Jieshu.SetActive(true);
                    ai = BOSSai.回收捕鱼网;
                    ai = BOSSai.死亡;
                    //返回后游戏结束
                    return;
                }
                return;
            }
            if (hit.tag == "Player")
            {
                _player = hit.gameObject;
                StopAllCoroutines();
                Bowen.SetActive(false);
                Leida.transform.DOLocalMove(new Vector3(0.34f, 1f, 0), 0.5f);
                IE = false;
                //显示定位标志
                _mubiao = Instantiate(Miaozun);
                _mubiao.transform.position = _player.transform.position;
                Destroy(_mubiao, 3f);
                ai = BOSSai.发射鱼叉;
            }
        }
        IEnumerator FasheYvcha()
        {
            IE = true;
            //鱼叉瞄准
            Yvcha.SetActive(true);
            Yvcha.transform.DOLookAt(_mubiao.transform.position, 0.5f);
            yield return new WaitForSeconds(1f);
            //发射鱼叉
            _yvchafashe = Instantiate(Yvcha);
            _yvchafashe.transform.position = Yvcha.transform.position;
            _yvchafashe.transform.rotation = Yvcha.transform.rotation;
            _yvchafashe.transform.DOMove(_mubiao.transform.position, 1f);
            Yvcha.SetActive(false);
            //如果命中，启动HuishouYvcha()，关闭FasheYvcha()
            yield return new WaitForSeconds(1.1f);
            //如果过了时间，则代表没命中
            Destroy(_yvchafashe);
            IE = false;
            IE2 = false;
            _player = null;
            _yvcha = false;
            //继续晃荡
            RandomZhuoyou();
        }
        IEnumerator HuishouYvcha()
        {
            IE2 = false;
            //命中玩家，将其拖回
            //将鱼叉固定在玩家头部
            _yvchafashe.transform.parent = _player.transform.GetChild(0);
            _yvchafashe.transform.DOLocalMove(new Vector3(0, 0, 0), 1f);
            _player.transform.DOMove(transform.position, 5f);
            //将玩家拖回后
            //游戏结束
            Jieshu.SetActive(true);
            ai = BOSSai.发呆;
            yield return new WaitForSeconds(1.4f);
        }
        IEnumerator Daibu()
        {
            IE2 = true;
            //_player.transform.position = _BYW.transform.position + new Vector3(0, -1, 0);
            //_player.transform.DOMove(_BYW.transform.position + new Vector3(0, -1, 0),0.1f);
            _player.transform.parent=_BYW.transform;
            _player.transform.DOLocalMove(new Vector3(0, -1, 0), 0.1f);
            //_player.transform.DOLocalMove(_BYW.transform.position+new Vector3(0, -1, 0), 0.1f);
            yield return null;
        }

        void RandomZhuangtai()
        {
            if (Random.Range(-1f, 1f) > 0)
            {
                ai = BOSSai.发射捕鱼网;
            }
            else
            {
                ai = BOSSai.探测;
            }
        }
        void RandomZhuoyou()
        {
            if (Random.Range(-1f, 1f) > 0)
            {
                ai = BOSSai.左;
            }
            else
            {
                ai = BOSSai.右;
            }
        }

    }
}