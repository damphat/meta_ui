using MetaUI;
using UnityEngine;

public class DropdownBug : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        var index = this.Value(1);

        this.Get("Dropdown1").ValueInt.SetSrc(index);
        this.Get("Dropdown1").ValueInt.Add(v => { this.Toast(v); });

        this.Get("Dropdown2").ValueInt.SetSrc(index);
        this.Get("Dropdown2").ValueInt.Add(v => { this.Toast(v); });
    }
}