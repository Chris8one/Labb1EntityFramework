using Labb1EntityFramework.Data;
using Labb1EntityFramework.Models;
using Labb1EntityFramework.Handlers;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Labb1EntityFramework
{
    public class Program
    {
        private static string userFirstName;
        private static string userLastName;
        private static Employee loggedInUser;
        private static bool loggedIn = false;
        private static bool admin;
        private static bool exitApp;
        private static bool logInBool = true;

        static void Main(string[] args)
        {
            Run();
        }

        static void Run()
        {
            Load();
            do
            {
                do
                {
                    Console.Clear();
                    MainMenu();
                } while (loggedIn == false);
                if (exitApp != true)
                {
                    if (!admin) EmployeeMenu();
                    else AdminMenu();
                }
            } while (exitApp == false);
        }

        static void Load()
        {
            try
            {
                Console.WriteLine("Loading..\n");
                Console.WriteLine("Please wait..");
                employeeList = EmployeesList.Employees();
                leaveAppliesList = LeaveAppliesList.Applies();
            }

            catch
            {
                Console.WriteLine("Something went wrong here..");
            }
        }

        // The main menu where you can log in as an employee or an admin, or create a new user
        static void MainMenu()
        {
            string prompt = "Main Menu\n\n";
            string[] options =
                {
                    "Employee Menu",
                    "Create New Employee",
                    "Admin Menu",
                    "Exit"
                };
            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Login();
                    break;
                case 1:
                    CreateNewUser();
                    break;
                case 2:
                    AdminMenu();
                    break;
                case 3:
                    ExitApp();
                    break;
            }
        }

        static private List<Employee> employeeList = new List<Employee>();
        static private List<LeaveApply> leaveAppliesList = new List<LeaveApply>();
        static private List<LeaveApply> viewMonthlyApplies = new List<LeaveApply>();

        // The employee login page, the employees can login with their full name
        static void Login()
        {
            Console.Clear();
            Console.WriteLine("Employee Menu Login\n\n");
            Console.Write("Enter your first and last name: ");
            var splitName = Console.ReadLine();
            while (!splitName.Contains(" "))
            {
                Console.Write("Please enter your first and last name with a space inbetween (Ex. John Doe)");
                splitName = Console.ReadLine();
            }

            userFirstName = splitName.Split(' ')[0];
            userLastName = splitName.Split(' ')[1];
            List<Employee> loginUser = UserSearch.SearchUser(userFirstName, userLastName);

            if (loginUser.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("User doesn't exist!");
                Console.WriteLine("\n\nPress any key to go back to the main menu!");
                Console.ReadLine();
            }
            if (loginUser.Count != 0)
            {
                loggedInUser = loginUser[0];
                loggedIn = true;
                Console.Clear();
            }
        }

        // Creates and add a new user to the database and checks if the user exists in the database
        static void CreateNewUser()
        {
            Console.Clear();
            Console.WriteLine("Create New Employee\n\n");
            Console.WriteLine("Enter your first name: ");
            userFirstName = Console.ReadLine();
            Console.WriteLine("Enter your last name: ");
            userLastName = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Create New Employee\n\n");
            Console.WriteLine("Is this correct? Y/N");
            Console.WriteLine("New employee: " + userFirstName + " " + userLastName);
            string answer = Console.ReadLine().ToLower();

            while (!(answer == "y" || answer == "n"))
            {
                Console.WriteLine("Press Y or N to continue or cancel");
                answer = Console.ReadLine();
            }

            if (answer == "y")
            {
                List<Employee> loginUser = UserSearch.SearchUser(userFirstName, userLastName);

                if (loginUser.Count != 0)
                {
                    Console.Clear();
                    Console.WriteLine("Create New Employee\n\n");
                    Console.WriteLine($"Employee {userFirstName} {userLastName} already exists!");
                    Console.ReadLine();
                }

                if (loginUser.Count == 0)
                {
                    var newUser = new Employee
                    {
                        FirstName = userFirstName,
                        LastName = userLastName
                    };
                    Console.Clear();
                    AddUser.NewUser(newUser);
                    Console.WriteLine("Create New Employee\n\n");
                    Console.WriteLine($"Employee successfully added!\nPress any key to log in as {userFirstName} {userLastName}..");

                    loginUser = UserSearch.SearchUser(userFirstName, userLastName);
                    loggedInUser = loginUser[0];
                    loggedIn = true;
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            if (answer == "n")
            {
                Console.Clear();
                Console.WriteLine("Create New Employee\n\n");
                Console.WriteLine($"Creation of the employee {userFirstName} {userLastName} was canceled\nPress any key to get back to main menu");
                Console.ReadLine();
            }
        }

        // The menu for an admin, the admin can see leave applications for a given month
        static void AdminMenu()
        {
            do
            {
                Console.Clear();
                string prompt = "Admin Menu\n\n";
                string[] options =
                    {
                        "See all leave applications",
                        "See all leave applications for a given month",
                        "Search leave applications by first or last name",
                        "Log out",
                        "Exit program"
                    };
                Menu adminMenu = new Menu(prompt, options);
                int selectedIndex = adminMenu.Run();

                switch (selectedIndex)
                {
                    case 0:
                        Console.Clear();
                        ViewCreatedApplies();
                        break;
                    case 1:
                        Console.Clear();
                        ViewMonthlyResults();
                        break;
                    case 2:
                        Console.Clear();
                        ApplySearch();
                        break;
                    case 3:
                        Console.WriteLine("Logged out");
                        logInBool = false;
                        Console.Clear();
                        break;
                    case 4:
                        ExitApp();
                        break;
                }
            } while (logInBool == true);
        }

        static void ExitApp()
        {
            Console.Clear();
            Environment.Exit(0);
        }

        // Menu for a logged in employee
        static void EmployeeMenu()
        {
            do
            {
                string prompt = "Employee Menu\nLogged in as: " + userFirstName + " " + userLastName + "\n";
                string[] options =
                    {
                        "Create a leave application",
                        "See all leave application",
                        "Search leave applications by first or last name",
                        "Your leave application history",
                        "Log out",
                        "Exit program"
                    };
                Menu employeeMenu = new Menu(prompt, options);
                int selectedIndex = employeeMenu.Run();

                switch (selectedIndex)
                {
                    case 0:
                        Console.Clear();
                        AddNewLeaveApply();
                        break;
                    case 1:
                        Console.Clear();
                        ViewCreatedApplies();
                        break;
                    case 2:
                        Console.Clear();
                        ApplySearch();
                        break;
                    case 3:
                        Console.Clear();
                        ShowPersonalHistoryOfApplies();
                        break;
                    case 4:
                        Console.WriteLine("Logged out");
                        loggedIn = false;
                        loggedInUser = null;
                        logInBool = false;
                        Console.Clear();
                        break;
                    case 5:
                        ExitApp();
                        break;
                }
            } while (logInBool == true);
        }

        // Creates and adds a new leave application to the database connected to the logged in employee
        static void AddNewLeaveApply()
        {
            Console.WriteLine("Create A New Leave Applicaion\n");
            Console.WriteLine("Please enter your leave reason");
            Console.Write("(Ex. Parental leave, vacation, day off): ");
            string leaveType = Console.ReadLine();
            Console.Write("Enter the start date for your leave (Year/Month/Day): ");
            string splitStartDate = Console.ReadLine();

            DateTime startDate = DateConverter(splitStartDate);

            Console.Write("Enter the end date for your leave (Year/Month/Day): ");
            string splitEndDate = Console.ReadLine();

            DateTime endDate = DateConverter(splitEndDate);

            Console.Clear();
            Console.WriteLine("Create A New Leave Application");
            Console.WriteLine("Check that all information is correct: \n" +
                "\nName: " + userFirstName + " " + userLastName +
                "\nLeave type: " + leaveType +
                "\nStart of leave: " + startDate +
                "\nEnd of leave: " + endDate +
                "\nIs all the information correct? (Y/N)");
            bool confirmInfo = false;
            do
            {
                string choice = Console.ReadLine();
                if (choice.ToLower() == "y")
                {
                    Console.WriteLine("Submitting the application..");
                    NewLeaveApplies.NewLeaveApply(loggedInUser.EmployeeId, leaveType, startDate, endDate);
                    Console.WriteLine("Application successfully submitted, press any key to go back to the menu");
                    Console.ReadLine();
                    Console.Clear();
                    confirmInfo = true;
                    break;
                }
                if (choice.ToLower() == "n")
                {
                    Console.WriteLine("Application cancelled..");
                    Console.WriteLine("Press any key to go back to the menu");
                    Console.ReadLine();
                    Console.Clear();
                    confirmInfo = true;
                    break;
                }
                else
                {
                    Console.WriteLine("Please Enter Y/N or User");
                }

            } while (confirmInfo == false);

        }

        // Shows the logged in employees history of leave applications
        static void ShowPersonalHistoryOfApplies()
        {
            ViewApplies(loggedInUser);
            Console.ReadLine();
        }

        // Gets the employee data for a givem month
        static void GetListApplies(List<LeaveApply> applies)
        {
            Console.WriteLine("Employee Leave Info\n");
            foreach (var apply in applies)
            {
                List<Employee> employees = new List<Employee>();
                var searchUser = from employee in employeeList
                                 where employee.EmployeeId == apply.EmployeeId
                                 select employee;
                foreach (var employee in searchUser)
                {
                    employees.Add(employee);
                }

                Console.WriteLine("\nEmployee name: " + employees[0].FirstName + " " + employees[0].LastName);
                Console.WriteLine("Type of leave: " + apply.LeaveType);
                Console.WriteLine("Leave period: " + apply.LeaveStartDate + " - " + apply.LeaveEndDate);
                Console.WriteLine("Length of leave: " + (apply.LeaveEndDate - apply.LeaveStartDate).TotalDays);
                Console.WriteLine("Was submitted: " + apply.LeaveSubmit + "\n");
            }
            logInBool = true;
        }

        // Shows all leave applications for the employees in the database
        static void ViewCreatedApplies()
        {
            GetListApplies(leaveAppliesList);
            Console.ReadLine();
            Console.Clear();
        }

        // Gets all leave applications from a given month
        static List<LeaveApply> GetMonthlyApplies(int month)
        {
            List<LeaveApply> result = new List<LeaveApply>();

            var searchQuery = from apply in leaveAppliesList
                              where apply.LeaveStartDate.Month == month
                              select apply;

            if (searchQuery != null)
            {
                foreach (LeaveApply apply in searchQuery)
                {
                    result.Add(apply);
                }
            }

            return result;
        }

        // Shows total days off during a given month an employee has been 
        static void SearchEmployeeDaysMonthly(List<LeaveApply> monthlyApplies)
        {
            double totalDaysApplied = 0;
            List<int> monthlyEmployeeId = new List<int>();
            var monthlyEmployees = from vacapp in monthlyApplies
                                   select vacapp.EmployeeId;

            foreach (var employeeid in monthlyEmployees)
            {
                if (!monthlyEmployeeId.Contains(employeeid))
                {
                    monthlyEmployeeId.Add(employeeid);
                }
            }

            foreach (var employeeid in monthlyEmployeeId)
            {
                foreach (var vacapp in monthlyApplies)
                {
                    if (vacapp.EmployeeId == employeeid)
                    {
                        totalDaysApplied = totalDaysApplied + (vacapp.LeaveEndDate - vacapp.LeaveStartDate).TotalDays;
                    }
                }
                string firstname = null;
                string lastname = null;

                var employeesmonth = employeeList.Where(e => e.EmployeeId == employeeid).ToList();
                foreach (var employee in employeesmonth)
                {
                    firstname = employee.FirstName;
                    lastname = employee.LastName;
                }
                Console.WriteLine("\n" + firstname + " " + lastname);
                Console.WriteLine("Total days off this month: " + totalDaysApplied);
                totalDaysApplied = 0;
            }
        }

        // Menu over months to see specific leave applications for each month
        static void ViewMonthlyResults()
        {
            string prompt = "Choose a month\n\n";
            string[] options =
                {
                    "January",
                    "February",
                    "March",
                    "April",
                    "May",
                    "June",
                    "July",
                    "August",
                    "September",
                    "October",
                    "November",
                    "December",
                    "Back to admin menu"
                };
            Menu monthMenu = new Menu(prompt, options);
            int selectedIndex = monthMenu.Run();

            Console.Clear();
            switch (selectedIndex)
            {
                case 0:
                    Console.WriteLine("January");
                    viewMonthlyApplies = GetMonthlyApplies(1);
                    break;
                case 1:
                    Console.WriteLine("February");
                    viewMonthlyApplies = GetMonthlyApplies(2);
                    break;
                case 2:
                    Console.WriteLine("March");
                    viewMonthlyApplies = GetMonthlyApplies(3);
                    break;
                case 3:
                    Console.WriteLine("April");
                    viewMonthlyApplies = GetMonthlyApplies(4);
                    break;
                case 4:
                    Console.WriteLine("May");
                    viewMonthlyApplies = GetMonthlyApplies(5);
                    break;
                case 5:
                    Console.WriteLine("Juni");
                    viewMonthlyApplies = GetMonthlyApplies(6);
                    break;
                case 6:
                    Console.WriteLine("Juli");
                    viewMonthlyApplies = GetMonthlyApplies(7);
                    break;
                case 7:
                    Console.WriteLine("August");
                    viewMonthlyApplies = GetMonthlyApplies(8);
                    break;
                case 8:
                    Console.WriteLine("September");
                    viewMonthlyApplies = GetMonthlyApplies(9);
                    break;
                case 9:
                    Console.WriteLine("October");
                    viewMonthlyApplies = GetMonthlyApplies(10);
                    break;
                case 10:
                    Console.WriteLine("November");
                    viewMonthlyApplies = GetMonthlyApplies(11);
                    break;
                case 11:
                    Console.WriteLine("December");
                    viewMonthlyApplies = GetMonthlyApplies(12);
                    break;
                case 12:
                    AdminMenu();
                    break;
            }

            if (viewMonthlyApplies.Count != 0)
            {
                GetListApplies(viewMonthlyApplies);
                SearchEmployeeDaysMonthly(viewMonthlyApplies);
            }
            else
            {
                Console.WriteLine("\n\nNo data for this month");
            }
            Console.ReadLine();
        }

        // Shows the leave applications of employee
        static void ViewApplies(Employee employee)
        {
            var searchQuery = from apply in leaveAppliesList
                              where apply.EmployeeId == employee.EmployeeId
                              select apply;

            foreach (var result in searchQuery)
            {
                Console.WriteLine("\nEmployee name: " + employee.FirstName + " " + employee.LastName);
                Console.WriteLine("Type of leave: " + result.LeaveType);
                Console.WriteLine("Leave period: " + result.LeaveStartDate + " - " + result.LeaveEndDate);
                Console.WriteLine("Was submitted: " + result.LeaveSubmit + "\n");
            }
        }

        // Search by first name, last name or both to see leave applications for that employee
        static void ApplySearch()
        {
            List<Employee> employeeSearch = new List<Employee>();

            Console.WriteLine("Employee Leave Info");
            Console.WriteLine("Leave Applications Search\nEnter first name, last name or both\n");
            string userInput = Console.ReadLine();
            Console.Clear();
            if (userInput.Contains(" "))
            {

                string firstName = userInput.Split(' ')[0];
                string lastName = userInput.Split(' ')[1];

                var searchUser = from employee in employeeList
                                 where employee.FirstName.ToLower() == firstName.ToLower() && employee.LastName.ToLower() == lastName.ToLower()
                                 select employee;

                foreach (var result in searchUser)
                {
                    employeeSearch.Add(result);
                }

                if (employeeSearch.Count == 0)
                {
                    Console.WriteLine("Leave Applications Search\n");
                    Console.WriteLine($"\nNo employee with the name {firstName} {lastName} was found in the database");
                }
                else if (employeeSearch.Count != 0)
                {
                    ViewApplies(employeeSearch[0]);
                }
            }
            else
            {
                var searchUser = from employee in employeeList
                                 where employee.FirstName.ToLower() == userInput.ToLower() || employee.LastName.ToLower() == userInput.ToLower()
                                 select employee;

                foreach (var result in searchUser)
                {
                    employeeSearch.Add(result);
                }

                if (employeeSearch.Count == 0)
                {
                    Console.WriteLine("Leave Applications Search\n");
                    Console.WriteLine($"\nNo employee with the name {userInput} was found in the database");
                }
                else if (employeeSearch.Count != 0)
                {

                    for (int i = 0; i < employeeSearch.Count; i++)
                    {
                        ViewApplies(employeeSearch[i]);
                    }
                }
            }
            Console.ReadLine();
        }

        // Converts a string to datetime
        static DateTime DateConverter(string dateInput)
        {
            while (!dateInput.Contains("/"))
            {
                Console.WriteLine("Enter date as followed: year/month/day (2022/01/15)");
                dateInput = Console.ReadLine();
            }

            int year = int.Parse(dateInput.Split('/')[0]);
            int month = int.Parse(dateInput.Split('/')[1]);
            int day = int.Parse(dateInput.Split('/')[2]);

            if (year < 100)
            {
                year = year + 2000;
            }
            DateTime returnDate = new DateTime(year, month, day);
            return returnDate;
        }
    }
}