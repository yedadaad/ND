using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mt3 : MonoBehaviour
{
    public GameObject mt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation=mt.transform.rotation;
    }
}
