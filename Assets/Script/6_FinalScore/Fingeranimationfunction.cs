using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fingeranimationfunction : MonoBehaviour
{
    [SerializeField] ReplyController replyController;
    
    void AllSlide()
    {
        replyController.AllSlide();
    }
}
