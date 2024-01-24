using UnityEngine;

public static class GameTools
{
    public static bool CompareLayers(LayerMask layerMask, int layer)
    {
        return (layerMask & 1 << layer) == 1 << layer;
    }

    public static string ConvertFloatToTime(float elapsedTime)
    {
        var minutes = Mathf.Floor(elapsedTime / 60);
        var seconds = Mathf.RoundToInt(elapsedTime % 60);
       // 
        return $"{minutes:00}:{(seconds == 60 ? 00 : seconds):00}";
    }

    public static UnityEngine.Resolution[] GetResolutions()
    {
        return Screen.resolutions;
    }
}
