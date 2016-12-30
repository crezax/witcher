using UnityEngine.SceneManagement;

public class RestartButton : BaseBehaviour {

  public void OnClick() {
    SceneManager.LoadScene(0);
  }
}
