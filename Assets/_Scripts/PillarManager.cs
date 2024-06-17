using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CountingPrototype
{
    public class PillarManager : MonoBehaviour
    {

        [SerializeField] Pillar pillarPrefab = null;

        [SerializeField] Pillar tickTockPillarPrefabs = null;

        [SerializeField] Pillar fatPillarPrefabs = null;
        [SerializeField] Pillar triPillarPrefabs = null;

        [SerializeField] Pillar telePillarPrefabs = null;
        // [SerializeField] GameObject tickTockPillarPrefab = null;

        [SerializeField] Vector3 firstPos = new Vector3(0.5f, 2.5f, 0);
        [SerializeField] float spaceX = 1;
        [SerializeField] float spaceY = 1;

        [SerializeField] int maxRawAmount = 7;
        [SerializeField] int maxColumAmount = 7;
        [SerializeField] float rawOffset = 0.5f;


        Pillar[] pillars;

        List<Teleport> teleports;
        GameManager gameManager;

        void Awake()
        {

            // InitPillars();
            pillars = new Pillar[(maxRawAmount + 1) * maxRawAmount / 2]; //若为奇数，直接有个0.5

            Debug.Log("柱子数组长度" + pillars.Length);
        }
        void OnEnable()
        {
            Debug.Log("PillarManager 注册NextLevel");
            gameManager = GameObject.FindObjectOfType<GameManager>();
            gameManager.OnNextLevel += NewLevel;

        }

        void NewLevel()
        {
            Debug.Log("PillarManager 绘制NewLevel");

            for (var i = 0; i < pillars.Length; i++)
            {
                if (pillars[i] != null)
                    Destroy(pillars[i].gameObject);
            }
            teleports = null;
            InitPillars();
            ReplacePillarByLevelAndMode(gameManager.CurrentLevel, gameManager.CurrentMode);
        }



        private void ReplacePillarByLevelAndMode(int level, int modeIndex)
        {
            SpawnRandomPillar(level + modeIndex, fatPillarPrefabs);
            SpawnRandomPillar(level + modeIndex - 1, tickTockPillarPrefabs);

            if (level + modeIndex > 2)
            {
                SpawnRandomPillar(level + modeIndex - 1, telePillarPrefabs);
            }

        }

        /// <summary>
        /// 从左下角位置开始，生成柱子
        /// </summary>
        void InitPillars()
        {

            int currentColumPillarAmount = maxColumAmount;
            Vector3 firstPosTemp = firstPos;
            for (var i = 0; i < maxRawAmount; i++)
            {
                float yPos = firstPosTemp.y + spaceY * i;
                for (var j = 0; j < currentColumPillarAmount; j++)
                {
                    float xPos = firstPosTemp.x + spaceX * j;
                    Vector3 pillarPos = new Vector3(xPos, yPos, 0);
                    Pillar pillar = Instantiate(pillarPrefab, pillarPos, pillarPrefab.transform.rotation);
                    pillar.SetPillar((gameManager.CurrentMode + 1) / 2);
                    pillars[GetPillarIndex(currentColumPillarAmount, j)] = pillar;

                }

                currentColumPillarAmount--;
                if (currentColumPillarAmount == 0)
                {
                    break;
                }
                //每行首位平移
                firstPosTemp.x += rawOffset;
            }

            //入口处柱子
            pillars[pillars.Length - 1].GetComponent<Pillar>().SetBounceForce(1);
            pillars[pillars.Length - 1].tag = "Untagged";
        }

        private int GetPillarIndex(int currentColumPillarAmount, int ordinalInRaw)
        {
            int index = 0;
            for (var i = maxColumAmount; i > 0; i--)
            {
                if (currentColumPillarAmount < i)
                {
                    index += i;
                }
                else if (currentColumPillarAmount == i)
                {
                    index += ordinalInRaw;
                    break;
                }
            }
            return index;
        }

        void SpawnRandomPillar(int amountToSpawn, Pillar prefabToSpawn)
        {
            for (var i = 0; i < amountToSpawn; i++)
            {
                int index = GetNonSpecialIndex();
                ReplacePillar(prefabToSpawn, index);
            }

        }

        private void ReplacePillar(Pillar prefabToSpawn, int index)
        {
            Pillar newPillar = Instantiate(prefabToSpawn, pillars[index].transform.position, prefabToSpawn.transform.rotation);

            Destroy(pillars[index].gameObject);
            pillars[index] = newPillar;
        }

        void SpawnTriangleLoop()
        {
            //排除入口处的点的index
            int firstIndex = Random.Range(maxRawAmount, pillars.Length - 3);
            //  Debug.Log($"第一个点的index:{firstIndex}");

            //计算第一个点所在行数和行内index
            int tempIndex = firstIndex;
            int rawNum = maxRawAmount;
            int ordinalInRaw = 0;
            for (var i = maxRawAmount; i > 0; i--)
            {
                if (tempIndex < i) //在行内
                {
                    //第一个点
                    rawNum = i;  //所在行数
                    ordinalInRaw = tempIndex; //行内index
                    if (tempIndex == i - 1)
                    {//刚好在行尾

                        // SpawnTriangle(firstIndex - 1, rawNum);
                        SpawnInvertTriangle(firstIndex - 1, rawNum);

                    }
                    else
                    {
                        //SpawnTriangle(firstIndex, rawNum);
                        SpawnInvertTriangle(firstIndex, rawNum);
                    }
                    break;
                }
                else
                {
                    tempIndex -= i;
                }

            }

            // Debug.Log($"第一个点,所在行：{rawNum}，行内index(0起)：{ordinalInRaw}");

            //  SpawnTriangle(firstIndex, rawNum);

        }

        private void SpawnTriangle(int firstIndex, int rawNum)
        {
            ReplacePillar(triPillarPrefabs, firstIndex);
            ReplacePillar(triPillarPrefabs, firstIndex + 1);
            ReplacePillar(triPillarPrefabs, firstIndex + rawNum);

            // Transform A = pillars[firstIndex].transform;
            // Transform B = pillars[firstIndex + 1].transform;
            // Transform C = pillars[firstIndex + rawNum].transform;

            // StartCoroutine(TriangleLoop(A, B, C));
        }

        //生成倒三角
        private void SpawnInvertTriangle(int firstIndex, int rawNum)
        {
            ReplacePillar(triPillarPrefabs, firstIndex);
            ReplacePillar(triPillarPrefabs, firstIndex + 1);
            ReplacePillar(triPillarPrefabs, firstIndex - rawNum);

            Transform A = pillars[firstIndex].transform;
            Transform B = pillars[firstIndex + 1].transform;
            Transform C = pillars[firstIndex - rawNum].transform;

            StartCoroutine(TriangleLoop(A, B, C));
        }

        IEnumerator TriangleLoop(Transform A, Transform B, Transform C)
        {
            Vector3 originA = A.position;

            Vector3 originB = B.position;

            Vector3 originC = C.position;


            Vector3 dirAB = (originB - originA).normalized;

            Vector3 dirBC = (originC - originB).normalized;

            Vector3 dirCA = (originA - originC).normalized;


            yield return new WaitForEndOfFrame();
            while (true)
            {
                yield return new WaitForEndOfFrame();

                TranslateTriPoint(A, B, C,
                                 originA, originB, originC,
                                 dirAB, dirBC, dirCA);

                // if (Vector3.Distance(A.position, originA) < 0.1f)
                // {
                //     TranslateTriPoint(A, B, C,
                //                   originA, originB, originC,
                //                   dirAB, dirBC, dirCA);
                // }
                // else if (Vector3.Distance(A.position, originB) < 0.1f)
                // {
                //     TranslateTriPoint(C, A, B,
                //                    originA, originB, originC,
                //                    dirAB, dirBC, dirCA);
                // }
                // else if (Vector3.Distance(A.position, originC) < 0.1f)
                // {
                //     TranslateTriPoint(B, C, A,
                //                    originA, originB, originC,
                //                    dirAB, dirBC, dirCA);
                // }

            }
        }

        private void TranslateTriPoint(Transform A, Transform B, Transform C, Vector3 originA, Vector3 originB, Vector3 originC, Vector3 dirAB, Vector3 dirBC, Vector3 dirCA)
        {
            Debug.Log("A A" + Vector3.Distance(A.position, originA));
            Debug.Log("A B" + Vector3.Distance(A.position, originB));
            Debug.Log("A C" + Vector3.Distance(A.position, originC));

            A.Translate(dirAB * 1 * Time.deltaTime);
            B.Translate(dirBC * 1 * Time.deltaTime);
            C.Translate(dirCA * 1 * Time.deltaTime);

            // if (Vector3.Distance(A.position, originA) < 0.1f)
            // {
            //     A.Translate(dirAB * 1 * Time.deltaTime);
            //     B.Translate(dirBC * 1 * Time.deltaTime);
            //     C.Translate(dirCA * 1 * Time.deltaTime);
            // }
            // else if (Vector3.Distance(A.position, originB) < 0.1f)
            // {
            //     A.Translate(dirBC * 1 * Time.deltaTime);
            //     B.Translate(dirCA * 1 * Time.deltaTime);
            //     C.Translate(dirAB * 1 * Time.deltaTime);
            // }
            // else if (Vector3.Distance(A.position, originC) < 0.1f)
            // {
            //     A.Translate(dirCA * 1 * Time.deltaTime);
            //     B.Translate(dirAB * 1 * Time.deltaTime);
            //     C.Translate(dirBC * 1 * Time.deltaTime);
            // }
        }

        private int GetNonSpecialIndex()
        {
            //看门柱不能被替换
            int index = Random.Range(0, pillars.Length - 1);
            //100次随机
            for (var i = 0; i < 100; i++)
            {

                if (pillars[index].IsSpecial)
                {
                    index = Random.Range(0, pillars.Length - 1);
                }
                else
                {
                    break;
                }
            }
            return index;
        }

        public Vector3 GetRandomTelePos(Teleport starter)
        {
            if (teleports == null)
            {
                teleports = new List<Teleport>();
                for (var i = 0; i < pillars.Length; i++)
                {
                    Teleport teleport = pillars[i].GetComponent<Teleport>();
                    if (pillars[i].GetComponent<Teleport>())
                    {
                        teleports.Add(pillars[i].GetComponent<Teleport>());
                    }
                }
            }

            int randomIndex = Random.Range(0, teleports.Count);
            for (var i = 0; i < 10; i++)
            {
                if (teleports[randomIndex] != starter)
                {
                    return teleports[randomIndex].transform.position;
                }
                else
                {
                    randomIndex = Random.Range(0, teleports.Count);
                }
            }

            return starter.transform.position + Vector3.up * 0.5f;
        }
    }
}


