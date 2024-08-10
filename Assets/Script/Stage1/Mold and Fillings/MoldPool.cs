using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class MoldPool : MonoBehaviour
{
    public ObjectPool<Mold> pool;
    [SerializeField] Mold mold;
    [SerializeField] GameObject startPos;

    private void Awake()
    {
        pool = new ObjectPool<Mold>(CreateMold, OnTakeMoldFromPool,
            OnReturnMoldToPool, OnDestroyMold, true, 4, 10);
    }

    private Mold CreateMold()
    {

        Mold newMold = Instantiate(mold, this.transform);
        newMold.SetPool(pool);

        return newMold;
    }

    private void OnTakeMoldFromPool(Mold _mold)
    {
        transform.position = startPos.transform.position;
        _mold.gameObject.SetActive(true);
    }

    private void OnReturnMoldToPool(Mold _mold)
    {
        transform.position = startPos.transform.position;
        _mold.gameObject.SetActive(false);
    }

    private void OnDestroyMold(Mold _mold)
    {

        Destroy(_mold.gameObject);
    }
}
