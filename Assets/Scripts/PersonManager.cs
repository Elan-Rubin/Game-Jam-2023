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
    private List<string> dialoguesList = new();
    [HideInInspector] public List<string> DialoguesList { get { return dialoguesList; } }
    private List<string> namesList = new();
    [HideInInspector] public List<string> NamesList { get { return namesList; } }


    void Start()
    {
        var d = dialogues.Split('/');
        foreach (var dialogue in d) dialoguesList.Add(dialogue);
        var n = names.Split('/');
        foreach (var title in n) namesList.Add(title);
    }

    void Update()
    {
        
    }
}
