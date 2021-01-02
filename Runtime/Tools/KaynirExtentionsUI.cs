using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class KaynirExtentionsUI
{
    public static void RefreshLayoutGroups(this RectTransform root)
    {
        var layoutGroups = root.GetComponentsInChildren<LayoutGroup>();

        foreach (LayoutGroup group in layoutGroups)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(group.GetComponent<RectTransform>());
        }
    }
}
