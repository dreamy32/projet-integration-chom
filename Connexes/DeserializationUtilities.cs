using UnityEngine;

public static class DeserializationUtilities
{
    public static void DeserializeTransform(SerializedTransform serializedTransform, Transform transform)
    {
        transform.position = serializedTransform._position;
        transform.rotation = serializedTransform._rotation;
        // transform.localScale = serializedTransform._scale;
    }
}
