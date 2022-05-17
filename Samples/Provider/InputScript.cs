using System.Collections;
using System.Collections.Generic;
using MetaUI;
using MetaUI.Sigos;
using UnityEngine;

public class InputScript : MonoBehaviour
{
    private Store store;

    private Store input;
    // Start is called before the first frame update
    
    private void Awake()
    {
        this.Toast("Input.Awake");
    }

    private void OnEnable()
    {
        this.Toast("Input.OnEnable");
        var input = GetComponentInParent<StoreBehavior>().At("input/input");
        
        this.Get("InputField").ValueString.
    }
    
    void Start()
    {
        this.Toast("Input.Start");
        store = GetComponentInParent<StoreBehavior>().At("todos");
        
        Debug.Log(store);
    }

}
