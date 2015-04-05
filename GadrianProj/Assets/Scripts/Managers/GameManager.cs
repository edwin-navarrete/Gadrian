using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private CharacterManager characterManager;
    [SerializeField]
    private MenuManager menuManager;
    [SerializeField]
    private GridManager gridManager;
    [SerializeField]
    private PersonalityManager personalitymanager;
    [SerializeField]
    private LevelGenerator levelGenerator;

    public void Awake ()
    {
        characterManager = CharacterManager.Instance;
        menuManager = MenuManager.Instance;
        gridManager = GridManager.Instance;
        personalitymanager = PersonalityManager.Instance;
        levelGenerator = LevelGenerator.Instance;
    }

    public void Start ()
    {
        //levelGenerator.Generatelevel( menuManager.LevelSelected );
        levelGenerator.Generatelevel();
        personalitymanager.InitializePersonalityModel();

    }
}
