using System.IO;
using UnityEngine;

public static class ScoreFileManager
{
    private static string fileName = "GameResults.txt";

    public static void SaveResult(string playerName, int score, int level)
    {
        string currentDate = System.DateTime.Now.ToString("dd.MM.yyyy | HH:mm");
        string lineToWrite = $"{currentDate}\nPlayer Name: {playerName}\nScore: {score}\n --------------------------------------- ";

        string filePath = Path.Combine(Application.streamingAssetsPath, "GameResults.txt");

        try
        {
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine(lineToWrite);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Could not save file: {e.Message}");
        }
    }
}