using MetaUI;
using UnityEngine;

public class Binding : MonoBehaviour
{
    private void Start()
    {
        var enable = this.Value(true);
        var title = this.Value("initial title");
        var icon = this.Value(Sprite.Create(Texture2D.redTexture, Rect.MinMaxRect(0, 0, 1, 1), Vector2.zero));
        var stringValue = this.Value("initial stringValue");
        var boolValue = this.Value(false);
        var intValue = this.Value(5);
        var floatValue = this.Value(0.5f);

        var dialog = this.Get("dialog");
        var cmds = this.Get("commands");

        cmds.Add().SetTitle("Text(value)").Clicked(() => dialog.EachChild(c => { c.Title.Set(title.Get()); }));
        cmds.Add().SetTitle("Text(provider)").Clicked(() => dialog.EachChild(c => c.Title.SetSrc(title)));

        cmds.Add().SetTitle("Value()").Clicked(() => dialog.EachChild(c => c.ValueString.Set(stringValue.Get())));
        cmds.Add().SetTitle("Value(provider)").Clicked(() => dialog.EachChild(c => c.ValueString.SetSrc(stringValue)));

        cmds.Add().SetTitle("Enable(value)").Clicked(() => dialog.EachChild(c => c.Interactable.Set(enable.Get())));
        cmds.Add().SetTitle("Enable(provider)").Clicked(() => dialog.EachChild(c => c.Interactable.SetSrc(enable)));

        cmds.Add().SetTitle("Clicked").Clicked(() => dialog.EachChild(c => c.Clicked(() => this.Toast(c.gameObject.name))));

        cmds.Add().SetTitle("SetXXX(value)").Clicked(() => dialog.EachChild(c =>
        {
            c.Title.Set(title.Get());
            c.Background.Set(icon.Get());
            c.ValueBool.Set(boolValue.Get());
            c.ValueInt.Set(intValue.Get());
            c.ValueFloat.Set(floatValue.Get());
            c.ValueString.Set(stringValue.Get());
        }));

        cmds.Add().SetTitle("SetXXX(() => value)").Clicked(() => dialog.EachChild(c =>
        {
            c.Title.SetSrc(title);
            c.Background.SetSrc(icon);
            c.ValueBool.SetSrc(boolValue);
            c.ValueInt.SetSrc(intValue);
            c.ValueFloat.SetSrc(floatValue);
            c.ValueString.SetSrc(stringValue);
        }));

        cmds.Add().SetTitle("GetXXX()").Clicked(() => dialog.EachChild(c =>
        {
            c.gameObject.name = "";
            c.gameObject.name += $"t:{c.Title.Get()},";
            c.gameObject.name += $"b:{c.ValueBool.Get()},";
            c.gameObject.name += $"i:{c.ValueInt.Get()},";
            c.gameObject.name += $"f:{c.ValueFloat.Get()},";
            c.gameObject.name += $"s:{c.ValueString.Get()},";
        }));

        cmds.Add().SetTitle("GetXXX((value) => {})").Clicked(() => dialog.EachChild(c =>
        {
            c.ValueBool.Add(x => this.Toast($"Bool:{x}"));
            c.ValueInt.Add(x => this.Toast($"Int:{x}"));
            c.ValueFloat.Add(x => this.Toast($"Float:{x}"));
            c.ValueString.Add(x => this.Toast($"String:{x}"));
        }));

        cmds.Add().SetTitle("inc").Clicked(() =>
        {
            enable.Set(!enable.Get());
            intValue.Set((intValue.Get() + 1) % 10);
            boolValue.Set(!boolValue.Get());
            floatValue.Set(intValue.Get() / 10f);
            stringValue.Set($"string {intValue.Get()}");
            title.Set($"title {intValue.Get()}");
            icon.Set(Sprite.Create(boolValue.Get() ? Texture2D.redTexture : Texture2D.grayTexture,
                Rect.MinMaxRect(0, 0, 1, 1), Vector2.zero));
        });

        ;
    }
}