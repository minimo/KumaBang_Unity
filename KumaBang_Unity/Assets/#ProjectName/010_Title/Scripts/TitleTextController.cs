using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleTextController : MonoBehaviour {

    [SerializeField] int index = 0;

	// Use this for initialization
	void Start () {
        //出現アニメーション
        switch (this.index) {
            case 0:
                this.transform.DOLocalMove(new Vector3(-150.0f, 300.0f, 0.0f), 3.0f)
                    .SetEase(Ease.OutBounce)
                    .SetDelay(0.5f);
//                    .SetRelative();
                break;
            case 1:
                this.transform.DOLocalMove(new Vector3(150.0f, 200.0f, 0.0f), 3.0f)
                    .SetEase(Ease.OutBounce)
                    .SetDelay(0.5f);
//                    .SetRelative();
                break;
            case 2:
                this.transform.DOLocalMove(new Vector3(0.0f, -300.0f, 0.0f), 2.0f)
                    .SetEase(Ease.OutBounce)
                    .SetDelay(2.0f);
//                    .SetRelative();
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
	}
}
