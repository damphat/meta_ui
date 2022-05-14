using MetaUI;
using UnityEngine;

public class DropdownTest : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        var index = this.Value(0);
        var d1 = this.Get("Dropdown1");
        var d2 = this.Get("Dropdown2");

        d1.ValueInt.SetSrc(index);
        d2.ValueInt.SetSrc(index);

        d1.ValueInt.Add(v => index.Set(v));
        d2.ValueInt.Add(v => index.Set(v));
    }

    // Update is called once per frame
    private void Update()
    {
    }
}