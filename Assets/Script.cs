using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// <para> 我的巨作：
///       _______________
///      /    /    /    /|
///     /____/____/____/ |
///    /    /    /    /| |
///   /____/____/____/ |/|
///  /    /    /    /| | |
/// /____/____/____/ |/|/|
/// |    |    |    | | | |
/// |____|____|____|/|/|/
/// |    |    |    | | /
/// |____|____|____|/|/
/// |    |    |    | /
/// |____|____|____|/
///    </para>
///    《魔方》
/// </summary>




public class Script : MonoBehaviour
{

    ///<summary>临时变量</summary>
    GameObject temp;

    ///<summary>存储对所有魔方块在当前位置的引用</summary>
    GameObject[,,] blocks;
    Script()
    {
        blocks = new GameObject[3, 3, 3];
    }

    void Start()
    {

        temp = Resources.Load<GameObject>("block");
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    blocks[i, j, k] = Object.Instantiate(temp);
                    blocks[i, j, k].transform.position = new Vector3(i, j, k);
                }
            }
        }
    }

    ///<summary>对顶面顺时针旋转90度</summary>
    public void RotUp()
    {
        temp = blocks[1, 2, 1];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (i == 1 && j == 1)
                {
                    continue;
                }
                blocks[i, 2, j].transform.SetParent(temp.transform);
            }
        }
        IEnumerator coroutine = Rot('y', blocks[1, 2, 1].transform);
        StartCoroutine(coroutine);


        //更改角块的引用 !IMPORTANT!
        temp = blocks[0, 2, 0];
        blocks[0, 2, 0] = blocks[2, 2, 0];
        blocks[2, 2, 0] = blocks[2, 2, 2];
        blocks[2, 2, 2] = blocks[0, 2, 2];
        blocks[0, 2, 2] = temp;

        //更改边块的引用 !IMPORTANT!
        temp = blocks[1, 2, 0];
        blocks[1, 2, 0] = blocks[2, 2, 1];
        blocks[2, 2, 1] = blocks[1, 2, 2];
        blocks[1, 2, 2] = blocks[0, 2, 1];
        blocks[0, 2, 1] = temp;

    }

    /// <summary>
    /// 逆时针旋转顶面90度
    /// <para>ToDo: 续写这个函数，算上<c>RotUp</c>写12个，可以适当复制粘贴</para>
    /// 
    /// </summary>
    public void RotUp_()
    {

    }
    public void RotDown()
    {

    }

    //逐帧地、平滑地旋转选中的魔方块
    IEnumerator Rot(char c, Transform o)
    {
        float dest;
        switch (c)
        {
            case 'y':

                dest = o.transform.rotation.eulerAngles.y + 90;
                for (int i = 0; i < 30; i++)
                {
                    o.Rotate(0, 3f, 0);
                    yield return 0;
                }
                o.rotation = Quaternion.Euler(0, dest, 0);

                break;
            case 'Y':

                dest = o.transform.rotation.eulerAngles.y - 90;
                for (int i = 0; i < 30; i++)
                {
                    o.Rotate(0, 3f, 0);
                    yield return 0;
                }
                o.rotation = Quaternion.Euler(0, dest, 0);

                break;
            case 'x':

                dest = o.transform.rotation.eulerAngles.x + 90;
                for (int i = 0; i < 30; i++)
                {
                    o.Rotate(0, 3f, 0);
                    yield return 0;
                }
                o.rotation = Quaternion.Euler(0, dest, 0);

                break;
                //ToDo: 续写
        }
        yield break;
    }
}
