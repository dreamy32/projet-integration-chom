using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

//
public class QuickSaveManager : MonoBehaviour
{
    public delegate void LoadAction();
    public static event LoadAction OnLoad;
    public delegate void SaveAction();
    public static event SaveAction OnSave;
    public static string previousSceneName;

    //Event
    // public void Load(InputAction.CallbackContext ctx)
    // {
    //     if (ctx.performed)
    //         OnLoad?.Invoke();
    // }

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public static void Save() => OnSave?.Invoke();

    public static void Load()
    {
        Debug.Log("Loaded !");
        OnLoad?.Invoke();

        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Settings" && scene.name != "Loading")
        {
            previousSceneName = scene.name;
            Debug.Log(previousSceneName);
        }
            
    }

}