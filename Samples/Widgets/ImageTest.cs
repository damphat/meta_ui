using MetaUI;
using UnityEngine;

public class ImageTest : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        var c = this.Get("Image");
        c.Title.Set("Hello");
    }
}