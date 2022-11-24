using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private void Start()
    {
        timeText = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        timeText.text = "Time: " + GameManager.Instance.CurrTime.ToString("F0");
        scoreText.text = "Score: " + GameManager.Instance.scoreMng.GetScore();
    }
}
