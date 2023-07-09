using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PersonManager : MonoBehaviour
{
    #region singleton
    private static PersonManager _instance;

    public static PersonManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }
    #endregion
    [SerializeField] private GameObject personPrefab;
    [HideInInspector] public GameObject PersonPrefab { get { return personPrefab; } }
    private List<GameObject> people = new();
    [HideInInspector] public List<GameObject> People { get { return people; } }

    [SerializeField] private string dialogues;
    [SerializeField] private string names;
    private string[] dialoguesList;
    private string[] namesList;
    [SerializeField] private List<Sprite> bodies = new();
    [SerializeField] private List<SpriteList> heads = new();
    [SerializeField] private List<SpriteList> noses = new();
    [SerializeField] private List<Sprite> eyes = new();
    [SerializeField] private List<SpriteList> mouthes = new();
    [SerializeField] private List<Sprite> hairs = new();
    [SerializeField] private List<SpriteList> hands = new();



    void Start()
    {
        dialoguesList = dialogues.Split("/");
        namesList = names.Split("/");
    }

    void Update()
    {
        
    }

    public string RandomName() => namesList[Random.Range(0, namesList.Length - 1)];
    public string RandomDialogue() => dialoguesList[Random.Range(0, dialoguesList.Length - 1)];
    public List<Sprite> RandomPerson()
    {
        var skinColor = Random.Range(0, 5);
        //this is very bad code should fix soon
        var returnList = new List<Sprite>
        {
            bodies[Random.Range(0, bodies.Count)],
            heads[skinColor].Sprites[Random.Range(0, heads[skinColor].Sprites.Count)],
            noses[skinColor].Sprites[Random.Range(0, noses[skinColor].Sprites.Count)],
            eyes[Random.Range(0, eyes.Count)],
            mouthes[skinColor].Sprites[Random.Range(0, mouthes[skinColor].Sprites.Count)],
            hairs[Random.Range(0, hairs.Count)],
            hands[skinColor].Sprites[Random.Range(0, hands[skinColor].Sprites.Count)]
        };

        return returnList;
    }
}

[System.Serializable]
struct SpriteList
{
    public List<Sprite> Sprites;
}
