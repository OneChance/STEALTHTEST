using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneFadeInOut : MonoBehaviour
{
		public float fadeSpeed = 1.5f;
		private bool sceneStarting = true;
		Image fadePanel;
		
		void Awake ()
		{
				fadePanel = GetComponent<Image> ();
		}

		void Update ()
		{
				if (sceneStarting) {
						FadeToClear ();
				}
		}

		void FadeToClear ()
		{
				fadePanel.color = Color.Lerp (fadePanel.color, Color.clear, fadeSpeed * Time.deltaTime);
		}

		void FadeToBlack ()
		{
				fadePanel.color = Color.Lerp (fadePanel.color, Color.black, fadeSpeed * Time.deltaTime);
		}

		void StartScene ()
		{
				FadeToClear ();
				if (fadePanel.color.a < 0.05) {
						fadePanel.color = Color.clear;
						fadePanel.enabled = false;
						sceneStarting = false;
				}
		}

		public void EndScene ()
		{
				fadePanel.enabled = true;		
				FadeToBlack ();
				if (fadePanel.color.a > 0.95) {
						Application.LoadLevel ("main");
				}
		}
}
