using UnityEngine;

// One balloon. It floats up, and it dies in one of two ways:
// 1. the player pops it
// 2. it escapes off the top of the screen
public class BalloonController : MonoBehaviour
{
    public float speed;

    [SerializeField] float popDuration = 0.2f;
    [SerializeField] float escapeHeight = 7f;

    Animator anim;
    bool popped;

    void Awake()
    {
        anim = GetComponent<Animator>();
        speed = Random.Range(1f, 5f);
    }

    void Update()
    {
        if (GameManager.instance.IsGameOver())
        {
            return;
        }

        float multiplier = GameManager.instance.GetBalloonSpeedMultiplier();

        transform.Translate(
            Vector2.up * speed * multiplier * Time.deltaTime
        );

        if (transform.position.y > escapeHeight)
        {
            if (!popped)
            {
                GameManager.instance.BalloonEscaped();
            }

            Destroy(gameObject);
        }
    }

    public void Pop()
    {
        if (popped)
        {
            return;
        }

        popped = true;

        anim.SetTrigger("Destroy");
        GameManager.instance.DestroyBalloon();

        Destroy(gameObject, popDuration);
    }
}