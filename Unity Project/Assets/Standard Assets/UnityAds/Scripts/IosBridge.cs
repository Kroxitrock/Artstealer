#if UNITY_IOS

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

namespace UnityEngine.Advertisements {
	internal static class IosBridge {
		[DllImport ("__Internal")]
		public static extern void UnityAdsInit(string gameId, bool testMode, string gameObjectName);

		[DllImport ("__Internal")]
		public static extern bool UnityAdsIsSupported();

		[DllImport ("__Internal")]
		public static extern bool UnityAdsIsReady(string placementId);

		[DllImport ("__Internal")]
		public static extern long UnityAdsGetPlacementState(string placementId);

		[DllImport ("__Internal")]
		public static extern void UnityAdsShow(string placementId);

		[DllImport ("__Internal")]
		public static extern void UnityAdsSetMetaData(string category, string data);

		[DllImport ("__Internal")]
		public static extern bool UnityAdsGetDebugMode();

		[DllImport ("__Internal")]
		public static extern void UnityAdsSetDebugMode(bool debugMode);

		[DllImport ("__Internal")]
		public static extern string UnityAdsGetVersion();

		[DllImport ("__Internal")]
		public static extern bool UnityAdsIsInitialized();
	}
}

#endif