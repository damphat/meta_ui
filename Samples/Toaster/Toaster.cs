#region using

using MetaUI;
using UnityEngine;

#endregion

public class Toaster : MonoBehaviour
{
    private void Start()
    {
        this.Get("**/$long").Clicked(() =>
            Toast.Info("This is a very long long long long long long long long long message!"));
        this.Get("**/$short").Clicked(() => Toast.Info("?? Short message ??"));
        this.Get("**/$lines").Clicked(() => Toast.Info("One\nTwo\rThree\r\nFour\n\rFive"));
        this.Get("**/$error").Clicked(() => Toast.Error("Error message"));
        this.Get("**/$result").Clicked(() => Toast.Success("Success message"));
    }
}