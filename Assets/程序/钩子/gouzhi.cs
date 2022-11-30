using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gouzhi : MonoBehaviour
{
    GameObject _any;
    string _name;
    string _SC;
    void OnTriggerEnter2D(Collider2D hit)
    {
        _any = hit.gameObject;
        _name = _any.transform.name;
        PD(name);
    }
    void PD(string name)
    {
        _SC=null;
        for (int i = 0; i < name.Length; i++)
        {
            char zm = name[i];
            _SC+=zm;
            if(i!=name.Length)
            {
                char zm2=name[i+1];
                if(char.IsNumber(zm2))
                {
                    _SC+=zm2;
                    i++;
                }
                else
                {
                    if(char.IsUpper(zm2))
                    {
                        _SC+="1";
                    }
                }
            }

            i++;
        }
        Debug.Log(_SC);
    }
}
