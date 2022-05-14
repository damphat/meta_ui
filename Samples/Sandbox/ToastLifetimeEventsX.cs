using MetaUI;
using UnityEngine;


public class ToastLifetimeEventsX : MonoBehaviour
{
    public int count = 14;
    private void Reset()
    {
        this.Toast("Reset");
    }

    private void Awake()
    {
        this.Toast("Awake");
    }

    private void OnEnable()
    {
        this.Toast("OnEnable");
    }

    // Start is called before the first frame update
    void Start()
    {
        
            this.Toast("Start");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (count++ < 5)
        {
            this.Toast("Update");
        }
    }

    private void OnDisable()
    {

        this.Toast("OnDisable");
    }

    private void OnDestroy()
    {
        this.Toast("OnDestroy");
    }
}
