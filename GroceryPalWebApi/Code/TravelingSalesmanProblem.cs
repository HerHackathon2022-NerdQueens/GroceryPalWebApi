using System;
using System.Collections.Generic;

namespace GroceryPalWebApi.Code
{
    // https://www.geeksforgeeks.org/traveling-salesman-problem-tsp-implementation/
    public static class TravelingSalesmanProblem
    {
        public static List<int> Run(int V, int[,] graph, int s)
        {
            List<int> vertex = new List<int>();

            for (int i = 0; i < V; i++)
                if (i != s)
                    vertex.Add(i);

            int min_path_weight = Int32.MaxValue;
            List<int> min_path = new List<int>();

            do
            {
                int current_pathweight = 0;
                int k = s;

                List<int> curr_min_path = new List<int>();
                curr_min_path.Add(k);

                for (int i = 0; i < vertex.Count; i++)
                {
                    current_pathweight += graph[k, vertex[i]];
                    k = vertex[i];
                    curr_min_path.Add(k);
                }

                current_pathweight += graph[k, s];

                if (current_pathweight < min_path_weight)
                {
                    min_path_weight = current_pathweight;
                    min_path = curr_min_path;
                }

            } while (FindNextPermutation(vertex));

            return min_path;
        }

        // Function to swap the data resent in the left and right indices
        public static List<int> Swap(List<int> data, int left,
                                     int right)
        {
            int temp = data[left];
            data[left] = data[right];
            data[right] = temp;

            return data;
        }

        // Function to reverse the sub-array starting from left to the right both inclusive
        private static List<int> Reverse(List<int> data,
                                        int left, int right)
        {
            while (left < right)
            {
                int temp = data[left];
                data[left++] = data[right];
                data[right--] = temp;
            }

            return data;
        }

        // Function to find the next permutation of the given integer array
        private static bool FindNextPermutation(List<int> data)
        {
            if (data.Count <= 1)
                return false;
            int last = data.Count - 2;

            while (last >= 0)
            {
                if (data[last] < data[last + 1])
                    break;
                last--;
            }

            if (last < 0)
                return false;
            int nextGreater = data.Count - 1;

            for (int i = data.Count - 1; i > last; i--)
            {
                if (data[i] > data[last])
                {
                    nextGreater = i;
                    break;
                }
            }

            data = Swap(data, nextGreater, last);
            data = Reverse(data, last + 1, data.Count - 1);

            return true;
        }
    }
}
