#if UNITY_EDITOR

using System;
using System.IO;
using System.Net;

namespace UnityEngine.Advertisements.Editor
{
	sealed class EditorPlatform : IPlatform
	{
		static string s_BaseUrl = "http://adserver.unityads.unity3d.com/games";

		bool m_DebugMode;
		Configuration m_Configuration;
		Placeholder m_Placeholder;

		public bool isInitialized
		{
			get
			{
				return m_Configuration != null;
			}
		}

		public bool isSupported
		{
			get
			{
				return Application.isEditor;
			}
		}

		public string version
		{
			get
			{
				return "2.0.8";
			}
		}

		public bool debugMode
		{
			get
			{
				return m_DebugMode;
			}
			set
			{
				m_DebugMode = value;
			}
		}

		public void Initialize(string gameId, bool testMode)
		{
			if(debugMode)
			{
				Debug.Log("UnityAdsEditor: Initialize(" + gameId + ", " + testMode + ");");
			}

			var placeHolderGameObject = new GameObject("UnityAdsEditorPlaceHolderObject")
			{
				hideFlags = HideFlags.HideAndDontSave | HideFlags.HideInInspector
			};
			m_Placeholder = placeHolderGameObject.AddComponent<Placeholder>();

			string configurationUrl = string.Join("/", new string[] {
				s_BaseUrl,
				gameId,
				string.Join("&", new string[] {
					"configuration?platform=editor",
					"unityVersion=" + Uri.EscapeDataString(Application.unityVersion),
					"sdkVersionName=" + Uri.EscapeDataString(version)
				})
			});
			WebRequest request = WebRequest.Create(configurationUrl);
			request.BeginGetResponse(result =>
			                         {
				WebResponse response = request.EndGetResponse(result);
				var reader = new StreamReader(response.GetResponseStream());
				string responseBody = reader.ReadToEnd();
				try
				{
					m_Configuration = new Configuration(responseBody);
					if (!m_Configuration.enabled)
					{
						Debug.LogWarning("gameId " + gameId + " is not enabled");
					}
				}
				catch (Exception exception)
				{
					Debug.LogError("Failed to parse configuration for gameId: " + gameId);
					Debug.Log(responseBody);
					Debug.LogException(exception);
				}
				reader.Close();
				response.Close();
			}, null);
		}

		public bool IsReady(string placementId)
		{
			if(placementId == null)
			{
				return isInitialized;
			}
			return isInitialized && m_Configuration.placements.ContainsKey(placementId);
		}

		public PlacementState GetPlacementState(string placementId)
		{
			if(IsReady(placementId))
			{
				return PlacementState.Ready;
			}
			return PlacementState.NotAvailable;
		}

		public void Show(string placementId, Action<ShowResult> callback)
		{
			// If placementId is null, use explicit defaultPlacement to match native behaviour
			if(isInitialized && placementId == null)
			{
				placementId = m_Configuration.defaultPlacement;
			}
			if(IsReady(placementId))
			{
				m_Placeholder.SetCallback(callback);
				m_Placeholder.Show(m_Configuration.placements[placementId]);
			}
			else
			{
				if(callback != null) {
					callback(ShowResult.Failed);
				}
			}
		}

		public void SetMetaData(MetaData metaData)
		{
		}
	}
}

#endif
