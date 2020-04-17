//Program to find the smallest number from the inputs provided by the user
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PRAM
{
    public class subset
    {
        public int index1 = 0;
        public int Index1Value = 0;
        public int Index2 = 0;
        public int Index2Value = 0;
    }
    public class Program
    {
        public static int[] Status;
        public static int NuOfThread;
        public static int StatusThreadCompleteCount = 0;
        public static int[] items;
        static void Main(string[] args)
        {
            int n;
            Console.WriteLine("Enter the Number of items From which smallest number is to computed:");
            n = Convert.ToInt32(Console.ReadLine());
            items = new int[n];
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Enter item no:" + i + ":");
                items[i] = Convert.ToInt32(Console.ReadLine());
            }
            NuOfThread = (n * (n - 1)) / 2;
            subset[] subsetList = new subset[NuOfThread];
            Status = new int[n];
            for (int i = 0; i < NuOfThread; i++)
            {
                subsetList[i] = new subset();
            }

            int k = 0;
            Console.WriteLine("Subset List");
            for (int j = 0; j < n; j++)
            {
                for (int i = j + 1; i < n; i++)
                {
                    subsetList[k].index1 = j;
                    subsetList[k].Index1Value = items[j];
                    subsetList[k].Index2 = i;
                    subsetList[k].Index2Value = items[i];
                    Console.WriteLine("(" + subsetList[k].index1 + "," + subsetList[k].Index2 + ")");
                    k++;
                }
            }
            foreach (var item in subsetList)
            {
                Thread t1 = new Thread(delegate ()
                {
                    ReturnStatus(item);
                });
                t1.Start();
            }
            Console.ReadKey();

        }
        public static void ReturnStatus(subset singleItem)
        {
            if (singleItem.Index1Value > singleItem.Index2Value)
            {
                Status[singleItem.index1] = -1;
                Console.WriteLine("(" + singleItem.Index1Value + "," + singleItem.Index2Value + ")" + "Sends Negative Signal to Processor" + singleItem.index1);
                StatusThreadCompleteCount++;
                if (StatusThreadCompleteCount == NuOfThread)
                {
                    DisplaySmallestNo();
                }

            }
            else
            {
                Status[singleItem.Index2] = -1;
                Console.WriteLine("(" + singleItem.Index1Value + "," + singleItem.Index2Value + ")" + "Sends Negative Signal to Processor" + singleItem.Index2);
                Console.WriteLine("(" + singleItem.Index1Value + "," + singleItem.Index2Value + ")" + "Does not send Negative Signal to Processor" + singleItem.Index2);
                StatusThreadCompleteCount++;
                if (StatusThreadCompleteCount == NuOfThread)
                {
                    DisplaySmallestNo();
                }
            }
        }
        public static void DisplaySmallestNo()
        {
            for (int i = 0; i < Status.Length; i++)
            {
                if (Status[i] == 0)// here item indicates index
                {
                    Console.WriteLine("Processor P " + i + "doesn't receive the negative signal.");
                    Console.WriteLine("The Smallest Number is:" + items[i]);
                }
            }
        }
    }
}
