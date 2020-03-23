using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;

    [Header("Enemies")]
    public Enemy enemyPrefab;
    public Transform enemyParent;

    [Header("Spawn")]
    public float altitudeToSpawn;

    [Header("Altitude")]
    public TextMeshProUGUI altitudeTxt;
    public Transform pointAltitude;

    private string _textAltitude = "{0:0}M";
    private float _currentAltitude;
    private float _lastAltitude = 0;
    private float _currentAltitudeSpawn;

    private void Start()
    {
        _currentAltitudeSpawn = altitudeToSpawn;
    }

    private void Update()
    {
        AddAltitude();
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
            _currentAltitudeSpawn += _currentAltitude + altitudeToSpawn;
            Debug.Log($"<b> SPAWN ENEMY </b>");
        }
    }
}