using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Person : MonoBehaviour
{
    private PersonManager manager;
    private string title, dialogue; //unity was getting mad that i had a variable named name
    [SerializeField] private TextMeshProUGUI dialogueText;
    private void Start()
    {
        StartCoroutine(nameof(LateStartCoroutine));
    }
    IEnumerator LateStartCoroutine()
    {
        yield return null;
        LateStart();
    }
    void LateStart()
    {
        manager = PersonManager.Instance;
        manager.People.Add(this.gameObject);

        title = manager.RandomName();
        dialogue = manager.RandomDialogue();

        Debug.Log("asdfjlasdf");

        //remove later
        SaySomething();
    }

    void Update()
    {

    }

    public void SaySomething() => StartCoroutine(nameof(SaySomethingCoroutine));
    private IEnumerator SaySomethingCoroutine()
    {
        var d = "";
        var x = 0;
        while (d.Length < dialogue.Length)
        {
            x++;
            d = dialogue.Substring(0, x);
            dialogueText.text = d;
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }
}
