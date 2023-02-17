using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LinqSnippers
{
    public class Snippers
    {
        static public void BasicLinQ()
        {
            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Sear león"
            };

            // 1. SELECT * of cars
            var carList = from car in cars select car;

            foreach (var car in carList)
            {
                Console.WriteLine(car);
            }

            // 2. SELECT WHERE car is AUDI
            var audiList = from car in cars where car.Contains("Audi") select car;

            foreach (var audi in audiList)
            {
                Console.WriteLine(audi);
            }
        }

        //Number Examples
        static public void LinqNumbers()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

            //Each Number multiplied by 3
            // take all numbers, but 9
            //order ascending

            var processedNumberList =
                numbers
                .Select(num => num * 3)
                .Where(num => num != 9)
                .OrderBy(num => num);
        }

        static public void SearchExamples()
        {
            List<string> texList = new List<string>()
            {
                "a",
                "bx",
                "c",
                "d",
                "e",
                "cj",
                "f",
                "c"
            };

            //1. First of all elements
            var first = texList.First();

            //2. First element that is "c"
            var cFirst = texList.First(text => text.Equals("c"));

            //3. First element that contains "j"
            var containJ = texList.First(text => text.Contains("j"));

            //4. First element that contains Z or default
            var firstOrDefault = texList.FirstOrDefault(text => text.Contains("z")); // "" or element that contains "z"

            //5. Last element that contains Z or default
            var LastOrDefault = texList.LastOrDefault(text => text.Contains("z")); // "" or element that contains "z"

            //6. Single values
            var uniquetexts = texList.Single();
            var uniqueorDefaultTexts = texList.SingleOrDefault();

            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 6 };

            // obtain {4,8}
            var myEventNumbers = evenNumbers.Except(otherEvenNumbers); // {4,8}

        }

        static public void MultipleSlects()
        {
            // SELECT MANY
            string[] myOptions =
            {
                "Opinión 1", "text 1",
                "Opinión 2", "text 2",
                "Opinión 3", "text 3"
            };

            var myOpinionSelection = myOptions.SelectMany(opinion => opinion.Split(","));


            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id= 1,
                    Name = "Enterpise 1",
                    Employees = new[]
                    {
                        new Employee()
                        {
                            Id= 1,
                            Name="David",
                            Email="David@gmail.com",
                            Salary= 3000
                        },
                        new Employee()
                        {
                            Id= 2,
                            Name="Pepe",
                            Email="pepe@gmail.com",
                            Salary= 1000
                        },
                        new Employee()
                        {
                            Id= 3,
                            Name="Juanjo",
                            Email="juanjo@gmail.com",
                            Salary= 3000
                        }
                    }
                },
                 new Enterprise()
                {
                    Id= 2,
                    Name = "Enterpise 2",
                    Employees = new[]
                    {
                        new Employee()
                        {
                            Id= 4,
                            Name="Ana",
                            Email="Ana@gmail.com",
                            Salary= 3000
                        },
                        new Employee()
                        {
                            Id= 5,
                            Name="Maria",
                            Email="Maria@gmail.com",
                            Salary= 3000
                        },
                        new Employee()
                        {
                            Id= 6,
                            Name="Marta",
                            Email="Mata@gmail.com",
                            Salary= 4000
                        }
                    }
                }
            };

            //obtain all empleyees of all Enterpises
            var employeeList = enterprises.SelectMany(enterprise => enterprise.Employees);

            // Know if any list is empty
            bool hastEnterprises = enterprises.Any();

            bool hasEmpleoyees = enterprises.Any(enterprise => enterprise.Employees.Any());

            // All enterprises at least has an employee wit more than 1000$ of salary
            var hasEmployeeWithSalaryMoreThatOrEquals1000 =
                enterprises.Any(enterprise =>
                enterprise.Employees.Any(employee => employee.Salary > 1000));

        }

        static void linqCollections()
        {
            var firstList = new List<string> { "a", "b", "c" };
            var secondList = new List<string> { "a", "c", "d" };

            // INNER JOIN

            var comminResult = from element in firstList
                               join secondElement in secondList
                               on element equals secondElement
                               select new { element, secondElement };

            var commonResult2 = firstList.Join(
                secondList,
                element => element,
                secondElement => secondElement,
                (element, secondElement) => new { element, secondElement }
            );


            // OUTER JOIN - LEFT
            var leftOutherJoin = from element in firstList
                                 join secondElement in secondList
                                 on element equals secondElement into temporalList
                                 from temporalElement in temporalList.DefaultIfEmpty()
                                 where element != temporalElement
                                 select new { Element = element };

            var leftOutherJoin2 = from element in firstList
                                  from secondElement in secondList.DefaultIfEmpty()
                                  select new
                                  {
                                      Element = element,
                                      SecondElement = secondElement
                                  };


            // OUTER JOIN - RIGHT
            var rightOutherJoin = from secondElement in secondList
                                  join element in firstList
                                  on secondElement equals element into temporalList
                                  from temporalElement in temporalList.DefaultIfEmpty()
                                  where secondElement != temporalElement
                                  select new { Element = secondElement };

            // UNION
            var unionList = leftOutherJoin.Union(rightOutherJoin);
        }


        static public void SkipTakeLinq()
        {
            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9,10
            };

            // SKIP saltar numeros (no tomarlos)

            var skipTwoFirstValues = myList.Skip(2); // { 3,4,5,6,7,8,9,10 }

            var skipLastTwoVAlues = myList.SkipLast(2); // { 1,2,3,4,5,6,7,8 }

            var skipWhileSmallerThan4 = myList.SkipWhile(s => s < 4); // { 5,6,7,8,9,10 }

            // TAKE tomar numeros

            var takeTwoFirstValues = myList.Take(2); // { 1,2 }

            var takeLastTwoVAlues = myList.TakeLast(2); // { 9,10 }

            var TakeWhileSmallerThan4 = myList.TakeWhile(s => s < 4); // { 1,2,3,4 }
        }

        // TODO:

        // variables

        // ZIP

        // Repeat

        // ALL

        // Agregate

        // Disctinct

        // GroupBy

    }

}