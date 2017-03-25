using System;

namespace UnityEngine.Advertisements
{
	interface IPlatform
	{
		bool isInitialized { get; }
		bool isSupported { get; }
		string version { get; }
		bool debugMode { get; set; }
		
		void Initialize(string gameId, bool testMode);
		bool IsReady(string placementId);
		PlacementState GetPlacementState(string placementId);
		void Show(string placementId, Action<ShowResult> callback);
		void SetMetaData(MetaData metaData);
	}
}