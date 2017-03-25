#if UNITY_ANDROID

using System;
using System.Collections.Generic;

namespace UnityEngine.Advertisements
{
	sealed class AndroidPlatform : IPlatform
	{
		private static AndroidJavaObject wrapper;
		private static bool wrapperInitialized = false;

		private AndroidJavaObject getAndroidWrapper() {
			if(!wrapperInitialized) {
				wrapperInitialized = true;
				wrapper = new AndroidJavaObject("com.unity3d.ads.unity4.unity4wrapper.UnityAdsUnity4Wrapper");
			}
			
			return wrapper;
		}

		private AndroidJavaObject getCurrentActivity() {
			return (new AndroidJavaClass("com.unity3d.player.UnityPlayer")).GetStatic<AndroidJavaObject>("currentActivity");
		}

		public bool isInitialized
		{
			get
			{
				return getAndroidWrapper().Call<bool>("isInitialized");
			}
		}
		
		public bool isSupported
		{
			get
			{
				return getAndroidWrapper().Call<bool>("isSupported");
			}
		}
		
		public string version
		{
			get
			{
				return getAndroidWrapper().Call<string>("getVersion");
			}
		}
		
		public bool debugMode
		{
			get
			{
				return getAndroidWrapper().Call<bool>("getDebugMode");
			}
			set
			{
				getAndroidWrapper().Call("setDebugMode", value);
			}
		}
		
		public void Initialize(string gameId, bool testMode)
		{
			getAndroidWrapper().Call("initialize", getCurrentActivity(), gameId, testMode, UnityAdsBridge.GetImpl().gameObject.name);
		}
		
		public bool IsReady(string placementId)
		{
			if(placementId == null) {
				return getAndroidWrapper().Call<bool>("isReady");
			} else {
				return getAndroidWrapper().Call<bool>("isReady", placementId);
			}
		}
		
		public PlacementState GetPlacementState(string placementId)
		{
			AndroidJavaObject rawPlacementState;
			if(placementId == null)
			{
				rawPlacementState = getAndroidWrapper().Call<AndroidJavaObject>("getPlacementState");
			}
			else
			{
				rawPlacementState = getAndroidWrapper().Call<AndroidJavaObject>("getPlacementState", placementId);
			}
			return (PlacementState)rawPlacementState.Call<int>("ordinal");
		}
		
		public void Show(string placementId, Action<ShowResult> callback)
		{
			UnityAdsBridge.SetCallback(callback);
			if(placementId != null) {
				getAndroidWrapper().Call("show", getCurrentActivity(), placementId);
			} else {
				getAndroidWrapper().Call("show", getCurrentActivity());
			}
		}

		public void SetMetaData(MetaData metaData)
		{
			var metaDataObject = new AndroidJavaObject("com.unity3d.ads.metadata.MetaData", getCurrentActivity());
			metaDataObject.Call("setCategory", metaData.category);
			foreach(KeyValuePair<string, object> entry in metaData.Values())
			{
				metaDataObject.Call("set", entry.Key, entry.Value);
			}
			metaDataObject.Call("commit");
		}
	}
}

#endif