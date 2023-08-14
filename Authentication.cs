using System;
using System.IO;

public class Users
{
    public void AddnewUser(string username, string role, string password)
    {
        string userLine = $"{username},{role},{password}";
        File.AppendAllText("Data/users.txt", userLine + Environment.NewLine);
    }

    public string UserLogin(string username, string password, out string role)
    {
        role = "";

        foreach (string line in File.ReadLines("Data/users.txt"))
        {
            string[] userFields = line.Split(',');
            if (userFields.Length == 3 && userFields[0] == username && userFields[2] == password)
            {
                role = userFields[1];
                return role;
            }
        }
        return role;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Users users = new Users();

        while (true)
        {
            Console.WriteLine("Welcome to The Jitu");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter username: ");
                    string username = Console.ReadLine();
                    Console.Write("Enter password: ");
                    string password = Console.ReadLine();

                    string role;
                    string authenticatedRole = users.UserLogin(username, password, out role);

                    if (!string.IsNullOrEmpty(authenticatedRole))
                    {
                        Console.WriteLine($"Welcome, {username}!");
                        if (authenticatedRole == "Admin")
                        {
                            Console.WriteLine("Admin login successful");

                        }
                        else if (authenticatedRole == "User")
                        {
                            Console.WriteLine("User login successful");

                        }
                    }
                    else
                    {
                        Console.WriteLine("Login failed");
                    }
                    break;

                case 2:
                    Console.WriteLine("Register as:");
                    Console.WriteLine("1. User");
                    Console.WriteLine("2. Admin");
                    Console.Write("Enter your choice: ");
                    int registerChoice = int.Parse(Console.ReadLine());

                    Console.Write("Enter a new username: ");
                    string newUsername = Console.ReadLine();
                    Console.Write("Enter a new password: ");
                    string newPassword = Console.ReadLine();

                    string registerRole = (registerChoice == 2) ? "Admin" : "User";
                    users.AddnewUser(newUsername, registerRole, newPassword);
                    Console.WriteLine($"{registerRole} registration successful");
                    break;

                case 3:
                    Console.WriteLine("Exiting...");
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }

            Console.WriteLine();
        }
    }
}