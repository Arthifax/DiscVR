using UnityEngine;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour {
	void Start () {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
		Destroy(this);
	}
}