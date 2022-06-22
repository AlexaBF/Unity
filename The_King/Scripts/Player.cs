using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Transform FPC;
    CoinManager coinManager;

    // Start is called before the first frame update
    void Start()
    {
        FPC = transform.Find("FirstPersonCharacter");
        if (FPC != null) Debug.Log(FPC.ToString());

        coinManager = GameObject.FindObjectOfType<CoinManager>();
        if (coinManager != null) Debug.Log(coinManager.ToString());

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
