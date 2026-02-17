using System;
using System.Collections.Generic;

class PartitionProblem
{
    static bool CanPartition(int[] nums)
    {
        int sum = 0;
        foreach (int num in nums)
            sum += num;

        if (sum % 2 != 0)
            return false;

        int target = sum / 2;
        int n = nums.Length;

        bool[,] dp = new bool[n + 1, target + 1];

        for (int i = 0; i <= n; i++)
            dp[i, 0] = true;

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= target; j++)
            {
                if (nums[i - 1] <= j)
                    dp[i, j] = dp[i - 1, j] || dp[i - 1, j - nums[i - 1]];
                else
                    dp[i, j] = dp[i - 1, j];
            }
        }

        return dp[n, target];
    }

    static List<List<int>>? GetPartitionSubsets(int[] nums)
    {
        int sum = 0;
        foreach (int num in nums)
            sum += num;

        if (sum % 2 != 0)
            return null;

        int target = sum / 2;
        int n = nums.Length;

        bool[,] dp = new bool[n + 1, target + 1];
        for (int i = 0; i <= n; i++)
            dp[i, 0] = true;

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= target; j++)
            {
                if (nums[i - 1] <= j)
                    dp[i, j] = dp[i - 1, j] || dp[i - 1, j - nums[i - 1]];
                else
                    dp[i, j] = dp[i - 1, j];
            }
        }

        if (!dp[n, target])
            return null;

        List<int> subset1 = new List<int>();
        List<int> subset2 = new List<int>();
        int remainingSum = target;

        for (int i = n; i > 0 && remainingSum > 0; i--)
        {
            if (!dp[i - 1, remainingSum])
            {
                subset1.Add(nums[i - 1]);
                remainingSum -= nums[i - 1];
            }
            else
            {
                subset2.Add(nums[i - 1]);
            }
        }

        for (int i = 0; i < n - subset1.Count - subset2.Count; i++)
            subset2.Add(0);

        return new List<List<int>> { subset1, subset2 };
    }

    static void Main()
    {
        int[] numbers = { 1, 5, 11, 5 };

        if (CanPartition(numbers))
        {
            Console.WriteLine("Разбиение возможно.");
            var subsets = GetPartitionSubsets(numbers);
            if (subsets != null)
            {
                Console.WriteLine($"Первая группа: {{{string.Join(", ", subsets[0])}}}");
                Console.WriteLine($"Вторая группа: {{{string.Join(", ", subsets[1])}}}");
            }
        }
        else
        {
            Console.WriteLine("Разбиение невозможно.");
        }
    }
}