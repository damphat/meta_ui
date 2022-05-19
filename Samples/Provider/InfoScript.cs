using MetaUI;
using MetaUI.Sigos;
using UnityEngine;

public class InfoScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var store = GetComponentInParent<StoreProvider>().At("input/input");
        this.Get("InputField1").ValueString.Bind(store);
        this.Get("InputField2").ValueString.Bind(store);
    }
}
