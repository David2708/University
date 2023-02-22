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

        //paging
        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resulstPerPage)
        {
            int startIndex = (pageNumber - 1) * resulstPerPage;
            return collection.Skip(startIndex).Take(resulstPerPage);
        }

        // variables
        static public void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var aboveAverage = from number in numbers
                               let average = numbers.Average()
                               let nSquared = Math.Pow(number, 2)
                               where nSquared > average
                               select number;

            Console.WriteLine("Average: {0}", numbers.Average());

            foreach(int number in aboveAverage)
            {
                Console.WriteLine("Number {0} Squared: {1}", number, Math.Pow(number, 2));
            }
        }

        // ZIP
        static public void ZippLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringNumbers = { "One", "Two", "Three", "Four", "Five" };

            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => number + " = " + word); 
            // { "1 = One" , "2 = Two" , ...}
        }


        // Repeat & Range
        static public void repeatRangeLinq()
        {
            // Generic a collection from 1 - 1000 ---> Range
            IEnumerable<int> fisrt1000 = Enumerable.Range(0, 1000);

            // Repeat a value N times
            var fiveXs = Enumerable.Repeat("X", 5); // {"x","x","x","x","x"}
        }

        static public void studentsLinq()
        {
            var classRoom = new[]
            {
                new Student
                {
                    Id=1,
                    Name = "David",
                    Grade = 90,
                    Certified= true,
                },
                new Student
                {
                    Id=2,
                    Name = "Juan",
                    Grade = 50,
                    Certified= true,
                },
                new Student
                {
                    Id=3,
                    Name = "Ana",
                    Grade = 96,
                    Certified= true,
                },
                new Student
                {
                    Id=4,
                    Name = "Álvaro",
                    Grade = 10,
                    Certified= false,
                },
                new Student
                {
                    Id=5,
                    Name = "Pedro",
                    Grade = 50,
                    Certified= true,
                }
            };

            var certifiedStudents = from student in classRoom
                                    where student.Certified
                                    select student;

            var notCertifiedStudents = from student in classRoom
                                    where student.Certified == false
                                    select student;

            var approvedStudentsNames = from student in classRoom
                                   where student.Grade >= 50 && student.Certified
                                   select student.Name;
        }


        // ALL
        static public void AllLinq()
        {
            var numbers = new List<int>() { 1,2,3,4,5 };
            bool allAreSamallerThan10 = numbers.All(x => x < 10); // true
            bool allAreBiggerOrEqualThan10 = numbers.All(x => x >= 2); // false

            var emtyList = new List<int>();
            bool allAreAfreGReaterThan0 = numbers.All(x => x < 10); // true


        }

        // Agregate
        static public void aggregateQueries()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //sum all numbers

            int sum = numbers.Aggregate((prevSum, current) => prevSum + current);
            // 0, 1 => 1
            // 1, 2 => 3
            // 3, 4 => 7

            string[] words = { "hello", "my", "name", "is", "David" }; // hello, my name is David
            string greeting = words.Aggregate((prevGreeting,current) => prevGreeting + current);
            // "","Heloo" => hello,
            // "hello" , "my" => hello, my
            // "my", "name => hello, my name ...
        }

        // Disctinct
        static public void distinctValues()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };

            IEnumerable<int> distinctVaues = numbers.Distinct();
        }

        // GroupBy
        static public void groupByExamples()
        {
            List<int> numbers = new List<int>() { 1,2,3,4,5,6,7,8,9 };

            //obtain only even numers and generate two groups
            var grouped = numbers.GroupBy(x => x % 2 == 0);

            // we will have two groups:
            // 1. The group that doesnt fit the condition (odd numbers)
            // 2. The group that fits the condition (even numbers)

            foreach (var group in grouped)
            {
                foreach(var value in group)
                {
                    Console.WriteLine(value); // 1,3,5,7,8,9 ... 2,4,6,8 (first the odds and then the even)
                }
            }

            // Another example
            var classRoom = new[]
            {
                new Student
                {
                    Id=1,
                    Name = "David",
                    Grade = 90,
                    Certified= true,
                },
                new Student
                {
                    Id=2,
                    Name = "Juan",
                    Grade = 50,
                    Certified= true,
                },
                new Student
                {
                    Id=3,
                    Name = "Ana",
                    Grade = 96,
                    Certified= true,
                },
                new Student
                {
                    Id=4,
                    Name = "Álvaro",
                    Grade = 10,
                    Certified= false,
                },
                new Student
                {
                    Id=5,
                    Name = "Pedro",
                    Grade = 50,
                    Certified= true,
                }
            };

            var certifiedQuery = classRoom.GroupBy(student => student.Certified && student.Grade >= 50);

            // we obtain two groups
            // 1. Not certified students
            // 2. Certified Students

            foreach (var group in certifiedQuery)
            {
                Console.WriteLine("--------------- {0} -----------", group.Key);
                foreach (var student in group)
                {
                    Console.WriteLine(student.Name); 
                }
            }
        }

        static public void relationsLinq()
        {
            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                Id = 1,
                Title = "My first post",
                Content = "My first content",
                Created = DateTime.Now,
                Comments = new List<Comment>()
                    {
                        new Comment
                        {
                            Id = 1,
                            Created = DateTime.Now,
                            Title = "My first comment",
                            Content = "My comment",

                        },
                        new Comment
                        {
                            Id = 2,
                            Created = DateTime.Now,
                            Title = "My secong comment",
                            Content = "My other comment",

                        }
                    }
                },
                new Post()
                {
                Id = 2,
                Title = "My second post",
                Content = "My second content",
                Created = DateTime.Now,
                Comments = new List<Comment>()
                    {
                        new Comment
                        {
                            Id = 4,
                            Created = DateTime.Now,
                            Title = "My four comment",
                            Content = "My new comment",

                        },
                        new Comment
                        {
                            Id = 2,
                            Created = DateTime.Now,
                            Title = "My other new comment",
                            Content = "My new  comment dos",

                        }
                    }
                }
            };

            var commentsWithContent = posts.SelectMany(
                post => post.Comments, (post, comment) => 
                new { PostId = post.Id, CommenContent = comment.Content });

        }

    }

}