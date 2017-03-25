using System;

namespace UnityEngine.Advertisements
{
	sealed class UnsupportedPlatform : IPlatform
	{
		public bool isInitialized
		{
			get
			{
				return false;
			}
		}
		
		public bool isSupported
		{
			get
			{
				return false;
			}
		}
		
		public string version
		{
			get
			{
				return null;
			}
		}
		
		public bool debugMode
		{
			get
			{
				return false;
			}
			set
			{
			}
		}
		
		public void Initialize(string gameId, bool testMode)
		{
		}
		
		public bool IsReady(string placementId)
		{
			return false;
		}
		
		public PlacementState GetPlacementState(string placementId)
		{
			return PlacementState.NotAvailable;
		}
		
		public void Show(string placementId, Action<ShowResult> callback)
		{
			if(callback != null) {
				callback(ShowResult.Failed);
			}
		}
		
		public void SetMetaData(MetaData metaData)
		{
		}
	}
}
