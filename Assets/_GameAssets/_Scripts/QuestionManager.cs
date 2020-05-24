using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] GameObject questionObject;
    [SerializeField] GameObject explanationObject;
    [SerializeField] Text questionText;
    [SerializeField] Text explanationText;
    [SerializeField] GameObject[] answerButtons = new GameObject[4];

    [Space]

    [Header("Audio")]
    [SerializeField] AudioClip hoverAudio;
    [SerializeField] AudioClip clickAudio;
    [SerializeField] AudioClip errorAudio;
    [SerializeField] AudioClip correctAudio;
    AudioSource audioSource;

    Player player;
    Question question;
    bool guessedCorrect;

    // COLORS
    Color32 green = new Color32(81, 255, 120, 255);
    Color32 red = new Color32(255, 91, 91, 255);
    Color32 defaultButtonColor = new Color32(220, 247, 255, 255);

    private void Awake()
    {
        player = GameObject.FindObjectOfType<Player>();
        audioSource = GetComponent<AudioSource>();
    }

    public void GetQuestion(Question questionFromTrigger)
    {
        question = questionFromTrigger;
        player.SetCanMove(false);
        DisplayQuestion();
    }

    public void DisplayQuestion()
    {
        questionObject.SetActive(true);

        questionText.text = question.GetQuestion();
        string[] answers = question.GetAnswers();
        explanationText.text = question.GetExplanation();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = answers[i];
        }
    }

    public void CheckAnswer(int answer)
    {
        if (!guessedCorrect){
            if (answer == question.GetCorrectAnswer()) {
                // Change to green color
                ChangeButtonColor(answerButtons[answer], green);
                audioSource.PlayOneShot(correctAudio);

                guessedCorrect = true;

                player.GetComponent<Animator>().SetBool("correctAnswer", true);

                Invoke("CorrectAnswer", 1.5f);
            }
            else if (answer != question.GetCorrectAnswer() && !guessedCorrect) {
                // Change to red color
                ChangeButtonColor(answerButtons[answer], red);
                audioSource.PlayOneShot(errorAudio);
            }
        }
    }

    void ChangeButtonColor(GameObject button, Color32 color)
    {
        button.GetComponent<Image>().color = color;
    }

    void CorrectAnswer()
    {
        player.GetComponent<Animator>().SetBool("correctAnswer", false);
        questionObject.SetActive(false);
        explanationObject.SetActive(true);
    }

    public void EndQuestion()
    {
        ResetButtonsColor();
        guessedCorrect = false;
        explanationObject.SetActive(false);
        player.SetCanMove(true);

        if (question.GetIsLastQuestion()) {
            ChangeScene();
        }
    }

    void ResetButtonsColor() {
        for (int i = 0; i < answerButtons.Length; i++) {
            ChangeButtonColor(answerButtons[i], defaultButtonColor);
        }
    }

    public void Hover()
    {
        audioSource.PlayOneShot(hoverAudio);
    }

    public void Click()
    {
        audioSource.PlayOneShot(clickAudio);
    }

    void ChangeScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}