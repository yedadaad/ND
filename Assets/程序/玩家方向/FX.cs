using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TarodevController {
public class FX : MonoBehaviour
{
    private PlayerController _player;

    // Start is called before the first frame update
    void Awake() 
    {
        _player = GetComponentInParent<PlayerController>();
    } 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_player.Input.X>0)
        {
            this.transform.rotation=new Quaternion(0,0,0,0);
        }
        if(_player.Input.X<0)
        {
            this.transform.rotation=new Quaternion(0,180,0,0);
        }
    }
}
}
