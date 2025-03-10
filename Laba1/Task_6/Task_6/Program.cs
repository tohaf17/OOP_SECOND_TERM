using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SortingVerificationSystem
{
    delegate double[] SortArr(double[] arr);
    class Program
    {
        static void Main()
        {
            CreateTest();
            var testFiles = Directory.GetFiles("TestArrays", "*.txt");
            int score = 0;
            foreach (var file in testFiles)
            {
                int tempScore = 0;
                Console.WriteLine("---------------------");
                double[] array = LoadArrayFromFile(file);
                Console.WriteLine(Path.GetFileName(file));
                Console.WriteLine("Shaker sort\t" + TestSortRes(array, ShakerSort, StudentShakerSort, out tempScore));
                score += tempScore;
                Console.WriteLine("Selection sort\t" + TestSortRes(array, SelectionSort, StudentSelectionSort, out tempScore));
                score += tempScore;
                Console.WriteLine($"Score: {score}");
                Console.WriteLine("---------------------");
            }

            Console.WriteLine($"Score: {score}");
        }

        static void CreateTest()
        {
            if (!Directory.Exists("TestArrays"))  
                Directory.CreateDirectory("TestArrays");
            CreateArrFile("TestArrays/rand.txt", () => CreateRandomArray(20000));
            CreateArrFile("TestArrays/rand2.txt", () => CreateRandomArray(20000));
            CreateArrFile("TestArrays/reversed.txt", () => CreateReversedArray(20000));
            CreateArrFile("TestArrays/reversed2.txt", () => CreateReversedArray(20000));
            CreateArrFile("TestArrays/sorted.txt", () => CreateSortedArray(20000));
            CreateArrFile("TestArrays/sorted2.txt", () => CreateSortedArray(20000));
        }

        static void CreateArrFile(string filePath, Func<double[]> generator)
        {
            double[] array = generator.Invoke();
            File.WriteAllText(filePath, string.Join(" ", array));
        }

        static double[] CreateRandomArray(int length)
        {
            Random random = new Random();
            double[] array = new double[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = random.Next(-10000, 10000);
            }
            return array;
        }

        static double[] CreateReversedArray(int length)
        {
            double[] array = CreateSortedArray(length);
            Array.Reverse(array);
            return array;
        }

        static double[] CreateSortedArray(int length)
        {
            Random random = new Random();
            double[] array = new double[length];
            array[0] = random.Next(int.MinValue / 2, int.MaxValue / 2);
            int delta = ((int)array[0] + int.MaxValue) / length;
            for (int i = 1; i < length; i++)
            {
                array[i] = array[i - 1] - delta;
            }
            return array;
        }

        static double[] LoadArrayFromFile(string filePath)
        {
            return Array.ConvertAll(File.ReadAllText(filePath).Split().ToArray(), double.Parse);
        }

        static string TestSortRes(double[] arr, SortArr ReferenceSort, SortArr StudentSort, out int score)
        {
            double[] refSortArr = (double[])arr.Clone();
            double[] studSortArr = (double[])arr.Clone();
            int timeRefSort = 0;
            int timeStudSort = 0;

            try
            {                
                timeStudSort = MeasureSortTime(StudentSort, studSortArr);      
            }
            catch 
            {
                score = 0;
                return "Runtime error (problem with student sort)";
            }

            try
            {
                timeRefSort = MeasureSortTime(ReferenceSort, refSortArr);
            }
            catch 
            {
                score = 0;
                return "Runtime error (server problem)";
            }

            if (CompareTimes(timeRefSort, timeStudSort) && CompareArr(refSortArr, studSortArr))
            {
                score = 10;
                return "Passed";
            }
            else
            {
                score = 0;
                return "Failed";
            }
        }

        static bool CompareArr(double[] arr1, double[] arr2)
        {
            if (arr1.Length != arr2.Length) return false;
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] != arr2[i]) return false;
            }
            return true;
        }

        static int MeasureSortTime(SortArr sort, double[] arr)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            arr = sort.Invoke(arr);
            stopwatch.Stop();
            return stopwatch.Elapsed.Milliseconds;
        }

        static bool CompareTimes(int teta, int tstud)
        {
            return Math.Max(0, teta / 5) <= tstud && tstud <= 5 * teta;
        }

        static double[] SelectionSort(double[] ex)
        {
            double[] array = new double[ex.Length];
            Array.Copy(ex, array, ex.Length);
            for (int i = 0; i < array.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[minIndex])
                        minIndex = j;
                }
                (array[i], array[minIndex]) = (array[minIndex], array[i]);
            }
            return array;
        }

        static double[] ShakerSort(double[] array)
        {
            bool swapped = true;
            int start = 0;
            int end = array.Length;

            while (swapped)
            {
                swapped = false;

                for (int i = start; i < end - 1; ++i)
                {
                    if (array[i] > array[i + 1])
                    {
                        (array[i], array[i + 1]) = (array[i + 1], array[i]);
                        swapped = true;
                    }
                }

                if (!swapped)
                    break;

                swapped = false;
                end = end - 1;
                for (int i = end - 1; i >= start; i--)
                {
                    if (array[i] > array[i + 1])
                    {
                        (array[i], array[i + 1]) = (array[i + 1], array[i]);
                        swapped = true;
                    }
                }

                start = start + 1;
            }
            return array;
        }

        static double[] StudentSelectionSort(double[] array)
        {
            double[] arr = new double[array.Length];
            Array.Copy(array, arr, array.Length);
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[minIndex])
                        minIndex = j;
                }
                (arr[i], arr[minIndex]) = (arr[minIndex], arr[i]);
            }
            return arr;
        }

        static double[] StudentShakerSort(double[] array)
        {
            bool swapped = true;
            int start = 0;
            int end = array.Length;

            while (swapped)
            {
                swapped = false;

                for (int i = start; i < end - 1; ++i)
                {
                    if (array[i] > array[i + 1])
                    {
                        (array[i], array[i + 1]) = (array[i + 1], array[i]);
                        swapped = true;
                    }
                }

                if (!swapped)
                    break;

                swapped = false;
                end = end - 1;
                for (int i = end - 1; i >= start; i--)
                {
                    if (array[i] > array[i + 1])
                    {
                        (array[i], array[i + 1]) = (array[i + 1], array[i]);
                        swapped = true;
                    }
                }

                start = start + 1;
            }
            return array;
        }
    }
}
