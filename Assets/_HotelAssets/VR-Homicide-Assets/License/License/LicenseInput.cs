using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class LicenseInput : MonoBehaviour {

	public void LicenseCheckWrapper() {
		GameObject.Find("Retain").GetComponent<LicenseTimer>().LicenseTimerStop();
		GameObject.Find("DemoCanvas").GetComponent<GraphicRaycaster>().enabled = false;
		StartCoroutine(LicenseCheck());
	}

	private IEnumerator LicenseCheck() {
		string url = StaticVars.licenseActivationUrl;
		WWWForm form = new WWWForm();
		form.AddField("licensekey", GameObject.Find("DemoCanvas/Text").GetComponent<Text>().text);
		form.AddField("deviceid", SystemInfo.deviceUniqueIdentifier);
		form.AddField("token", StaticVars.licenseServerSecretKey);
		WWW www = new WWW(url, form);

		float timer = 0f;
		float timeOut = 7f;
		bool failed = false;

		while (!www.isDone) {
			if (timer > timeOut) {
				failed = true;
				break;
			}
			timer += Time.deltaTime;
			yield return null;
		}

		if (failed || !string.IsNullOrEmpty(www.error))	 {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
		}
		else {
			if (www.text.All(char.IsDigit) && !string.IsNullOrEmpty(www.text)) {
				StaticVars.licenseExpirationTime = int.Parse(www.text);
				GameObject.Find("Retain").GetComponent<LicenseTimer>().LicenseTimerStart();
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
			}
			else {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
			}
		}
	}

}