using System.Collections;
using System.Collections.Generic;
using MetaUI;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    // Start is called before the first frame update
    private WrapGameObject text;
    private WrapGameObject input;

    void Start()
    {
        text = this.Get("Text");
        input = this.Get("InputField");

        input.Title.Set("username");
        input.ValueString.Set("damphat");
        input.ValueString.Add(value => Toast.Info(value));

        text.Title.SetSrc(input.ValueString);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
