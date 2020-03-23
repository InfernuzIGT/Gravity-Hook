using Events;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool currentState;
    [Space]
    public Sprite spriteOn;
    public Sprite spriteOff;

    private string _tagPlayer = "Player";
    private string _tagEnemy = "Enemy";

    private SpriteRenderer _spriteRenderer;
    private GameOverEvent _gameOverEvent = new GameOverEvent();

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentState = false;
        _spriteRenderer.sprite = spriteOff;
    }

    public void ChangeState(bool state)
    {
        currentState = state;
        _spriteRenderer.sprite = currentState ? spriteOn : spriteOff;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(_tagPlayer) && currentState)
        {
            EventController.TriggerEvent(_gameOverEvent);
        }

        if (other.gameObject.CompareTag(_tagEnemy))
        {
            Destroy(other.gameObject);
        }
    }

}