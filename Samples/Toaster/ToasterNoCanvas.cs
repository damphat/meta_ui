using MetaUI;
using UnityEngine;

public class ToasterNoCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Toast.ConfigSeconds = 30;
        this.Toast("Good morning!");
        this.ToastError("Error: C� l?i");
        this.ToastSuccess("Th�nh c�ng r?i !");
    }
}
