using UnityEngine;
using UnityEngine.UI;

public class HpBarScript : MonoBehaviour
{
    public float maxW = 596, w = 0;
    public RectTransform bar;
    void Start()
    {
    }

    public void update_bar(float newHp)
    {
        bar.sizeDelta = new Vector2 (newHp, bar.sizeDelta.y);
    }
}
