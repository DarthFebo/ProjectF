
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{

    private static int retryBuildIndex = 0;

    private void OnTriggerEnter(Collider collision)
    {
        EndGame();
    }

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Retry()
    {
        SceneManager.LoadScene(retryBuildIndex);
    }

    public void EndGame()
    {
        retryBuildIndex = SceneManager.GetActiveScene().buildIndex;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("diedscene");
    }

    public void MainMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("menuscene");
    }
    



}
