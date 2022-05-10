using System.Collections;
using System.Collections.Generic;
using MetaUI;
using UnityEngine;

public class DropdownTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var index = 0;
        var d1 = this.Get("Dropdown1");
        var d2 = this.Get("Dropdown2");

        d1.ValueInt.Set(() => index);
        d2.ValueInt.Set(() => index);

        d1.ValueInt.Get(v => index = v);
        d2.ValueInt.Get(v => index = v);



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
