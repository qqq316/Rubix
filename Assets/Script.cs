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

    private void RefreshBlocksReference()
    {
        GameObject[,,] copy = (GameObject[,,])blocks.Clone();

        foreach (GameObject b in blocks)
        {
            //b.GetComponent<MeshRenderer>().material.color = Color.cyan;
            //b.transform.SetParent(this.transform, true);
            copy[Mathf.RoundToInt(b.transform.position.x), Mathf.RoundToInt(b.transform.position.y), Mathf.RoundToInt(b.transform.position.z)] = b;
            //b.transform.Translate(0f, 1f, 0f);
        }
        blocks = copy;
        return;
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
                    blocks[i, j, k] = Object.Instantiate(temp, this.transform);
                    blocks[i, j, k].transform.position = new Vector3(i, j, k);
                }
            }
        }
        blocks[2, 2, 2].GetComponent<MeshRenderer>().material.color = Color.red;
    }
    //此函数折叠起来更好看
    private void Parenting(char c)
    {
        switch (c)
        {
            case 'r'://right
                temp = blocks[2, 1, 1];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (i == 1 && j == 1)
                        {
                            continue;
                        }
                        blocks[2, i, j].transform.SetParent(temp.transform);
                    }
                }
                break;
            case 'l'://left
                temp = blocks[0, 1, 1];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (i == 1 && j == 1)
                        {
                            continue;
                        }
                        blocks[0, i, j].transform.SetParent(temp.transform);
                    }
                }
                break;
            case 'f'://front
                temp = blocks[1, 1, 0];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (i == 1 && j == 1)
                        {
                            continue;
                        }
                        blocks[i, j, 0].transform.SetParent(temp.transform);
                    }
                }
                break;
            case 'b'://back
                temp = blocks[1, 1, 2];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (i == 1 && j == 1)
                        {
                            continue;
                        }
                        blocks[i, j, 2].transform.SetParent(temp.transform);
                    }
                }
                break;
            case 'u'://up
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
                break;
            case 'd'://down
                temp = blocks[1, 0, 1];
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
                break;
            default:
                Debug.LogError("Please check the input character in method \"parenting\" ");
                break;
        }
    }

    private IEnumerator Rot(char c, Transform o)
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
                    o.Rotate(0, -3f, 0);
                    yield return 0;
                }
                o.rotation = Quaternion.Euler(0, dest, 0);

                break;
            case 'x':

                dest = o.transform.rotation.eulerAngles.x + 90;
                for (int i = 0; i < 30; i++)
                {
                    o.Rotate(3f, 0, 0);
                    yield return 0;
                }
                //o.rotation = Quaternion.Euler(dest, 0, 0);

                break;
            case 'X':

                dest = o.transform.rotation.eulerAngles.x - 90;
                for (int i = 0; i < 30; i++)
                {
                    o.Rotate(-3f, 0, 0);
                    yield return 0;
                }
                //o.rotation = Quaternion.Euler(dest, 0, 0);

                break;
                //ToDo: 续写
        }
        RefreshBlocksReference();

        yield break;
    }

    ///<summary>对顶面顺时针旋转90度</summary>
    public void RotUp()
    {
        Parenting('u');
        StartCoroutine(Rot('y', blocks[1, 2, 1].transform));
    }
    /// <summary>
    /// 逆时针旋转顶面90度
    /// </summary>
    public void RotUp_()
    {
        Parenting('u');
        //IEnumerator coroutine = Rot('y', blocks[1, 2, 1].transform);
        StartCoroutine(Rot('Y', blocks[1, 2, 1].transform));

    }

    /// <summary>positive x</summary>
    public void RotRight()
    {
        Parenting('r');
        //IEnumerator coroutine = Rot('y', blocks[1, 2, 1].transform);
        StartCoroutine(Rot('x', blocks[2, 1, 1].transform));
    }
    public void RotRight_()
    {
        Parenting('r');
        //IEnumerator coroutine = Rot('y', blocks[1, 2, 1].transform);
        StartCoroutine(Rot('X', blocks[2, 1, 1].transform));
    }

    public void RotDown()
    {

    }

    //逐帧地、平滑地旋转选中的魔方块
}
