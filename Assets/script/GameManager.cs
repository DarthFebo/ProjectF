
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{

    private void OnTriggerEnter(Collider collision)
    {
        EndGame();
    }

    public void EndGame()
    {
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
