using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PresentScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var sequence = DOTween.Sequence(); //Sequence生成
                                           //Tweenをつなげる
        sequence.Append(transform.DOMoveX(transform.position.x + 0.2f, 0.1f))
                .Append(transform.DOMoveX(transform.position.x - 0.4f, 0.1f))
                .Append(transform.DOMoveX(transform.position.x + 0.2f, 0.1f))
                .SetDelay(3)
        .SetLoops(-1, LoopType.Restart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void dell()
    {
        this.gameObject.SetActive(false);
    }
}
