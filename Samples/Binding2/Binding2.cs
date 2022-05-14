using MetaUI;
using UnityEngine;

public class Binding2 : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        this.Get("objects").EachChild(child => { Debug.Log(child.Title?.Get()); });
    }
}