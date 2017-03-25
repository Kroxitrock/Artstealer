#if UNITY_ANDROID || UNITY_IOS

using UnityEngine;
using System;

namespace UnityEngine.Advertisements
{
	public class UnityAdsBridge : MonoBehaviour
	{
		private static UnityAdsBridge impl;

		private Action<ShowResult> callback;

		public static UnityAdsBridge GetImpl()
		{
			if(!impl)
			{
				impl = (UnityAdsBridge)FindObjectOfType(typeof(UnityAdsBridge));
			}
				
			if(!impl)
			{
				GameObject singleton = new GameObject() { hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector };
				impl = singleton.AddComponent<UnityAdsBridge>();
				singleton.name = @"UnityAdsPluginBridgeObject";
				DontDestroyOnLoad(singleton);
			}
				
			return impl;
		}

		public static void SetCallback(Action<ShowResult> showCallback)
		{
			GetImpl().callback = showCallback;
		}

		public void Awake () {
			if(gameObject == GetImpl().gameObject) {
				DontDestroyOnLoad(gameObject);
			}
			else {
				Destroy(gameObject);
			}
		}

		public void onUnityAdsReady(string placementId)
		{
			Debug.Log(@"onUnityAdsReady " + placementId);
		}
		
		public void onUnityAdsStart(string placementId)
		{
			Debug.Log(@"onUnityAdsStart " + placementId);
		}

		public void onUnityAdsCompleted(string placementId)
		{
			Debug.Log(@"onUnityAdsCompleted" + placementId);
			if(callback != null) {
				callback(ShowResult.Finished);
			}
		}

		public void onUnityAdsSkipped(string placementId)
		{
			Debug.Log(@"onUnityAdsSkipped" + placementId);
			if(callback != null) {
				callback(ShowResult.Skipped);
			}
		}

		public void onUnityAdsFailed(string placementId)
		{
			Debug.Log(@"onUnityAdsFailed" + placementId);
			if(callback != null) {
				callback(ShowResult.Failed);
			}
		}
	}
}

#endif