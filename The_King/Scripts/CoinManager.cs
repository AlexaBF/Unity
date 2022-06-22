using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        /*
        int count = transform.childCount;
        for (int k = 0; k < count; k++)
        {
            CoinController coin = transform.GetChild(k).GetComponent<CoinController>();
            coin.RandomColor();
        }
        */

        player = GameObject.Find("FPSController");
        if (player != null) Debug.Log(player.ToString());

        BroadcastMessage("RandomColor");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
