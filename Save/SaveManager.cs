using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    /// <summary>
    /// Dictionnaire de sauvegarde
    /// </summary>
    public static Dictionary<string, dynamic> Data;

    private static string FilePath { get; set; }

    [SerializeField, Tooltip("Must include file extension (.json, .txt)")]
    private string fileName = "save.json";

    //Initialize saveFile
    [Header("Value Initialization")] [SerializeField, Tooltip("The scene names are case insensitives.")]
    public string[] levelNames;



    private void Awake()
    {
        //En spécifiant que c'est du camelCase, on sauve du temps lors de la désérialization.
        JsonSerializerSettingsManager.Settings = JsonSerializerSettingsManager.GetCamelCaseSettings();
        FilePath = $"{Application.persistentDataPath}/{fileName}";
        //
        if (File.Exists(FilePath))
        {
            var file = File.ReadAllText(FilePath);
            if (string.IsNullOrEmpty(file))
            {
                Create();
                return;
            }

            Data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(file);
        }
        else
            Create();
    }

    private void Initialize()
    {
        var length = levelNames.Length;
        //
        var levelDictionary = new Dictionary<string, bool>(length);
        var personalBDict = new Dictionary<string, float>(length);
        var collectiblesDict = new Dictionary<string, bool>(length);
        foreach (var s in levelNames)
        {
            var loweredName = s.ToLower();
            levelDictionary.Add(loweredName, false);
            collectiblesDict.Add(loweredName, false);
            personalBDict.Add(loweredName, 0);
        }

        Data = new Dictionary<string, dynamic>(3)
        {
            { SavableData.LEVEL, levelDictionary },
            { SavableData.LVL_PB, personalBDict },
            { SavableData.COLLECTIBLES, collectiblesDict }

            //Ajouter une constante dans SavableData, add here
            //Oublie pas de changer la capacité du constructeur
        };
    }

    private void Create()
    {
        //
        var streamWriter = File.CreateText(FilePath);
        streamWriter.Close();
        //
        Initialize();
        //
        Save();
    }

    private void Start()
    {
        // SaveTools.Save_NestedValue(SavableData.LVL_PB, "Stage 214124", 423423.6346340f);
        // SaveTools.Save_NestedValue(SavableData.COLLECTIBLES, "Cake", true);
    }

    /// <summary>
    /// Sérialise les données de sauvegarde dans le fichier.
    /// </summary>
    public static void Save()
    {
        var json = JsonConvert.SerializeObject(Data, JsonSerializerSettingsManager.Settings);
        File.WriteAllText(FilePath, json);
    }

    /// <summary>
    /// Efface le fichier de sauvegarde.
    /// </summary>
    public static void Erase()
    {
        Debug.Log("Erased...");
        File.Delete(FilePath);
        Data.Clear();
    }
}
