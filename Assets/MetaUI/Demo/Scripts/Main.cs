#region

using com.damphat.MetaUI;
using UnityEngine;

#endregion

public class Main : MonoBehaviour
{
    private string current;
    private string email;
    private string password;

    private void Start()
    {
        var cv = UI.Canvas();

        var login = cv.Get("signInPanel").Show(() => current == null);

        login.Get("google").Click(() => current = "damphat@gmail.com");

        login.Get("email").Changed(value => email = value);

        login.Get("password").Changed(value => password = value);

        login.Get("signIn")
            .Click(() => current = email)
            .Disable(() => string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password));

        var game = cv.Get("game").Show(() => current != null);

        game.Get("current").Text(() => current);

        game.Get("signOut")
            .Text(() => $"Sign Out {current}")
            .Click(() => current = null);

        var show = true;

        var leftPanel = cv.Get("leftPanel");

        leftPanel.Get("toggle")
            .Text(() => show ? "<<" : ">>")
            .Click(() => show = !show);

        var commands = leftPanel.Get("commands").Show(() => show);

        commands.Add()
            .Text("www.damphat.com")
            .Click(() => Application.OpenURL("www.damphat.com"));

        commands.Add()
            .Text("Exit")
            .Click(Application.Quit);

        var rightPanel = cv.Get("rightPanel");
        var value = "init";
        rightPanel.Get("input1").Value(() => value).Changed(v => value = v);
        rightPanel.Get("input2").Value(() => value).Changed(v => value = v);
    }
}