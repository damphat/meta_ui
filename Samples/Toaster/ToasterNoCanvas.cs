#region using

using MetaUI;
using UnityEngine;

#endregion

public class ToasterNoCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Toast.ConfigSeconds = 30;
        this.Toast("Good morning!");
        this.ToastError("Error: C� l?i");
        this.ToastSuccess("Th�nh c�ng r?i !");
    }
}