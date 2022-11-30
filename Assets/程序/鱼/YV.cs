using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TarodevController
{
    public enum Fangxiang
    {
        左,
        右,
        被钓,
        跟随,
        帮助
    }
    public class YV : MonoBehaviour
    {
        public Fangxiang fx;
        public float Speed = 3f;
        public GameObject Player;
        public float JvLi = 3f;
        public GameObject A;
        public GameObject B;
        // Start is called before the first frame update
        void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            switch (fx)
            {
                case Fangxiang.左:
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(-100f, transform.position.y), Speed * Time.deltaTime);
                    transform.rotation = new Quaternion(0, 180, 0, 0);
                    break;
                case Fangxiang.右:
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(100f, transform.position.y), Speed * Time.deltaTime);
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                    break;
                case Fangxiang.被钓:
                    A.SetActive(true);
                    B.SetActive(false);
                    break;
                case Fangxiang.跟随:
                    A.SetActive(false);
                    B.SetActive(true);
                    if (Player.GetComponent<PlayerController>().DYL != null)
                    {
                        fx = Fangxiang.帮助;
                        Debug.Log("我来帮你");
                    }
                    if ((transform.position - Player.transform.position).sqrMagnitude >= JvLi)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, Speed * Time.deltaTime);
                        if (Player.transform.position.x > transform.position.x)
                        {
                            transform.rotation = new Quaternion(0, 0, 0, 0);
                        }
                        else
                        {
                            transform.rotation = new Quaternion(0, 180, 0, 0);
                        }
                    }
                    break;
                case Fangxiang.帮助:
                    //嘴巴吸附到鱼钩上
                    A.SetActive(false);
                    B.SetActive(false);
                    if (Player.GetComponent<PlayerController>().DYL == null)
                    {
                        fx = Fangxiang.跟随;
                        Debug.Log("我不打扰");
                        return;
                    }
                    transform.position = Vector2.MoveTowards(transform.position, Player.GetComponent<PlayerController>().DYL._yvgou.transform.position, Speed * 2 * Time.deltaTime);
                    //增加玩家挣脱速度
                    Player.GetComponent<PlayerController>().DYL._TuoZhuai += 0.1f * Time.deltaTime;
                    break;
            }
        }
        void OnTriggerEnter2D(Collider2D hit)
        {
            if (hit.transform.tag == "Player")
            {
                if (fx == Fangxiang.被钓)
                {
                    fx = Fangxiang.跟随;
                }
            }
        }
    }
}