using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollUI : MonoBehaviour
{
    public RawImage back, front;

    void Update()
    {
        if (WhereAmI.instance.phase == 0)
        {
            back.uvRect = new Rect(0.05f * Time.time, 0, 1, 1);
            front.uvRect = new Rect(0.08f * Time.time, 0, 1, 1);
        }

        if (WhereAmI.instance.phase == 1 || WhereAmI.instance.phase == 2)
        {
            back.uvRect = new Rect(0.05f * Time.time, 0, 1, 1);
        }
    }
}
