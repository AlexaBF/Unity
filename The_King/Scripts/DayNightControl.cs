using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightControl : MonoBehaviour
{
    //public float target = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) {
            //target = 180;
            StartCoroutine(DayNightRotation());
        }

        /*
        if (target > 0) {
            float ammount = 30 * Time.deltaTime;
            transform.Rotate(ammount, 0, 0);
            target -= ammount;
        }
        */
    }

    IEnumerator DayNightRotation() {
        float target = 180;
        while (target > 0)
        {
            float ammount = 30 * Time.deltaTime;
            transform.Rotate(ammount, 0, 0);
            target -= ammount;
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        transform.parent.Find("Point Light").GetComponent<Light>().enabled = true;
    }
}
