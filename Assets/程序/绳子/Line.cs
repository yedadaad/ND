using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Line : MonoBehaviour
{
    public LineRenderer LineRenderer;
    public Transform targetpoing;
    public Transform startpoint;
    public float z=0f;
    // Start is called before the first frame update
    void Start()
    {
        LineRenderer = GetComponent<LineRenderer>();
        LineRenderer.useWorldSpace = true;
    }

    // Update is called once per frame
    void Update()
    {
        LineRenderer.SetPosition(0, startpoint.position);
        LineRenderer.SetPosition(1, targetpoing.position);
        transform.rotation=new Quaternion(0,0,0,transform.rotation.w);
    }
}
