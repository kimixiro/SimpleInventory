using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CSVGenerator 
{
   [MenuItem("CSV/Generate")]
   public static void GenerateCards()
   {
      if (Selection.activeObject == null) return;

      string path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(),
         AssetDatabase.GetAssetPath(Selection.activeObject));

      if (!IsCSVFile(path))
      {
         Debug.LogError("Not a CSV file");
         return;
      }

      List<Dictionary<string, object>> rawCSVData = CSVReader.Read(path);

      if (rawCSVData.Count > 0)
      {
         bool confirmed = EditorUtility.DisplayDialog("Mass Generate Cart",
            $"Are you sure you wnt to create {rawCSVData.Count} entries?","Yes","No");
         
         if (confirmed) PerformGeneration(rawCSVData);
         
      }
      else
      {
         Debug.LogError("No entries read from CSV");
         return;
      }
   }
   
   private  static void PerformGeneration(List<Dictionary<string,object>> csvData){

      for (int i = 1; i < csvData.Count; i++)
      {
         Dictionary<string, object> potentialItem = csvData[i];
         
         Debug.Log($"{potentialItem["Name"]}");

         
         string name, sprite;
         
         
         
         name = potentialItem["Name"].ToString();
         sprite = potentialItem["Sprite"].ToString();
         

         CreateScriptableObjectItem(name, sprite);

      }
   }

   public static void CreateScriptableObjectItem(string name, string sprite )
   {
      AssetItem newItem = ScriptableObject.CreateInstance<AssetItem>();

      newItem.Name = name;
      newItem.UIIcon = Resources.Load<Sprite>(sprite); 
      
      AssetDatabase.CreateAsset(newItem,$"Assets/Data/Item/{name.Trim()}.asset");
      AssetDatabase.SaveAssets();
      
   }

   private static bool IsCSVFile(string fullpath)
   {
      return fullpath.ToLower().EndsWith(".csv");
   }
}
