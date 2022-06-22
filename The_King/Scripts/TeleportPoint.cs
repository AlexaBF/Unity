using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    public KeyCode teleportKey;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) Debug.LogError("TeleportPoint can't find Player tag");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(teleportKey))
        {
            player.SetActive(false);
            player.transform.position = this.transform.position;
            player.SetActive(true);
        }
    }
}
