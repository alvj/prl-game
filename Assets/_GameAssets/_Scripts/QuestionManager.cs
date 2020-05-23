using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour {
    [SerializeField] Text questionText;
    [SerializeField] Text[] answerTexts = new Text[4];

    public void DisplayQuestion(Question question) {
        questionText.text = question.GetQuestion();
        for (int i = 0; i < answerTexts.Length; i++) {
            answerTexts[i].text = question.GetAnswers()[i];
        }
    }
}
