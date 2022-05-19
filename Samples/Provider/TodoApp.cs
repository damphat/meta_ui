using System;
using MetaUI;
using MetaUI.Sigos;
using UnityEngine;

public class TodoApp : MonoBehaviour
{

    private void OnEnable()
    {
        var todos = Sigo.Create(
            0, Sigo.Create(
                "id", 0,
                "text", "Mot hai ba bon nam sau bay tam chin muoi.",
                "done", false
            ),
            1, Sigo.Create("id", 1, "text", "An trua", "done", false),
            2, Sigo.Create("id", 2, "text", "An toi", "done", false)
        );

        var input = Sigo.Create(
            "input", "type your todo ..."
        );

        var store = GetComponent<StoreProvider>().store;
        store.Set(Sigo.Create("todos", todos, "input", input));
    }
}

 