using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button unlockLevelButton;

    private void Start()
    {
        // Load the gold score
        int goldScore = PlayerPrefs.GetInt("goldScore", 0);

        // Enable the button if the gold score is 100 or more
        unlockLevelButton.interactable = (goldScore >= 100);

        // Add a listener to the button to load the new level
        unlockLevelButton.onClick.AddListener(UnlockLevel);
    }

    private void UnlockLevel()
    {
        // Load the new level scene
        SceneManager.LoadScene("UnlockedLevel");
    }
}
