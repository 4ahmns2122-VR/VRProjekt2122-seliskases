using UnityEngine;
using UnityEngine.Chess;

public class PuzzleTrigger : MonoBehaviour
{
    public GameObject boardContainer;
    public GameObject key;
    public Transform keySpawnPos;
    public AudioSource timerSound;

    private bool keyIsSpawned = false;

    private void Start()
    {
        Board.puzzleFinishedDelegate += PuzzleFinished;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            boardContainer.SetActive(true);
            timerSound.PlayOneShot(timerSound.clip);
            Player.chessIsActivated = true;
        }
    }

    private void PuzzleFinished()
    {
        UserInterfaceManager.instance.SetToNormalHand();
        UserInterfaceManager.instance.timer.gameObject.SetActive(false);

        timerSound.Stop();

        if (!keyIsSpawned)
        {
            Instantiate(key, keySpawnPos.position, Quaternion.identity);
            keyIsSpawned = true;
        }
    }

    private void OnDestroy()
    {
        Board.puzzleFinishedDelegate -= PuzzleFinished;
    }
}
