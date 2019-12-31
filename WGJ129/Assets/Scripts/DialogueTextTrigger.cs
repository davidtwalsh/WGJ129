using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTextTrigger : MonoBehaviour
{
    public string dialogue;
    public GameObject DialogueTextObj;
    public TextMeshPro dialogueTMP;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DialogueTextObj.SetActive(true);
            dialogueTMP.text = dialogue;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DialogueTextObj.SetActive(false);
        }
    }
}
