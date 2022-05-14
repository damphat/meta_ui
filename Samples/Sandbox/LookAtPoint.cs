using UnityEngine;

[ExecuteInEditMode]
public class LookAtPoint : MonoBehaviour
{
    public Vector3 lookAtPoint = Vector3.zero;

    private void Update()
    {
        transform.LookAt(lookAtPoint);
    }
}