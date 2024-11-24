using System;
using System.Collections.Generic;

class FenwickTreeRandomPicker
{
    // 1부터 N까지의 숫자 중 count개의 난수를 반환
    public static List<int> GetRandomNumbers(int N, int count,int seed)
    {
        // 팬윅 트리 초기화
        int[] tree = new int[N + 1];
        for (int i = 1; i <= N; i++)
        {
            Update(tree, i, 1, N);
        }

        List<int> result = new List<int>();
        Random random = new Random(seed);
        for (int i = 0; i < count; i++)
        {
            // 1부터 남아 있는 총 숫자 개수 사이에서 랜덤 값 선택
            int rnd = random.Next(1, PrefixSum(tree, N) + 1);

            // k번째 숫자 찾기
            int selected = FindKth(tree, rnd, N);
            result.Add(selected);

            // 선택된 숫자 제거
            Update(tree, selected, -1, N);
        }

        return result;
    }

    // 팬윅 트리에 값 추가/제거
    private static void Update(int[] tree, int index, int delta, int size)
    {
        while (index <= size)
        {
            tree[index] += delta;
            index += index & -index; // 상위 노드로 이동
        }
    }

    // 1부터 index까지의 누적 합 계산
    private static int PrefixSum(int[] tree, int index)
    {
        int sum = 0;
        while (index > 0)
        {
            sum += tree[index];
            index -= index & -index; // 상위 노드로 이동
        }
        return sum;
    }

    // k번째 남아 있는 숫자 찾기
    private static int FindKth(int[] tree, int k, int size)
    {
        int low = 1, high = size, result = -1;
        while (low <= high)
        {
            int mid = (low + high) / 2;
            if (PrefixSum(tree, mid) >= k)
            {
                result = mid;
                high = mid - 1;
            }
            else
            {
                low = mid + 1;
            }
        }
        return result;
    }
}