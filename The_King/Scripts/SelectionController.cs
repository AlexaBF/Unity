using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo)) {
                hitInfo.transform.SendMessage("DoAction", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
