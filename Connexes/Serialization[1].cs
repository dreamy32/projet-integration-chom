using System;
using UnityEngine;

[Serializable]
public class SerializedTransform
{
    //Source: https://forum.unity.com/threads/how-to-save-a-transform.495981/#post-4266253
    public Vector3 _position;
    public Quaternion _rotation;
    public Vector3 _scale;

    public SerializedTransform(Transform transform)
    {
        _position = transform.position;
        _rotation = transform.rotation;
        _scale = transform.localScale;
    }
}