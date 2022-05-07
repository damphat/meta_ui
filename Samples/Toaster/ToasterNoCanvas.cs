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
        this.ToastError("Error: Có l?i");
        this.ToastSuccess("Thành công r?i !");
    }
}