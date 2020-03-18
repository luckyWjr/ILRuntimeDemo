using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hotfix
{
	public static class GlobalDefine
	{
		#region UI
		public const string UI_ROOT_NAME = "UI";
		public const string UI_DEFAULT_CANVAS_NAME = UI_ROOT_NAME + "/DefaultCanvas";
		public const string UI_BANNER_CANVAS_NAME = UI_ROOT_NAME + "/BannerCanvas";
		public const string UI_LOADING_CANVAS_NAME = UI_ROOT_NAME + "/LoadingCanvas";
		public const string UI_POPUP_CANVAS_NAME = UI_ROOT_NAME + "/PopupCanvas";
		#endregion
		
		#region Scene
		public const string SCENE_PATH = "Scenes/";
		public const string SCENE_SUFFIX = ".unity";
		public const string CLEAR_SCENE_NAME = "ClearScene";
		public const string SAMPLE_SCENE_NAME = "SampleScene";
		public const string GAME_SCENE_NAME = "GameScene";
		#endregion
	}
}

