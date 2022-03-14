//DERIVCO GRADUATE DEVELOPER ASSESSMENT 2022- RAYHAAN NAKOODA

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DerivcoAssessment_RayhaanNakooda
{
    class Program
    {

        static PlayerNames playerNames = new PlayerNames();
        static Worker worker = new Worker();

        static void Main(string[] args)
        {

            TextFileMethod();
           
            string matchingNumber = "";
            Console.WriteLine("\t\t\t\t**********************************************\n" + 
                              "\t\t\t\tGOOD MATCH PROGRAM created by Rayhaan Nakooda\n" +
                              "\t\t\t\t**********************************************\n");

            //Type your username and press enter
            Console.Write("Please enter the first name of the first player  : ");

            //Create a string variable and get user input from the keyboard and store it in the variable
            playerNames.PlayerOneName = Console.ReadLine();

            //Type your username and press enter
            Console.Write("Please enter the first name of the second player : ");

            //Create a string variable and get user input from the keyboard and store it in the variable
            playerNames.PlayerTwoName = Console.ReadLine();

            //Print the value of the variable (userName), which will display the input value
            Console.WriteLine("\n\nThe two players entered in the Good Match Program are " + playerNames.PlayerOneName + " and " + playerNames.PlayerTwoName);

            string fullword = playerNames.PlayerOneName + "matches" + playerNames.PlayerTwoName;

            //Call our method
            var numOfOccurences = GetNoOfOccurenceCharInString(fullword);

            Console.WriteLine("\n\nThe number of times a character occurs is shown below ");

            foreach (var key in numOfOccurences.Keys)
            {
                Console.Write($"{key}-{numOfOccurences[key]}  ");
                matchingNumber = matchingNumber + numOfOccurences[key];
            }

            Console.WriteLine("\n\n\nThe number of below is used to calculate the match percentage between the pair\n" + matchingNumber);
             
            //Call our sum method            
            Console.WriteLine(SumOfTheNumbers(matchingNumber));

        }


        public static Dictionary<char, int> GetNoOfOccurenceCharInString(string stringToCheck)
        {
            //Track the character and count in a Dictionary.
            Dictionary<char, int> map = new Dictionary<char, int>();

            //For a string like "Jack and Jill" it will store something like this in the memory
            // j =2, a = 2, c = 1, k = 1, " " = 2, n = 1, d = 1, i = 1, l = 2

            //Shift to Lower case and Loop through each character
            foreach (var c in stringToCheck.ToLower())
            {
                if (c == ' ') //If the current char is a space do not count;
                {
                    continue;
                }

                //Check if map already has the char, if yes use the count stored in the memory, else initialize to 0
                var count = map.ContainsKey(c) ? map[c] : 0;

                map[c] = count + 1; //Increment the count by 1
            }

            return map;
        }


        public static string SumOfTheNumbers(string number)
        {

            //Test to see if number has number of digits > 2 or no
            var result = number.Length > 2 ? "" : number;

            //Checks if the number length is not equal to 2
            while (number.Length != 2)
            {
                var l = number.Length;
                result = "";
                for (var i = 0; i < l / 2; i++)
                {
                    //Add the both start left and end right digits
                    result += (number[i] + number[l - 1 - i] - 2 * '0').ToString();
                }

                //Check if number of digit is odd or event
                if (l % 2 != 0)
                    result += number[l / 2];

                number = result;
            }

            //If Else statement to check if the results are greater than a certain number 
            if (Convert.ToInt32(result) > 80)
            {
                Console.WriteLine("\n\nThe final results are in and \n" + playerNames.PlayerOneName + " matches " + playerNames.PlayerTwoName + " " + result + "%, good match");
            }
            else
            {
                Console.WriteLine("\n\nThe final results are in and \n" + playerNames.PlayerOneName + " matches " + playerNames.PlayerTwoName + " " + result + "%");
            }

            return null;
        }



        public static string SumOfTheNumbersForTextFileEntries(string number)
        {

            //Test to see if number has number of digits > 2 or no
            var result = number.Length > 2 ? "" : number;

            //Checks if the number length is not equal to 2
            while (number.Length != 2)
            {
                var l = number.Length;
                result = "";
                for (var i = 0; i < l / 2; i++)
                {
                    //Add the both start left and end right digits
                    result += (number[i] + number[l - 1 - i] - 2 * '0').ToString();
                }

                //Check if number of digit is odd or event
                if (l % 2 != 0)
                    result += number[l / 2];

                number = result;
            }
          
            return result;
        }


        public static void TextFileMethod()
        {

            try
            {
                //Read the file
                var lines = File.ReadAllLines("Players.txt")
                    .Select(s => s.Split(','))
                    .ToArray();

                //Loops through the array and add the key/value pairs to the dictionary
                for (int i = 0; i < lines.Length; i++)
                {
                    //This if statement checks if the value after the , is equal to "f" or "m"
                    if (lines[i][1].Equals("f"))
                    {
                        worker.female[lines[i][0]] = lines[i][1]; //Adds the f entries from the textfile to the List in the worker class
                    }

                    else
                    {
                        worker.male[lines[i][0]] = lines[i][1]; //Adds the m entries from the textfile to the List in the worker class
                    }
                }

                //Loops through the 2 dictionaries which are located in the worker class
                foreach (KeyValuePair<string, string> entry in worker.male)
                {
                    foreach (KeyValuePair<string, string> entry1 in worker.female)
                    {
                        worker.listNames.Add(entry.Key + "matches" + entry1.Key); //Adds the keys of each dictionary to the List in the worker class
                    }
                }

                //Loops through the List 
                for (int i = 0; i < worker.listNames.Count(); i++)
                {
                    var numOfOccurences = GetNoOfOccurenceCharInString(worker.listNames[i]);

                    string matchingNumber = "";

                    foreach (var key in numOfOccurences.Keys)
                    {
                        matchingNumber = matchingNumber + numOfOccurences[key];
                    }

                    worker.listNumbers.Add(matchingNumber);
                }

                //This query merges the two Lists into a single List
                var matchesAndNumbers = worker.listNames.Zip(worker.listNumbers, (names, numbers) => new { List = names, Numbers = numbers });


                //StreamWriter to write into a textfile called output.txt
                using (StreamWriter writetext = new StreamWriter("output.txt"))
                {
                    //Loops through the single List and writes the results into the textfile
                    foreach (var result in matchesAndNumbers)
                    {
                        writetext.WriteLine(result.List + " - " + result.Numbers + " - " + SumOfTheNumbersForTextFileEntries(result.Numbers.ToString()) + "%\n");
                    }

                }
            }

            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message); //if you want to show the exception message
            }

        }


    }
}
