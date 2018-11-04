﻿using Hash_Table;
using System;

class Example
{
    static void Main()
    {
        var hardCode = "SoftUni rocks";
        var dict = new MyDictionary<Char, int>();
        var chars = hardCode.ToCharArray();

        foreach (var c in chars)
        {
            if (!dict.ContainsKey(c))
            {
                dict[c] = 0;
            }

            dict[c] += 1;
        }

        foreach (var kvp in dict)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value} time/s");
        }

        //HashTable<string, int> grades = new HashTable<string, int>();

        //Console.WriteLine("Grades:" + string.Join(",", grades));
        //Console.WriteLine("--------------------");

        //grades.Add("Peter", 3);
        //grades.Add("Maria", 6);
        //grades["George"] = 5;
        //foreach (var grade in grades)
        //{
        //    Console.WriteLine($"{grade.Key}: {grade.Value}");
        //}

        //Console.WriteLine("Grades:" + string.Join(",", grades));
        //Console.WriteLine("--------------------");

        //grades.AddOrReplace("Peter", 33);
        //grades.AddOrReplace("Tanya", 4);
        //grades["George"] = 55;
        //Console.WriteLine("Grades:" + string.Join(",", grades));
        //Console.WriteLine("--------------------");

        //Console.WriteLine("Keys: " + string.Join(", ", grades.Keys));
        //Console.WriteLine("Values: " + string.Join(", ", grades.Values));
        //Console.WriteLine("Count = " + string.Join(", ", grades.Count));
        //Console.WriteLine("--------------------");

        //grades.Remove("Peter");
        //grades.Remove("George");
        //grades.Remove("George");
        //Console.WriteLine("Grades:" + string.Join(",", grades));
        //Console.WriteLine("--------------------");

        //Console.WriteLine("ContainsKey[\"Tanya\"] = " + grades.ContainsKey("Tanya"));
        //Console.WriteLine("ContainsKey[\"George\"] = " + grades.ContainsKey("George"));
        //Console.WriteLine("Grades[\"Tanya\"] = " + grades["Tanya"]);
        //Console.WriteLine("--------------------");
    }
}
