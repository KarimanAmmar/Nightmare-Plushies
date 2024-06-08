using UnityEngine;

public class FrameRateDisplay : MonoBehaviour
{
	GUIStyle style = new GUIStyle();
	Rect frameRateRect;
	Rect performanceRect;
	Rect errorRect;
	float deltaTime = 0.0f;

	void Start()
	{
		int w = Screen.width, h = Screen.height;
		frameRateRect = new Rect(0, h * 0.05f, w, h * 0.02f);
		performanceRect = new Rect(0, h * 0.1f, w, h * 0.02f);
		errorRect = new Rect(0, h * 0.15f, w, h * 0.02f);
		style.alignment = TextAnchor.UpperCenter;
		style.fontSize = Mathf.RoundToInt(h * 0.02f);
		style.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	}

	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

		if (ErrorCondition())
		{
			Debug.LogError("An error occurred!");
		}
	}

	bool ErrorCondition()
	{
		return false;
	}

	void OnGUI()
	{
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string frameRateText = string.Format("Frame Rate: {0:0.} fps", fps);

		float performance = Mathf.Clamp01(1.0f - (msec / 16.0f));
		string performanceText = string.Format("Performance: {0:0.}%", performance * 100);

		frameRateRect.x = Screen.width / 2 - frameRateRect.width / 2;
		performanceRect.x = Screen.width / 2 - performanceRect.width / 2;
		errorRect.x = 0;

		GUI.Label(frameRateRect, frameRateText, style);
		GUI.Label(performanceRect, performanceText, style);

		if (ErrorCondition())
		{
			GUI.Label(errorRect, "An error occurred!", style);
		}
	}
}
