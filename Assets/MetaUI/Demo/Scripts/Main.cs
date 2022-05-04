#region

using System;
using System.Threading.Tasks;
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

        login.Get("google").Clicked(() => current = "damphat@gmail.com");

        login.Get("email").Changed(value => email = value);

        login.Get("password").Changed(value => password = value);

        login.Get("signIn")
            .Clicked(() => current = email)
            .Disable(() => string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password));

        var game = cv.Get("game").Show(() => current != null);

        game.Get("current").Text(() => current);

        game.Get("signOut")
            .Text(() => $"Sign Out {current}")
            .Clicked(() => current = null);

        var show = true;

        var leftPanel = cv.Get("leftPanel");

        leftPanel.Get("toggle")
            .Text(() => show ? "<<" : ">>")
            .Clicked(() =>
            {
                show = !show;
            });

        var commands = leftPanel.Get("commands").Show(() => show);

        commands.Add()
            .Text("www.damphat.com")
            .Clicked(() => Application.OpenURL("www.damphat.com"));

        commands.Add()
            .Text("Toast(Hello, 1)")
            .Clicked(() => UI.Toast("Hello everybody! this is a long long long message 1111111111111 22222222222222 33333333333 4444444", 10));

        commands.Add()
            .Text("Toast({first: Phat, last: Dam })")
            .Clicked(() => UI.Toast("{\n  first: Phat,\n  last: Dam\n}", 10));

        commands.Add()
            .Text("Exit")
            .Clicked(Application.Quit);

        commands.Add()
            .Text("() => true")
            .Clicked(() => true);

        commands.Add()
            .Text("() => throw new Exception(an error)")
            .Clicked(() => throw new Exception("an error"));

        commands.Add()
            .Text("() => delay(3000)")
            .Clicked(async () =>
            {
                await Task.Delay(3000);
            });

        commands.Add()
            .Text("() => delay(3000) then return true")
            .Clicked(async () =>
            {
                await Task.Delay(3000);
                return true;
            });

        commands.Add()
            .Text("() => delay(3000) then throw")
            .Clicked(async () =>
            {
                await Task.Delay(3000);
                throw new Exception("an error");
            });

        var rightPanel = cv.Get("rightPanel");
        var value = "init";
        rightPanel.Get("input1").Value(() => value).Changed(v => value = v);
        rightPanel.Get("input2").Value(() => value).Changed(v => value = v);
    }
}