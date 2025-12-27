using System.IO;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int currentLevel = 1;
    private UIManager uiManager;

    public GameData gameData;

    //Prefabs
    public GameObject playerPrefab;

    public GameObject linearBalloonPrefab;
    public GameObject curvedBalloonPrefab;
    public GameObject expertBalloonPrefab;

    void Start()
    {
        this.uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        this.LoadXml();
        this.LoadCurrentLevel();
    }

    public Level CurrentGameLevel => this.gameData.Levels.LevelList.Where(level => level.Id == currentLevel).FirstOrDefault();

    public void NextLevel()
    {
        Debug.Log("New level");
        if (this.gameData.Levels.LevelList.Count == this.currentLevel)
        {
            this.GameOver();
            return;
        }

        this.currentLevel++;
        this.LoadCurrentLevel();
    }

    private void GameOver()
    {
        this.uiManager.State = UIState.GameOver;
        Time.timeScale = 0f;
    }

    private void LoadCurrentLevel()
    {
        MovePlayerToBeginning(10, 10, 10);
        foreach (var balloon in this.CurrentGameLevel.Balloons.BalloonList)
        {
            if (balloon.Trajectory.Type == TrajectoryType.linear)
            {
                CreateBalloon(this.linearBalloonPrefab, balloon);
            }
            else if (balloon.Trajectory.Type == TrajectoryType.curved)
            {
                CreateBalloon(this.curvedBalloonPrefab, balloon);
            }
            else 
            {
                CreateBalloon(this.expertBalloonPrefab, balloon);
            }
        }
    }

    private void CreateBalloon(GameObject linearBalloonPrefab, Balloon balloon)
    {
        // TODO
        // Instantiate at start point
        // Set the item in the reference.
        // Set the hits in the reference
        Instantiate(this.linearBalloonPrefab);
    }

    private void MovePlayerToBeginning(int x, int y, int z)
    {
        // TODO
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
        string xmlPath = Path.Combine(Application.streamingAssetsPath, "game.xml");
        string xsdPath = Path.Combine(Application.streamingAssetsPath, "game.xsd");

        if (!XmlValidator.Validate(xmlPath, xsdPath))
        {
            Debug.LogError("XML is INVALID. Game will not load.");
            return;
        }

        this.gameData = XmlLoader.LoadGame(xmlPath);
        Debug.Log("Game loaded successfully!");
    }
}
