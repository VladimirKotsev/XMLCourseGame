using System.IO;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int currentLevel = 1;
    public GameData gameData;

    void Start()
    {
        this.LoadXml();
        this.LoadCurrentLevel();
    }

    public Level CurrentGameLevel => this.gameData.Levels.LevelList.Where(level => level.Id == currentLevel).FirstOrDefault();

    public void NextLevel()
    {
        Debug.Log("Noviq level");
        if (this.gameData.Levels.LevelList.Count == this.currentLevel)
        {
            // TODO: Game over
            return;
        }

        this.currentLevel++;
        this.LoadCurrentLevel();
    }

    private void LoadCurrentLevel()
    {
        // Player at zero
        // Instantiate the baloons -> for every ballon create instance with item inside

    }

    public void CheckForLevelCompletion(int currentBalloonCount) 
    {
        if (currentBalloonCount == this.CurrentGameLevel.Balloons.BalloonList.Count) 
        {
            this.NextLevel();
        }
    }

    private void LoadXml() 
    {
        string path = Path.Combine(Application.streamingAssetsPath, "game.xml");

        this.gameData = XmlLoader.LoadGame(path);
    }
}
