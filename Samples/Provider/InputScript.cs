using System;
using System.Collections;
using System.Collections.Generic;
using MetaUI;
using MetaUI.Sigos;
using UnityEngine;

public class InputScript : MonoBehaviour
{
    private void Start()
    {
        var store = GetComponentInParent<StoreProvider>();
        var todosRef = store.At("todos");
        var inputRef = store.At("input/input");
        
        this.Get("InputField").ValueString.Bind(inputRef);

        this.Get("Add").Clicked(() =>
        {
            var input = inputRef.Get() as string;
            if (!string.IsNullOrEmpty(input))
            {
                var key = Guid.NewGuid().ToString();
                todosRef.Set(key, Sigo.Create("id", key, "text", inputRef.Get(), "done", false));
                inputRef.Set("");    
            }
            
        });
    }
}
