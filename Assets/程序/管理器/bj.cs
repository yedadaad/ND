using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class bj : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    public GameObject A;
 
    void Start()
    {
        image = GetComponent<Image>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        A.SetActive(true);
        Debug.Log(1);
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        A.SetActive(false);
        Debug.Log(2);
    }
}
