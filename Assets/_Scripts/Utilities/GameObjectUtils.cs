using UnityEngine;

public static class GameObjectUtils
{
    public static void Enable(this GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
    public static void Disable(this GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
