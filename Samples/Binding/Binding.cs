using MetaUI;
using UnityEngine;

public class Binding : MonoBehaviour
{
    void Start()
    {
        var enable = true;
        var title = "initial title";
        var icon = Sprite.Create(Texture2D.redTexture, Rect.MinMaxRect(0,0,1,1), Vector2.zero);
        var stringValue = "initial stringValue";
        var boolValue = false;
        var intValue = 5;
        var floatValue = 0.5f;
        
        var dialog = this.Get("dialog");
        var cmds = this.Get("commands");

        cmds.Add().Text("Text(value)").Clicked(() => dialog.EachChild(c =>
        {
            c.Text(title);
        }));
        cmds.Add().Text("Text(provider)").Clicked(() => dialog.EachChild(c => c.Text(() => title)));

        cmds.Add().Text("Value()").Clicked(() => dialog.EachChild(c => c.Value(stringValue)));
        cmds.Add().Text("Value(provider)").Clicked(() => dialog.EachChild(c => c.Value(() => c.Binder.Kind)));

        cmds.Add().Text("Enable(value)").Clicked(() => dialog.EachChild(c => c.Enable(enable)));
        cmds.Add().Text("Enable(provider)").Clicked(() => dialog.EachChild(c => c.Enable(() => enable)));

        cmds.Add().Text("Clicked").Clicked(() => dialog.EachChild(c => c.Clicked(() => this.Toast(c.gameObject.name))));

        cmds.Add().Text("SetXXX(value)").Clicked(() => dialog.EachChild(c =>
        {
            var t = c.Title;
            if (t == null)
            {
                Debug.Log("null");
            }
            t.Set("xx");

            c.Title.Set(title);
            c.Background.Set(icon);
            c.ValueBool.Set(boolValue);
            c.ValueInt.Set(intValue);
            c.ValueFloat.Set(floatValue);
            c.ValueString.Set(stringValue);
        }));

        cmds.Add().Text("SetXXX(() => value)").Clicked(() => dialog.EachChild(c =>
        {

            c.Title.Set(() => title);
            c.Background.Set(() => icon);
            c.ValueBool.Set(() => boolValue);
            c.ValueInt.Set(() => intValue);
            c.ValueFloat.Set(() => floatValue);
            c.ValueString.Set(() => stringValue);
        }));
        
        cmds.Add().Text("GetXXX()").Clicked(() => dialog.EachChild(c =>
        {
            c.gameObject.name = "";
            c.gameObject.name += $"t:{c.Title.Get()},";
            c.gameObject.name += $"b:{c.ValueBool.Get()},";
            c.gameObject.name += $"i:{c.ValueInt.Get()},";
            c.gameObject.name += $"f:{c.ValueFloat.Get()},";
            c.gameObject.name += $"s:{c.ValueString.Get()},";
        }));

        cmds.Add().Text("GetXXX((value) => {})").Clicked(() => dialog.EachChild(c =>
        {
            c.ValueBool.Get(x => this.Toast($"Bool:{x}"));
            c.ValueInt.Get(x => this.Toast($"Int:{x}"));
            c.ValueFloat.Get(x => this.Toast($"Float:{x}"));
            c.ValueString.Get(x => this.Toast($"String:{x}"));
        }));

        cmds.Add().Text("inc").Clicked(() =>
        {
            enable = !enable;
            intValue = (intValue + 1) % 10;
            boolValue = !boolValue;
            floatValue = intValue / 10f;
            stringValue = $"string {intValue}";
            title = $"title {intValue}";
            icon = Sprite.Create(boolValue ? Texture2D.redTexture: Texture2D.grayTexture, Rect.MinMaxRect(0, 0, 1, 1), Vector2.zero);
        });

        ;
    }
}
