using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Animations;
using DG.Tweening;

public class Person : MonoBehaviour
{
    private PersonManager manager;
    private string title, dialogue; //unity was getting mad that i had a variable named name
    private int skinColor;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueBox;
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
        var bodyparts = manager.RandomPerson();
        var renderer = transform.GetChild(0);
        for (int i = 0; i < renderer.childCount; i++)
            renderer.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = bodyparts[i];
        //remove later
        //SaySomething();
    }

    void Update()
    {

    }

    public void SaySomething() => StartCoroutine(nameof(SaySomethingCoroutine));
    private IEnumerator SaySomethingCoroutine()
    {
        dialogueBox.SetActive(true);
        dialogueBox.transform.localScale = Vector3.zero;
        dialogueBox.transform.DOScale(Vector3.one, 0.2f)/*.OnComplete(() => dialogueBox.transform.DOPunchScale(Vector3.one * 1.25f, 0.1f))*/;
        yield return new WaitForSeconds(0.3f);
        var d = "";
        var x = 0;
        while (d.Length < dialogue.Length)
        {
            x++;
            d = dialogue.Substring(0, x);
            dialogueText.text = d.ToLower();
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        dialogueText.text = "";

        dialogueBox.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => dialogueBox.SetActive(false));
    }
}
