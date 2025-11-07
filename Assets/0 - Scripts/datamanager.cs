using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public class datamanager : MonoBehaviour
{
    private List<string> gameDataFirstHalf = new List<string>();
    private List<string> gameDataSecondHalf = new List<string>();

    private int roundSuccessHitCounter = 0;
    private List<float> roundRTs = new List<float>();

    private string filePath;

    private string id;

    private string dataFilePath;
    private string dataHeader = "id, gameType, round, trial, timestamp, time, mouseX, mouseY, position, effectDelay, startRT, endRT, RT, status;";


    [DllImport("__Internal")]
    private static extern void receiveGameData(string data);
    [DllImport("__Internal")]
    private static extern void receiveMidGameData(string data);
    [DllImport("__Internal")]
    private static extern void gameEnd();

    private void Start()
    {
        // generate random 4-digit id
        // 1st digit: 1-9, rest: 0-9
        id = UnityEngine.Random.Range(1, 10).ToString() + UnityEngine.Random.Range(0, 10).ToString() + UnityEngine.Random.Range(0, 10).ToString() + UnityEngine.Random.Range(0, 10).ToString();
        string projectRoot = Directory.GetParent(Application.dataPath).FullName;
        string folderPath = Path.Combine(projectRoot, "Data");
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        dataFilePath = Path.Combine(folderPath, $"{id}_trialData.csv");

        // Header schreiben, falls Datei noch nicht existiert
        if (!File.Exists(dataFilePath))
        {
            File.WriteAllText(dataFilePath, dataHeader + "\n");
        }

        Debug.Log("Trial CSV Pfad: " + dataFilePath);
    }

    public void AddTrialToData(int round, int trial, float mouse_x, float mouse_y, string position, float effect_delay, float start_RT, float end_RT, float RT, int status)
    {
        string row = $"{id}, RLD, {round}, {trial}, {DateTime.Now:HH:mm:ss.fff}, {Time.time}, {mouse_x}, {mouse_y}, {position}, {effect_delay}, {start_RT}, {end_RT}, {RT}, {status}";
        if (round <= 8)
        {
            // first half of the game
            gameDataFirstHalf.Add(row);
        }
        else
        {
            // second half of the game
            gameDataSecondHalf.Add(row);
        }

        // stat stuff for UI
        if (status == 1) // if successful hit
        {
            // roundSuccessHits.Add(1);
            roundSuccessHitCounter++;
        }
        roundRTs.Add(RT);
        try
        {
            File.AppendAllText(dataFilePath, row + "\n");
        }
        catch (Exception e)
        {
            Debug.LogError("Fehler beim Speichern der Trial-Daten: " + e.Message);
        }

    }

    // called with game end event
    public void SendData()
    {
        string allRows = string.Join(";", gameDataFirstHalf);

        StartMidGameProcedure(allRows);

        gameDataFirstHalf.Clear(); // clear data after sending it
    }

    public void SendSecondData()
    {
        string allRows = string.Join(";", gameDataSecondHalf);

        StartGameEndProcedure(allRows);
    }


    public string GetID()
    {
        return id;
    }

    public int GetRoundSuccessHits()
    {
        return roundSuccessHitCounter;
    }

    public List<float> GetRoundRTs()
    {
        return roundRTs;
    }

    public void ResetRoundStats()
    {
        roundSuccessHitCounter = 0;
        roundRTs.Clear();
    }


// For sending data to JavaScript
    public void StartGameEndProcedure(string rowData)
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        receiveGameData(rowData);
        gameEnd();
        #endif
    }

    public void StartMidGameProcedure(string rowData)
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        receiveMidGameData(rowData);
        #endif
    }

}
