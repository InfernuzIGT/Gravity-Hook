using System.Collections;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController player;

    [Header("Enemies")]
    public Enemy enemyPrefab;
    public Transform enemyParent;

    [Header("Spawn")]
    public Vector2 rangeWidthSpawn = new Vector2(-2, 2);
    public Vector2 rangeAltitudeSpawn = new Vector2(2, 4);
    public Vector2 rangeAmountSpawn = new Vector2(1, 2);
    public Vector2 rangeOffsetSpawn = new Vector2(0, 2);

    [Header("Altitude")]
    public TextMeshProUGUI altitudeTxt;
    public Transform pointAltitude;

    [Header("Other")]
    public bool isPlaying = true;
    public Image fadeImg;
    public float fadeTime;
    public float resetTime;

    private string _textAltitude = "{0:0}M";
    private float _currentAltitude;
    private float _lastAltitude = 0;
    private float _currentAltitudeSpawn;
    private float _amountSpawn;
    private GameOverEvent _gameOverEvent = new GameOverEvent();

    private void Start()
    {
        Fade(false);

        _currentAltitude = player.transform.position.y - pointAltitude.position.y;
        _currentAltitudeSpawn = (player.transform.position.y - pointAltitude.position.y) + rangeAltitudeSpawn.x;
    }

    private void OnEnable()
    {
        EventController.AddListener<GameOverEvent>(GameOver);
    }

    private void OnDisable()
    {
        EventController.RemoveListener<GameOverEvent>(GameOver);
    }

    private void Update()
    {
        if (isPlaying)
        {
            AddAltitude();
        }
    }

    private void AddAltitude()
    {
        _currentAltitude = player.transform.position.y - pointAltitude.position.y;

        if (_currentAltitude > _lastAltitude)
        {
            _lastAltitude = _currentAltitude;
            altitudeTxt.text = string.Format(_textAltitude, _lastAltitude);

            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (_currentAltitude > _currentAltitudeSpawn)
        {
            _currentAltitudeSpawn += Random.Range(rangeAltitudeSpawn.x, rangeAltitudeSpawn.y);

            _amountSpawn = Random.Range(rangeAmountSpawn.x, rangeAmountSpawn.y);

            for (int i = 0; i < _amountSpawn; i++)
            {
                Vector3 newPos = new Vector3(
                    Random.Range(rangeWidthSpawn.x, rangeWidthSpawn.y),
                    _currentAltitudeSpawn + Random.Range(rangeOffsetSpawn.x, rangeOffsetSpawn.y),
                    0);

                Instantiate(enemyPrefab, newPos, Quaternion.identity, enemyParent);
            }

        }
    }

    public void GameOver(GameOverEvent evt)
    {
        isPlaying = false;
        StartCoroutine(ResetGame());
    }

    private IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(resetTime);
        Fade(true);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(0);
    }
    private void Fade(bool fadeIn)
    {
        fadeImg.CrossFadeAlpha(fadeIn ? 1 : 0, fadeTime, true);
    }

}