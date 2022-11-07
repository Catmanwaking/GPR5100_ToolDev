//Author: Dominik Dohmeier
using Newtonsoft.Json;
using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Tiles
{
    public class FileLoader : MonoBehaviour
    {
        [SerializeField] private MapDataSO mapData;
        [SerializeField] private GameObject ScrollContent;

        [SerializeField] private ButtonScript buttonScriptGO;

        [SerializeField] private TMP_Text errorText;

        private string path;

        private void Start()
        {
#if UNITY_EDITOR
            Debug.Log(AppDomain.CurrentDomain.BaseDirectory);
            string[] levels = { "DebugMap" };
#else
            path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Levels");
            string[] levels = Directory.GetFiles(path);
#endif
            for (int i = 0; i < levels.Length; i++)
            {
                ButtonScript button = Instantiate(buttonScriptGO, ScrollContent.transform, false);
                button.SetText(Path.GetFileNameWithoutExtension(levels[i]), LoadMap);
            }
        }

        public void LoadMap(string levelName)
        {
#if UNITY_EDITOR
            var jsonFile = Resources.Load(levelName, typeof(TextAsset)) as TextAsset;
            if (jsonFile == null)
                throw new ApplicationException($"{levelName} could not be loaded");
            string jsonString = jsonFile.text;
#else
            string jsonString = File.ReadAllText(Path.Combine(path, $"{levelName}.txt"));
#endif
            try
            {
                mapData.MapTiles = (TileContent[,])JsonConvert.DeserializeObject(jsonString, typeof(TileContent[,]));
                SceneManager.LoadScene(1);
            }
            catch (JsonReaderException)
            {
                errorText.text = $"Error: \"{levelName}\" is not a valid JSON file";
            }
            catch (JsonSerializationException)
            {
                errorText.text = $"Error: \"{levelName}\" is not a valid TDS file";
            }
        }

        public void ExitApplication()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}