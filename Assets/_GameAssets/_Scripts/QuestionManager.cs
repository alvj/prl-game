using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour {
    [SerializeField] Text questionText;

    public void DisplayQuestion(Question question) {
        questionText.text = question.GetQuestion();
    }
}
