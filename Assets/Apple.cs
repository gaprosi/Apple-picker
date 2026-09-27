using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public static float bottomY = -20f;

    void Update()
    {
        if (transform.position.y < bottomY)
        {
            Destroy(this.gameObject);

            // Tell ApplePicker that an apple was missed
            ApplePicker apScript =
                Camera.main.GetComponent<ApplePicker>();

            apScript.AppleMissed();
        }
    }
}