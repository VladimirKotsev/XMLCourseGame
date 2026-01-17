using System.Collections;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int currentLevel = 1;
    private UIManager uiManager;
    private InventoryToggle inventoryToggle;
    private ScoreManager scoreManager;

    public GameData gameData;

    //Prefabs
    public GameObject playerPrefab;

    public GameObject linearBalloonPrefab;
    public GameObject curvedBalloonPrefab;
    public GameObject expertBalloonPrefab;
    public TMP_InputField nameInputField;

    void Start()
    {
        this.uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        this.scoreManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<ScoreManager>();
        this.inventoryToggle = GameObject.FindGameObjectWithTag("UIManager").GetComponent<InventoryToggle>();
        this.LoadXml();
        this.LoadCurrentLevel();
        this.scoreManager.UpdateLevel(this.currentLevel, this.gameData.Levels.LevelList.Count());
        this.inventoryToggle.ToggleInventory();

        StartCoroutine(FocusInput(this.nameInputField));
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
        this.scoreManager.UpdateLevel(this.currentLevel, this.gameData.Levels.LevelList.Count());
    }

    private void GameOver()
    {
        this.uiManager.State = UIState.GameOver;
        this.inventoryToggle!.ToggleInventory();
        Time.timeScale = 0f;
    }

    private void LoadCurrentLevel()
    {
        foreach (var balloon in CurrentGameLevel.Balloons.BalloonList)
        {
            CreateBalloonInstance(balloon);
        }
    }

    private void CreateBalloonInstance(Balloon balloon)
    {
        GameObject instance;

        switch (balloon.Trajectory.Type)
        {
            case TrajectoryType.linear:
                instance = Instantiate(linearBalloonPrefab);
                SetupMovement<LinearTargetMovement>(instance, balloon);
                break;

            case TrajectoryType.curved:
                instance = Instantiate(curvedBalloonPrefab);
                SetupMovement<CurvedTargetMovement>(instance, balloon);
                break;

            default:
                instance = Instantiate(expertBalloonPrefab);
                SetupMovement<ExpertTargetMovement>(instance, balloon);
                break;
        }

        SetupTarget(instance, balloon);
    }

    private void SetupMovement<T>(GameObject instance, Balloon balloon)
    where T : TargetMovement
    {
        var movement = instance.GetComponent<T>();

        TargetMovement m = movement;
        m.startPosition = new Vector3(balloon.Trajectory.StartX, 10, balloon.Trajectory.StartY);
        m.endPosition = new Vector3(balloon.Trajectory.EndX, 10, balloon.Trajectory.EndY);
        m.moveSpeed = balloon.Trajectory.Speed;
    }

    private void SetupTarget(GameObject instance, Balloon balloon)
    {
        var target = instance.GetComponent<Target>();
        target.Health = balloon.Hitpoints;
        target.Item = new InventoryItem { Name = balloon.Item, Description = balloon.Description, Icon = balloon.Icon };
    }

    public void CheckForLevelCompletion(int currentBalloonCount) 
    {
        var balloonsCountToLevel = this.gameData.Levels.LevelList
            .Where(level => level.Id <= this.CurrentGameLevel.Id)
            .Select(level => level.Balloons.BalloonList.Count())
            .Sum();

        if (currentBalloonCount == balloonsCountToLevel) 
        {
            this.NextLevel();
        }
    }

    public void StartGame() 
    {
        this.uiManager.State = UIState.Crosshair;
        this.inventoryToggle.ToggleInventory();
        string playerName = nameInputField.text;
        // TODO: Same name 
    }

    public void ReloadGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        // TODO: Serialize game
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

    private IEnumerator FocusInput(TMP_InputField inputField)
    {
        yield return null;
        inputField.Select();
        inputField.ActivateInputField();
    }
}
