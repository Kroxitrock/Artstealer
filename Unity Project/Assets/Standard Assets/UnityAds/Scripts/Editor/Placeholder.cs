#if UNITY_EDITOR

using System;
using System.IO;
using System.Reflection;

namespace UnityEngine.Advertisements.Editor
{
	sealed class Placeholder : MonoBehaviour
	{
		Texture2D m_LandscapeTexture;
		Texture2D m_PortraitTexture;
		
		bool m_Showing;
		
		bool m_AllowSkip;

		Action<ShowResult> callback;

		public void Awake()
		{
			m_LandscapeTexture = textureFromFile(Application.dataPath + "/Standard Assets/UnityAds/Textures/landscape.jpg");
			m_PortraitTexture = textureFromFile(Application.dataPath + "/Standard Assets/UnityAds/Textures/portrait.jpg");
		}

		public void SetCallback(Action<ShowResult> newCallback)
		{
			callback = newCallback;
		}

		public void Show(bool allowSkip)
		{
			m_AllowSkip = allowSkip;
			m_Showing = true;
		}
		
		public void OnGUI()
		{
			if(!m_Showing)
			{
				return;
			}
			GUI.ModalWindow(0, new Rect(0, 0, Screen.width, Screen.height), ModalWindowFunction, "");
		}

		void ModalWindowFunction(int id)
		{
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Screen.width > Screen.height ? m_LandscapeTexture : m_PortraitTexture, ScaleMode.ScaleAndCrop);
			
			if(m_AllowSkip && GUI.Button(new Rect(20, 20, 150, 50), "Skip"))
			{
				m_Showing = false;
				if(callback != null)
				{
					callback(ShowResult.Skipped);
				}
			}
			
			if(GUI.Button(new Rect(Screen.width - 170, 20, 150, 50), "Close"))
			{
				m_Showing = false;
				if(callback != null)
				{
					callback(ShowResult.Finished);
				}
			}
		}

		Texture2D textureFromFile(string filePath) {
			return textureFromBytes(textureBytesForFrame(filePath));
		}
		
		Texture2D textureFromBytes(byte[] bytes) {
			Texture2D texture2D = new Texture2D(1, 1);
			texture2D.LoadImage(bytes);
			return texture2D;
		}
		
		byte[] textureBytesForFrame(string imageURL) {
			byte[] array = null;
			using(FileStream fileStream = File.OpenRead(imageURL)) {
				int num = (int)fileStream.Length;
				array = new byte[num];
				int num2 = 0;
				int num3;
				while((num3 = fileStream.Read (array, num2, num - num2)) > 0) {
					num2 += num3;
				}
			}
			return array;
		}
	}
}

#endif