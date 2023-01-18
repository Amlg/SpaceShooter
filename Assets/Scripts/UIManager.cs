using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //handle to text
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private TextMeshProUGUI _gameOverText;
    [SerializeField]
    private TextMeshProUGUI _restartGameText;
    private GameManager _gameManager;

   
    // Start is called before the first frame update
    void Start()
    {

        _scoreText.text = "Score: " + 0;
        _gameOverText.enabled = false;
        _restartGameText.enabled = false;
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL");
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }
    
    public void UpdateLives(int currentLives)
    {
        _LivesImg.sprite = _liveSprites[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }
    public void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.enabled = true;
        _restartGameText.enabled = true;
        StartCoroutine(gameOverFlickerRoutine());
    }
    IEnumerator gameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
    
}
