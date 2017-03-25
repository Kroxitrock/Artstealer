#if UNITY_IOS

using System;

namespace UnityEngine.Advertisements
{
	sealed class IosPlatform : IPlatform
	{
		public bool isInitialized
		{
			get
			{
				return IosBridge.UnityAdsIsInitialized();
			}
		}
		
		public bool isSupported
		{
			get
			{
				return IosBridge.UnityAdsIsSupported();
			}
		}
		
		public string version
		{
			get
			{
				return IosBridge.UnityAdsGetVersion();
			}
		}
		
		public bool debugMode
		{
			get
			{
				return IosBridge.UnityAdsGetDebugMode();
			}
			set
			{
				IosBridge.UnityAdsSetDebugMode(value);
			}
		}
		
		public void Initialize(string gameId, bool testMode)
		{
			IosBridge.UnityAdsInit(gameId, testMode, UnityAdsBridge.GetImpl().gameObject.name);
		}
		
		public bool IsReady(string placementId)
		{
			return IosBridge.UnityAdsIsReady(placementId);
		}

		public PlacementState GetPlacementState(string placementId)
		{
			return (PlacementState)IosBridge.UnityAdsGetPlacementState(placementId);
		}

		public void Show(string placementId, Action<ShowResult> callback)
		{
			UnityAdsBridge.SetCallback(callback);
			IosBridge.UnityAdsShow(placementId);
		}

		public void SetMetaData(MetaData metaData)
		{
			IosBridge.UnityAdsSetMetaData(metaData.category, metaData.ToJSON());
		}
	}
}

#endif