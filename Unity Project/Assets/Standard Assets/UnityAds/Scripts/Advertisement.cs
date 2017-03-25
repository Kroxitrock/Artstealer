using System;

namespace UnityEngine.Advertisements
{
	public static class Advertisement
	{
		static bool s_Initialized;
		static readonly IPlatform s_Platform = GetPlatform();
		static bool s_Showing;
		static DebugLevelInternal s_DebugLevel = Debug.isDebugBuild ? DebugLevelInternal.Error | DebugLevelInternal.Warning | DebugLevelInternal.Info | DebugLevelInternal.Debug : DebugLevelInternal.Error | DebugLevelInternal.Warning | DebugLevelInternal.Info;

		static IPlatform GetPlatform()
		{
			try
			{
#if UNITY_EDITOR
				return new UnityEngine.Advertisements.Editor.EditorPlatform();
#elif UNITY_ANDROID
				return new AndroidPlatform();
#elif UNITY_IOS
				return new IosPlatform();
#else
				return new UnsupportedPlatform();
#endif
			}
			catch(Exception exception)
			{
				Debug.LogError("Initializing Unity Ads.");
				Debug.LogException(exception);
				return new UnsupportedPlatform();
			}
		}

		[Flags]
		enum DebugLevelInternal
		{
			None = 0,
			Error = 1,
			Warning = 2,
			Info = 4,
			Debug = 8
		}

		[Obsolete("Use Advertisement.debugMode instead.")]
		[Flags]
		public enum DebugLevel
		{
			None = 0,
			Error = 1,
			Warning = 2,
			Info = 4,
			Debug = 8
		}

		[Obsolete("Use Advertisement.debugMode instead.")]
		public static DebugLevel debugLevel
		{
			get
			{
				return (DebugLevel)s_DebugLevel;
			}
			set
			{
				s_DebugLevel = (DebugLevelInternal)value;
			}
		}

		public static bool isInitialized
		{
			get
			{
				return s_Initialized;
			}
			private set
			{
				s_Initialized = value;
			}
		}

		public static bool isSupported
		{
			get
			{
				return 
					Application.isEditor ||
					Application.platform == RuntimePlatform.Android && s_Platform.isSupported ||
					Application.platform == RuntimePlatform.IPhonePlayer && s_Platform.isSupported;
			}
		}

		public static bool debugMode
		{
			get
			{
				return s_Platform.debugMode;
			}
			set
			{
				s_Platform.debugMode = value;
			}
		}

		public static string version
		{
			get
			{
				return s_Platform.version;
			}
		}

		public static bool isShowing
		{
			get
			{
				// Because engine is paused when ads are showing, this is a legacy option that can be hardcoded to false
				return false;
			}
		}

		public static void Initialize(string gameId)
		{
			Initialize(gameId, false);
		}

		public static void Initialize(string gameId, bool testMode)
		{
			if(!isInitialized)
			{
				isInitialized = true;

				var framework = new MetaData("framework");
				framework.Set("name", "Unity");
				framework.Set("version", Application.unityVersion);
				SetMetaData(framework);

				var adapter = new MetaData("adapter");
				adapter.Set("name", "AssetStore");
				adapter.Set("version", version);
				SetMetaData(adapter);

				s_Platform.Initialize(gameId, testMode);
			}
		}

		public static bool IsReady()
		{
			return IsReady(null);
		}

		public static bool IsReady(string placementId)
		{
			return s_Platform.IsReady(string.IsNullOrEmpty(placementId) ? null : placementId);
		}

		public static PlacementState GetPlacementState()
		{
			return GetPlacementState(null);
		}

		public static PlacementState GetPlacementState(string placementId)
		{
			return s_Platform.GetPlacementState(string.IsNullOrEmpty(placementId) ? null : placementId);
		}

		public static void Show()
		{
			Show(null, null);
		}

		public static void Show(ShowOptions showOptions)
		{
			Show(null, showOptions);
		}

		public static void Show(string placementId)
		{
			Show(placementId, null);
		}

		public static void Show(string placementId, ShowOptions showOptions)
		{
			Action<ShowResult> callback = null;
			if(showOptions != null)
			{
				if(showOptions.resultCallback != null) {
					callback = showOptions.resultCallback;
				}
				if(!string.IsNullOrEmpty(showOptions.gamerSid))
				{
					var player = new MetaData("player");
					player.Set("server_id", showOptions.gamerSid);
					SetMetaData(player);
				}
			}
			s_Platform.Show(string.IsNullOrEmpty(placementId) ? null : placementId, callback);
		}

		public static void SetMetaData(MetaData metaData)
		{
			s_Platform.SetMetaData(metaData);
		}
	}
}