using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LicenseTimer : MonoBehaviour {

	private int timeOnPause = 0;
	private int timeOnResume = 0;


	public void LicenseTimerStart() {
		InvokeRepeating("LicenseExpirationTime", 1f, 1f);
	}


	public void LicenseTimerStop() {
		CancelInvoke("LicenseExpirationTime");
	}


	private void LicenseExpirationTime() {
		StaticVars.licenseExpirationTime--;

		if (StaticVars.licenseExpirationTime < 1) {
			CancelInvoke("LicenseExpirationTime");
			SceneManager.LoadScene(0, LoadSceneMode.Single);
		}
	}


	private void OnApplicationPause(bool pauseStatus) {
		int hourOnPause;
		int minutesOnPause;
		int secondsOnPause;

		int hourOnResume;
		int minutesOnResume;
		int secondsOnResume;

		if (pauseStatus) {
			hourOnPause = int.Parse(DateTime.Now.ToString("HH"));
			minutesOnPause = int.Parse(DateTime.Now.ToString("mm"));
			secondsOnPause = int.Parse(DateTime.Now.ToString("ss"));
			timeOnPause = (hourOnPause * 60 * 60) + (minutesOnPause * 60) + secondsOnPause;
		}
		else {
			hourOnResume = int.Parse(DateTime.Now.ToString("HH"));
			minutesOnResume = int.Parse(DateTime.Now.ToString("mm"));
			secondsOnResume = int.Parse(DateTime.Now.ToString("ss"));
			timeOnResume = (hourOnResume * 60 * 60) + (minutesOnResume * 60) + secondsOnResume;

			StaticVars.licenseExpirationTime = StaticVars.licenseExpirationTime - (timeOnResume - timeOnPause);
		}
	}
}