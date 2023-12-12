using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class SaveTools
{
    /// <summary>
    /// Facilite l'accessiblité à un dictionnaire imbriqué du dictionnaire de sauvegarde.
    /// </summary>
    /// <param name="key">La clé du dictionnaire de sauvegarde.</param>
    /// <typeparam name="T">Le type de valeur du dictionnaire imbriqué.</typeparam>
    /// <returns>Un dictionnaire imbriqué dans le dictionnaire de sauvegarde.</returns>
    public static Dictionary<string, T> Get_NestedDictionary<T>(string key)
    {
        if (SaveManager.Data.TryGetValue(key, out var nestedDict))
        {
            return nestedDict switch
            {
                Dictionary<string, T> parsedDict => parsedDict,
                JObject jObject =>
                    //JsonConvert ne désérialise pas les objets "valeurs" d'un objet désérialisé
                    JsonConvert.DeserializeObject<Dictionary<string, T>>(jObject.ToString()),
                _ => throw new Exception("The list is another type than a Dictionary or JSON Object.")
            };
        }

        return null;
    }

    /// <summary>
    /// Sauvegarder une valeur dans un dictionnaire imbriqué du dictionnaire de sauvegarde.
    /// </summary>
    /// <param name="mainKey">La clé du dictionnaire de sauvegarde.</param>
    /// <param name="nestedKey">La clé du dictionnaire imbriqué.</param>
    /// <param name="value">La valeur à sauvegarder.</param>
    /// <typeparam name="T">Le type de valeur du dictionnaire imbriqué.</typeparam>
    public static void Save_NestedValue<T>(string mainKey, string nestedKey, T value)
    {
        var nestedDict = Get_NestedDictionary<T>(mainKey);
        if (!nestedDict.ContainsKey(nestedKey.ToLower()))
            Debug.LogException(new Exception(
                $"The key [{nestedKey}] doesn't exist in the dictionary of the key [{mainKey}].\nIt may not have been initialized."));
        //spécifier toLower ou sinon on va créer une duplication.
        //Voir le JsonConverter de la méthode Save() 
        nestedDict[nestedKey.ToLower()] = value;
        //
        SaveManager.Data[mainKey] = nestedDict;
        SaveManager.Save();
    }
}