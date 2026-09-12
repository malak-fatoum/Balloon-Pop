using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] balloons;
    [SerializeField] Transform[] balloonPos;

    public void Initialize()
    {
        InvokeRepeating(nameof(Spawn), 1f, 1f);
    }

    public void StopSpawning()
    {
        if (IsInvoking(nameof(Spawn)))
        {
            CancelInvoke(nameof(Spawn));
        }
    }

    void Spawn()
    {
        int randomBalloon = Random.Range(0, balloons.Length);
        int randomBalloonPos = Random.Range(0, balloonPos.Length);

        Instantiate(
            balloons[randomBalloon],
            balloonPos[randomBalloonPos].position,
            balloonPos[randomBalloonPos].rotation
        );
    }
}