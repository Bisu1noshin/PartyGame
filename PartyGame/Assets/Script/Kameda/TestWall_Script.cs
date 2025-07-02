using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
/// <summary>
///     伸びる壁 テスト
/// </summary>
public class TestWall_Script : MonoBehaviour
{
    List<WallPosition> list;
    int stopCnt;
    public GameObject wallPre;
    const float Top = 4.0f, Right = 6.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        list = new List<WallPosition>();
        Set4Walls(new Vector2(-2, 1), 14, 6);
        Set4Walls(new Vector2(3, 0), 24, 8);
        Set4Walls(new Vector2(0, -2), 10, 4);
    }
    void Set4Walls(Vector2 pos, int max, int begin) //時計回りに4方向の壁を作る
    {
        Direction d_ = (Direction)(Random.Range(0, 4));
        for(int i = 0; i < 4; ++i)
        {
            SetWalls(d_, new Vector3(pos.x, -0.875f, pos.y), max, begin);
            SetRight(ref d_);
        }
    }
    void SetWalls(Direction d_, Vector3 pos, int max, int begin)
    {
        stopCnt = begin;
        Vector3 position = pos;
        while (CreateWall(position, d_, max)) //メインの壁を作って前に進む
        {
            position += Dir.GetFront(d_);
        }
        foreach (WallPosition p in list)
        {
            stopCnt = 0;
            Vector3 childrenPos = p.position + Dir.GetFront(p.dir);
            while (CreateWallChildren(childrenPos))
            {
                childrenPos += Dir.GetFront(p.dir);
            }
        }
        list.Clear();
    }
    bool CreateWall(Vector3 wallPos, Direction d_, int max)
    {
        int rand = Random.Range(0, max);
        DirectCreateWall(wallPos);
        switch (rand)
        {
            case 0: //右分岐
                list.Add(new WallPosition(wallPos, GetRightNum(d_)));
                break;
            case 1: //左分岐
                list.Add(new WallPosition(wallPos, GetLeftNum(d_)));
                break;
            case 2: //左右分岐
                list.Add(new WallPosition(wallPos, GetRightNum(d_)));
                list.Add(new WallPosition(wallPos, GetLeftNum(d_)));
                break;
            default://直進 or 止まる
                if(rand > max - stopCnt++) //止まる　満たさなければ直進
                {
                    return false;
                }
                break;
        }
        if (TouchGreatWall(wallPos)) //外壁に接したら終了
        {
            return false;
        }
        return true;
    }
    bool CreateWallChildren(Vector3 pos) //分岐した壁を作る
    {
        int rand = Random.Range(0, 4);
        DirectCreateWall(pos);
        if(rand >= 4 - stopCnt++) return false; //止まる　満たさなければ直進
        if (TouchGreatWall(pos)) return false;  //外壁に接したら止まる
        return true;
    }
    void DirectCreateWall(Vector3 pos)
    {
        if (!GameObject.Find("WallHost"))
        {
            GameObject go = new GameObject("WallHost");
        }
        GameObject host = GameObject.Find("WallHost");
        GameObject wall = Instantiate(wallPre);
        wall.transform.position = pos;
        wall.transform.SetParent(host.transform, true);
    }
    Direction GetRightNum(Direction dir) //現在の方向から見て右を返す
    {
        return (Direction)((int)(dir + 1) % 4);
    }
    Direction GetLeftNum(Direction dir) //現在の方向から見て左を返す
    {
        return (Direction)((int)(dir + 3) % 4);
    }
    Direction SetRight(ref Direction dir)
    {
        dir = (Direction)((int)(dir + 1) % 4);
        return dir;
    }
    bool TouchGreatWall(Vector3 pos) //外壁に接しているかを返す
    {
        if (pos.x <= -Right || pos.x >= Right) return true;
        if(pos.z <= -Top || pos.z >= Top) return true;
        return false;
    }
}
enum Direction { Top,  Right, Bottom, Left };
class Dir //列挙体と座標を関連付けるためのメソッドクラス
{   
    public static Vector3[] dirVec = new Vector3[4]
    {
        new Vector3(0, 0, 0.5f),
        new Vector3(0.5f, 0, 0),
        new Vector3(0, 0, -0.5f),
        new Vector3 (-0.5f, 0, 0)
    };
    public static Vector3 GetFront(Direction dir)
    {
        return dirVec[(int)dir];
    }
    public static Vector3 GetRight(Direction dir)
    {
        return dirVec[((int)dir + 1) % 4];
    }
    public static Vector3 GetLeft(Direction dir)
    {
        return dirVec[((int)dir + 3) % 4];
    }
}
class WallPosition //枝の開始位置と方向を管理するクラス
{
    public Vector3 position;
    public Direction dir;
    public WallPosition()
    {
        position = Vector3.zero;
        dir = Direction.Top;
    }
    public WallPosition(Vector3 pos, Direction d_)
    {
        position = pos;
        dir = d_;
    }
}