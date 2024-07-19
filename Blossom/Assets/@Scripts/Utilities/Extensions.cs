using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions {

    #region Generals


    public static T GetOrAddComponent<T>(this GameObject obj) where T : Component => Utilities.GetOrAddComponent<T>(obj);
    public static T FindChild<T>(this GameObject obj, string name = null) where T : Component => Utilities.FindChild<T>(obj, name);
    public static T FindChildDirect<T>(this GameObject obj, string name = null) where T : Component => Utilities.FindChildDirect<T>(obj, name);
    public static GameObject FindChild(this GameObject obj, string name = null) => Utilities.FindChild(obj, name);
    public static GameObject FindChildDirect(this GameObject obj, string name = null) => Utilities.FindChildDirect(obj, name);


    #endregion

}