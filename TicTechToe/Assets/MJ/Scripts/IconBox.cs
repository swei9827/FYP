using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconBox : MonoBehaviour
{
	public Image icon;
	public GameObject iconBox;
	public Animator iconBoxAnim;
    bool recorded;
    public bool playerIconBox;

	public void SetIcon(Sprite s)
	{
		icon.sprite = s;
        if (s == null)
        {
            iconBoxAnim.SetBool("Enable", false);
            FxManager.StopMusic("PickedUpFx");
        }
        else if (s != null)
		{
			iconBoxAnim.SetBool("Enable", true);
            if(!recorded && playerIconBox) {
                FxManager.PlayMusic("PickedUpFx");
                DataRecord.AddEvents(0, s.name.ToString());
                recorded = true;
            }
        }
	}

	public void Close ()
	{
		iconBoxAnim.SetBool("Enable", false);
        recorded = false;
	}

}
