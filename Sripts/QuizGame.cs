using UnityEngine;
using UnityEngine.UI;

public class QuizGame : MonoBehaviour
{
    public Image questionImage;
    public Button[] optionButtons;
    public Text scoreText;
    public Text questionText;
    public Text totalScoreText;
    public Question[] questions;
    public GameObject quizPanel;
    public GameObject resultPanel;
    public GameObject submissionPanel;
    public Button restartButton;
    public Button submissionButton;
    public Sprite starSprite;
    public Sprite smallTrophySprite;
    public Sprite bigTrophySprite;
    public Image rewardImage;
    public Text rewardText;

    private int currentQuestionIndex;
    private int score;
    private AudioSource audioSource;
    private AudioClip currentAudioClip; // Track the current audio clip

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DisplayQuestion();
    }

    private void DisplayQuestion()
    {
        if (currentQuestionIndex >= questions.Length)
        {
            QuizCompleted();
            return;
        }

        Question currentQuestion = questions[currentQuestionIndex];

        // Stop the previous audio clip before playing the new one
        if (currentAudioClip != null)
        {
            audioSource.Stop();
        }

        // Play the audio clip for the current question
        currentAudioClip = currentQuestion.audioClip;
        audioSource.clip = currentAudioClip;
        audioSource.Play();

        questionImage.sprite = currentQuestion.questionImage;
        questionText.text = currentQuestion.questionText;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].GetComponentInChildren<Text>().text = currentQuestion.options[i];
        }
    }

    public void CheckAnswer(int chosenOption)
    {
        Question currentQuestion = questions[currentQuestionIndex];

        if (chosenOption == currentQuestion.correctOption)
        {
            score += 5;
        }

        scoreText.text = "Score: " + score;

        // Stop the audio clip for the current question
        if (currentAudioClip != null)
        {
            audioSource.Stop();
        }

        currentQuestionIndex++;

        if (currentQuestionIndex < questions.Length)
        {
            DisplayQuestion();
        }
        else
        {
            QuizCompleted();
        }
    }

    private void QuizCompleted()
    {
        Debug.Log("Quiz completed! Final score: " + score);

        totalScoreText.text = "Your Total Score is: " + score+"/60";

        quizPanel.SetActive(false);
        resultPanel.SetActive(false);
        submissionPanel.SetActive(true);


        if (score <= 30)
        {
            CreateReward(starSprite, new Vector3(1.0f, 1.0f, 1f));
            rewardText.text = "Congratulations, You have won a star, better luck for next time...!!";
        }
        else if (score <= 50)
        {
            CreateReward(smallTrophySprite, new Vector3(1.0f, 1.0f, 1f));
            rewardText.text = "Congratulations, You have won a Cup, Excellent..!!";
        }
        else
        {
            CreateReward(bigTrophySprite, new Vector3(1.0f, 1.0f, 1f));
            rewardText.text = "Congratulations, You have won a Trophy, Wonderful..!!";
        }

        restartButton.onClick.AddListener(RestartQuiz);
    }

    private void CreateReward(Sprite sprite, Vector3 scale)
    {
        if (rewardImage == null)
        {
            Debug.LogError("Reward Image component is not assigned!");
            return;
        }

        rewardImage.sprite = sprite;
        rewardImage.rectTransform.localScale = scale;
    }

    public void PreviousQuestion()
    {
        currentQuestionIndex--;
        DisplayQuestion();
    }

    private void RestartQuiz()
    {
        currentQuestionIndex = 0;
        score = 0;

        scoreText.text = "Score: 0";
        totalScoreText.text = "";

        rewardImage.sprite = null;

        resultPanel.SetActive(false);
        quizPanel.SetActive(true);

        DisplayQuestion();
    }

    public void OnSubmissionButtonClicked()
    {
        ShowResultPanel();
    }

    private void ShowResultPanel()
{
    submissionPanel.SetActive(false);
    resultPanel.SetActive(true);
}


}

[System.Serializable]
public class Question
{
    public Sprite questionImage;
    public string questionText;
    public string[] options;
    public int correctOption;
    public AudioClip audioClip;
}
