using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionTrigger : MonoBehaviour
{
    [SerializeField] Question question;
    [SerializeField] QuestionManager qm;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            qm.DisplayQuestion(question);
        }
    }
}