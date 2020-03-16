using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hotfix.Manager;
using System;

namespace Hotfix.UI
{
    public enum DialogType
    {
        OnlyConfirm,
        ConfirmAndCancel
    }

	public static class UIHelper
	{
        static GameObject m_dialogViewPrefab;

        #region ShowPanel Method
        public static void ShowPanel<T>() where T : UIPanel
        {
            ShowPanel<T>(null, null);
        }

        public static void ShowPanel<T>(Action<T> callback) where T : UIPanel
        {
            ShowPanel<T>(callback, null);
        }

        public static void ShowPanel<T>(Action<T> callback, object data) where T : UIPanel
        {
            UIPanelManager.instance.ShowPanel<T>(callback, data);
        }
        #endregion

        #region Dialog
        public static void ShowDialogConfirmAndCancel(string title, string content, Action confirmCallback = null)
        {
            ShowDialog(DialogType.ConfirmAndCancel, title, content, confirmCallback);
        }
        public static void ShowDialogOnlyConfirm(string title, string content, Action confirmCallback = null)
        {
            ShowDialog(DialogType.OnlyConfirm, title, content, confirmCallback);
        }
        public static void ShowDialog(DialogType type, string title, string content, Action confirmCallback = null)
        {
            if(m_dialogViewPrefab == null)
                m_dialogViewPrefab = Resources.Load("DialogView") as GameObject;

            GameObject go = GameObject.Instantiate(m_dialogViewPrefab) as GameObject;
            DialogView view = UIViewManager.instance.CreateView<DialogView>(go);
            view.Setting(type, title, content, confirmCallback);
            view.Show();
        }
        #endregion
    }
}

