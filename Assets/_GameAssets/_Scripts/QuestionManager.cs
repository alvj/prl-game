using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
    {
    [SerializeField] GameObject questionObject;
    [SerializeField] GameObject explanationObject;
    [SerializeField] Text questionText;
    [SerializeField] Text explanationText;
    [SerializeField] GameObject[] answerButtons = new GameObject[4];

    Player player;
    Question question;
    bool guessedCorrect;

    private void Awake() {
        player = GameObject.FindObjectOfType<Player>();
    }

    public void GetQuestion(Question questionFromTrigger) {
        question = questionFromTrigger;
        player.SetCanMove(false);
        DisplayQuestion();
    }

    public void DisplayQuestion() {
        questionObject.SetActive(true);

        questionText.text = question.GetQuestion();
        string[] answers = question.GetAnswers();
        explanationText.text = question.GetExplanation();

        for (int i = 0; i < answerButtons.Length; i++) {
            answerButtons[i].GetComponentInChildren<Text>().text = answers[i];
        }
    }

    public void CheckAnswer(int answer) {
        if (answer == question.GetCorrectAnswer()) {
            ChangeButtonColor(answerButtons[answer], Color.green);
            guessedCorrect = true;
            Invoke("CorrectAnswer", 1.5f);
        } else if (answer != question.GetCorrectAnswer() && !guessedCorrect) {
            ChangeButtonColor(answerButtons[answer], Color.red);
        }
    }

    void ChangeButtonColor(GameObject button, Color color) {
        button.GetComponent<Image>().color = color;
    }

    void CorrectAnswer() {
        questionObject.SetActive(false);
        explanationObject.SetActive(true);
    }

    public void EndQuestion() {
        explanationObject.SetActive(false);
        player.SetCanMove(true);
    }
}