using System;
using MetaUI;
using MetaUI.Sigos;
using UnityEngine;

public class TodoApp : MonoBehaviour
{
    private void Awake()
    {
        this.Toast("TodoApp.Awake");
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

            var store = GetComponent<StoreBehavior>().store;
            Debug.Log("TodoApp");
            Debug.Log(store);
            store.Set(Sigo.Create("todos", todos, "input", input));
            Debug.Log(store);
        }
        {
            var store = GetComponent<StoreBehavior>().store;
            var input = store.At("input");
            Debug.Log(input);
        }
    }

    private void OnEnable()
    {
        this.Toast("TodoApp.OnEnable");
    }

    public void Start()
    {
        this.Toast("TodoApp.Start");
    }

}

 