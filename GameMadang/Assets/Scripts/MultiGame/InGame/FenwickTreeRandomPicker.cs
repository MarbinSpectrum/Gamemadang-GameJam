using System;
using System.Collections.Generic;

class FenwickTreeRandomPicker
{
    // 1���� N������ ���� �� count���� ������ ��ȯ
    public static List<int> GetRandomNumbers(int N, int count,int seed)
    {
        // ���� Ʈ�� �ʱ�ȭ
        int[] tree = new int[N + 1];
        for (int i = 1; i <= N; i++)
        {
            Update(tree, i, 1, N);
        }

        List<int> result = new List<int>();
        Random random = new Random(seed);
        for (int i = 0; i < count; i++)
        {
            // 1���� ���� �ִ� �� ���� ���� ���̿��� ���� �� ����
            int rnd = random.Next(1, PrefixSum(tree, N) + 1);

            // k��° ���� ã��
            int selected = FindKth(tree, rnd, N);
            result.Add(selected);

            // ���õ� ���� ����
            Update(tree, selected, -1, N);
        }

        return result;
    }

    // ���� Ʈ���� �� �߰�/����
    private static void Update(int[] tree, int index, int delta, int size)
    {
        while (index <= size)
        {
            tree[index] += delta;
            index += index & -index; // ���� ���� �̵�
        }
    }

    // 1���� index������ ���� �� ���
    private static int PrefixSum(int[] tree, int index)
    {
        int sum = 0;
        while (index > 0)
        {
            sum += tree[index];
            index -= index & -index; // ���� ���� �̵�
        }
        return sum;
    }

    // k��° ���� �ִ� ���� ã��
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