using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserManagementSystem
{
    public class Program
    {
        static string filePath = "users.xml";

        static void Main(string[] args)
        {
            List<User> users = LoadUsers();

            while (true)
            {
                Console.WriteLine("1. Реєстрація користувача");
                Console.WriteLine("2. Авторизація адміністратора");
                Console.WriteLine("3. Вийти");
                Console.Write("Виберіть опцію: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterUser(users);
                        break;
                    case "2":
                        AdminLogin(users);
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        break;
                }
            }
        }

        static void RegisterUser(List<User> users)
        {
            Console.Write("Введіть електронну пошту: ");
            string email = Console.ReadLine();

            if (IsUserExists(users, email))
            {
                Console.WriteLine("Користувач з такою поштою вже існує.");
                return;
            }

            Console.Write("Введіть пароль: ");
            string password = Console.ReadLine();

            Console.Write("Введіть повне ім'я: ");
            string fullName = Console.ReadLine();

            Console.Write("Введіть дату народження (рррр-мм-дд): ");
            string dateOfBirth = Console.ReadLine();

            Console.Write("Введіть номер телефону: ");
            string phoneNumber = Console.ReadLine();

            User newUser = new User(email, password, fullName, dateOfBirth, phoneNumber);
            users.Add(newUser);

            SaveUsers(users);

            Console.WriteLine("Користувач успішно зареєстрований.");
        }

        static void AdminLogin(List<User> users)
        {
            Console.Write("Введіть пароль адміністратора: ");
            string adminPassword = Console.ReadLine();

            if (adminPassword == "adminPassword")
            {
                Console.WriteLine("Список користувачів:");

                foreach (var user in users)
                {
                    Console.WriteLine($"Електронна пошта: {user.Email}, Повне ім'я: {user.FullName}, Дата народження: {user.DateOfBirth}, Номер телефону: {user.PhoneNumber}");
                }
            }
            else
            {
                Console.WriteLine("Невірний пароль адміністратора.");
            }
        }

        static List<User> LoadUsers()
        {
            List<User> users = new List<User>();

            if (File.Exists(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
                using (StreamReader reader = new StreamReader(filePath))
                {
                    users = (List<User>)serializer.Deserialize(reader);
                }
            }

            return users;
        }

        static void SaveUsers(List<User> users)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, users);
            }
        }

        static bool IsUserExists(List<User> users, string email)
        {
            foreach (var user in users)
            {
                if (user.Email == email)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }

        public User() { }

        public User(string email, string password, string fullName, string dateOfBirth, string phoneNumber)
        {
            Email = email;
            Password = password;
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
        }
    }
}
