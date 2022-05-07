#region

using MetaUI;
using UnityEngine;

#endregion

public class Finder : MonoBehaviour
{
    private void Start()
    {
        this.Get("**/queryString").Changed(qs =>
        {
            try
            {
                var go = this.Get(qs);
                this.Toast(go.gameObject);
            }
            catch (MetaUIException e)
            {
                
            }
        });
    }
}