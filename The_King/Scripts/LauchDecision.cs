using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauchDecision : MonoBehaviour
{
    public GameObject decisionPanelPrefab;
    Transform canvas;

    private void Start()
    {
        canvas = GameObject.Find("Canvas").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Instantiate(decisionPanelPrefab, canvas);
        }
    }
}
