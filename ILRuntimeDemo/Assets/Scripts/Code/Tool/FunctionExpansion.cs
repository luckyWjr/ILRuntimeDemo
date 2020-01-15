using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool
{
	public static class FunctionExpansion
	{
        public static void SetParentAndResetTrans(this Transform trans, Transform parent)
        {
            trans.SetParent(parent);
            trans.ResetTrans();
        }

        public static void ResetTrans(this Transform trans)
        {
            trans.localPosition = Vector3.zero;
            trans.localRotation = Quaternion.identity;
            trans.localScale = Vector3.one;
        }

        public static void SetParentAndResetTrans(this RectTransform trans, Transform parent)
        {
            trans.SetParent(parent);
            trans.ResetTrans();
        }

        public static void ResetTrans(this RectTransform trans)
        {
            trans.transform.ResetTrans();
            trans.offsetMax = Vector2.zero;
            trans.offsetMin = Vector2.zero;
        }
    }
}

