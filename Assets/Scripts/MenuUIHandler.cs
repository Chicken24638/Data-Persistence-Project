using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;
    public TMP_InputField nameField;

    void Start()
    {
        SetBestScore();
        SetNameFieldText(nameField);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        GameData.Instance.SaveBestScore(GameData.Instance.bestScore, GameData.Instance.bestPlayerName);

        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    private void SetBestScore()
    {
        if (GameData.Instance.bestScore != 0 && GameData.Instance.bestPlayerName != "")
        {
            bestScoreText.text = $"Best Score : {GameData.Instance.bestPlayerName} : {GameData.Instance.bestScore}";
        }
        else if (GameData.Instance.bestScore != 0 && GameData.Instance.bestPlayerName == "")
        {
            bestScoreText.text = $"Best Score : Unknown Player : {GameData.Instance.bestScore}";
        }
        else
        {
            bestScoreText.text = "No best score yet";
        }
    }

    public void SetPlayerName(TMP_InputField nameField)
    {
        GameData.Instance.SaveName(nameField);
    }

    private void SetNameFieldText(TMP_InputField nameField)
    {
        nameField.text = GameData.Instance.playerName;
    }
}
