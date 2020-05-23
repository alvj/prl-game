using UnityEngine;

[CreateAssetMenu(fileName = "Nueva Pregunta", menuName = "Question")]
public class Question : ScriptableObject
{
    [TextArea(3, 15)]
    [SerializeField] string question;
    [TextArea(2, 10)]
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswer;
    [TextArea(4, 30)]
    [SerializeField] string explanation;
}
