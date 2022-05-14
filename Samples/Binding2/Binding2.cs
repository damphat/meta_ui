using System.Collections;
using System.Collections.Generic;
using MetaUI;
using MetaUI.Generic;
using UnityEngine;

public class Binding2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.Get("objects").EachChild(child =>
        {
            Debug.Log(child.Title?.Get());
        });
    }
}
