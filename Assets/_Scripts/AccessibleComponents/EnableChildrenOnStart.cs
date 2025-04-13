using UnityEngine;

public class EnableChildrenOnStart : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0, len = transform.childCount; i < len; ++i)
        {
            transform.GetChild(i).gameObject.Enable();
        }
    }
}
