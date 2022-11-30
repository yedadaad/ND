using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum gq
{
    标题,
    第一关,
    第二关,
    第三关,
    第四关,
    第五关
}
public class GQ : MonoBehaviour
{
    public gq 关卡;
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag == "Player")
        {
            switch(关卡)
            {
                case gq.标题:
                //转场特效
                Biaoti();
                return;
                case gq.第一关:
                Gq1();
                return;
                case gq.第二关:
                Gq2();
                return;
                case gq.第三关:
                Gq3();
                return;
                case gq.第四关:
                Gq4();
                return;
                case gq.第五关:
                Gq5();
                return;
            }
        }
    }
    public void Biaoti()
    {
        SceneManager.LoadScene(0);
    }
    public void Gq1()
    {
        SceneManager.LoadScene(1);
    }
    public void Gq2()
    {
        SceneManager.LoadScene(2);
    }
    public void Gq3()
    {
        SceneManager.LoadScene(3);
    }

    public void Gq4()
    {
        SceneManager.LoadScene(4);
    }
    public void Gq5()
    {
        SceneManager.LoadScene(5);
    }
}
