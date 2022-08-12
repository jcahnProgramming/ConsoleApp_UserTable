using System;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json;

namespace TestApp
{
    internal class Program
    {

        public static List<User> users = new List<User>();
        public static bool killProgram = false;
        public string filePath = "db.json";

        static void Main(string[] args)
        {
            LoadData();
            while (killProgram == false)
            {
                string? userSelection;
                ShowMenu();
                userSelection = Console.ReadLine();
                ReturnSelection(userSelection);

                ShowMenu();
                userSelection = Console.ReadLine();
                ReturnSelection(userSelection);
            }

            if (killProgram == true)
            {
                SaveData();
                Environment.Exit(0);
            }
        }

        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the User Input System");
            Console.WriteLine("Please select from the following options");
            Console.WriteLine("1. Input New User");
            Console.WriteLine("2. Existing User Lookup");
            Console.WriteLine("3. Delete User");
            Console.WriteLine("4. List All Existing Users");
            Console.WriteLine("5. Exit Program");
        }

        static void NewUser()
        {
            Console.Clear();

            Console.WriteLine("Please type in a desired username: ");
            string? username = Console.ReadLine();
            NullCheck(username);
            while (DuplicateCheck(1, username) == true)
            {
                Console.WriteLine("That username is taken. \n Please type in another desired username: ");
                username = Console.ReadLine();
                username = NullCheck(username);
                DuplicateCheck(1, username);
            }

            Console.WriteLine("Please type in a desired password: ");
            string? password = Console.ReadLine();
            password = NullCheck(password);
            while (password == null)
            {
                Console.WriteLine("Please type in a valid password: ");
                password = Console.ReadLine();
                password = NullCheck(password);
            }

            Console.WriteLine("What is the users first name: ");
            string? firstName = Console.ReadLine();
            firstName = NullCheck(firstName);
            while (firstName == null)
            {
                Console.WriteLine("Please type in a valid first name: ");
                firstName = Console.ReadLine();
                firstName = NullCheck(firstName);
            }

            Console.WriteLine("What is the users last name: ");
            string? lastName = Console.ReadLine();
            NullCheck(lastName);
            while (lastName == null)
            {
                Console.WriteLine("Please type in a valid last name: ");
                lastName = Console.ReadLine();
                lastName = NullCheck(lastName);
            }

            Console.WriteLine("What is the users age: ");
            string? age = Console.ReadLine();
            while (age == null)
            {
                Console.WriteLine("Not valid age. \n Please type in a valid age: ");
                age = Console.ReadLine();
            }

            DateTime createdDate = DateTime.Now;

            User tmp = User.CreateUser(username, password, firstName, lastName, age, createdDate);

            users.Add(tmp);
        }

        static bool DuplicateCheck(int valueID, string? value)
        {
            switch (valueID)
            {

                case 1:
                    {
                        for (int i = 0; i < users.Count; i++)
                        {
                            if (value == users[i].username)
                            {
                                return true;
                            }
                        }
                    }
                    break;

                default:
                    {
                        return true;
                    }
            }
            return false;
        }

        static string? NullCheck(string? value)
        {
            while (value == null)
            {
                value = Console.ReadLine();
            }
            return value;
        }

        static void LookupUser(string username)
        {
            Console.Clear();
            bool foundUser = false;

            for (int i = 0; i < users.Count; i++)
            {
                if (username == users[i].username)
                {
                    foundUser = true;
                    Console.WriteLine("User Data Found:");
                    Console.WriteLine("Username: " + users[i].username);
                    Console.WriteLine("Password: " + users[i].password);
                    Console.WriteLine("First Name: " + users[i].firstName);
                    Console.WriteLine("Last Name: " + users[i].lastName);
                    Console.WriteLine("Age: " + users[i].age);
                    Console.WriteLine("Created Date: " + users[i].created.ToString());
                    Console.WriteLine();
                    Console.WriteLine("Press any key to exit to Main Menu...");
                    Console.ReadKey();
                    ShowMenu();
                }
                if (!foundUser)
                {
                    Console.WriteLine("No user with that username has been found...");
                    Console.ReadKey();
                    ShowMenu();
                }
            }
        }

        static void DeleteUser()
        {
            Console.Clear();

            Console.WriteLine("Please type in a username to delete: ");
            string? user = Console.ReadLine();
            user = NullCheck(user);

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].username == user)
                {
                    users.RemoveAt(i);
                    ShowMenu();
                }
            }
        }

        static void ListAllUsers()
        {
            Console.Clear();

            if (users.Count <= 0)
            {
                Console.WriteLine("No users currently exist, try creating some...");
                ShowMenu();
            }

            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine("------------------------------------------------------------------------------------------");
                Console.WriteLine($"ID: {users.Count} | Username: {users[i].username} | Password: {users[i].password}");
                Console.WriteLine($"First Name: {users[i].firstName} | Last Name: {users[i].lastName}");
                Console.WriteLine($"Age: {users[i].age} | Date Created: {users[i].created.ToString()}");
                Console.WriteLine("------------------------------------------------------------------------------------------");
                Console.WriteLine();
            }

            Console.ReadKey();
            ShowMenu();
        }

        static void ExitProgram()
        {
            killProgram = true;
        }

        static void ReturnSelection(string? _userSelection)
        {
            switch (_userSelection)
            {
                case "0":
                    {
                        ShowMenu();
                    }
                    break;
                case "1":
                    {
                        NewUser();
                    }
                    break;
                case "2":
                    {
                        Console.WriteLine("Please type in a username: ");
                        string? username = Console.ReadLine();
                        while (username == null)
                        {
                            Console.WriteLine("Please type in a username: ");
                            username = Console.ReadLine();
                        }
                        LookupUser(username);
                    }
                    break;
                case "3":
                    {
                        DeleteUser();
                    }
                    break;
                case "4":
                    {
                        ListAllUsers();
                    }
                    break;
                case "5":
                    {
                        ExitProgram();
                    }
                    break;

                default:
                    {
                        ShowMenu();
                    }
                    break;
            }
        }

        static void SaveData()
        {
            string jsonData = JsonConvert.SerializeObject(users);
            string fileName = "db.json";

            if (!File.Exists(fileName))
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.WriteLine(jsonData);
                }
            }
            if (File.Exists(fileName))
            {
                File.Delete(fileName);

                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.WriteLine(jsonData);
                }
            }
        }

        static void LoadData()
        {
            string curFile = "db.json";
            if (File.Exists(curFile))
            {
                using (StreamReader sr = new StreamReader(curFile))
                {
                    string content = sr.ReadToEnd();
                    users.Clear();

                    users = JsonConvert.DeserializeObject<List<User>>(content);
                }
            }
            
        }







    }




}


