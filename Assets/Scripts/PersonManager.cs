using System.Collections;
using System.Collections.Generic;
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
}
