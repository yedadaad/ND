using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TarodevController
{
public class G : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(Player.GetComponent<PlayerController>());
    }
}
}
