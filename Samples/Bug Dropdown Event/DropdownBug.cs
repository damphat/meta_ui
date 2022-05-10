using MetaUI;
using UnityEngine;

public class DropdownBug : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int index = 1;
        
        this.Get("Dropdown1").ValueInt.Set(() => index);
        this.Get("Dropdown1").ValueInt.Get((v) =>
        {
            this.Toast(v);
        });

        this.Get("Dropdown2").ValueInt.Set(() => index);
        this.Get("Dropdown2").ValueInt.Get((v) =>
        {
            this.Toast(v);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
