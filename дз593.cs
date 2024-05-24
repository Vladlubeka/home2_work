using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp60
{
    namespace TodoApp
    {
        public class Task
        {
            public string Title { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime DueDate { get; set; }
            public bool IsCompleted { get; set; }

            public Task(string title, DateTime dueDate)
            {
                Title = title;
                CreatedDate = DateTime.Now;
                DueDate = dueDate;
                IsCompleted = false;
            }

            public override string ToString()
            {
                return $"{Title} (Создано: {CreatedDate}, Срок: {DueDate}, Статус: {(IsCompleted ? "Выполнено" : "Не выполнено")})";
            }
        }

        public class Controller
        {
            private List<Task> tasks = new List<Task>();

            public void AddTask(string title, DateTime dueDate)
            {
                tasks.Add(new Task(title, dueDate));
                Console.WriteLine("Задача успешно добавлена.");
            }

            public void CompleteTask(int taskId)
            {
                if (taskId >= 0 && taskId < tasks.Count)
                {
                    tasks[taskId].IsCompleted = true;
                    Console.WriteLine("Задача отмечена как выполненная.");
                }
                else
                {
                    Console.WriteLine("Неверный ID задачи.");
                }
            }

            public void DeleteTask(int taskId)
            {
                if (taskId >= 0 && taskId < tasks.Count)
                {
                    tasks.RemoveAt(taskId);
                    Console.WriteLine("Задача успешно удалена.");
                }
                else
                {
                    Console.WriteLine("Неверный ID задачи.");
                }
            }

            public void ShowTasks()
            {
                if (tasks.Count == 0)
                {
                    Console.WriteLine("Нет доступных задач.");
                    return;
                }

                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"{i}. {tasks[i]}");
                }
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                Controller controller = new Controller();

                while (true)
                {
                    Console.WriteLine("\nМеню списка задач:");
                    Console.WriteLine("1. Показать задачи");
                    Console.WriteLine("2. Добавить новую задачу");
                    Console.WriteLine("3. Отметить задачу как выполненную");
                    Console.WriteLine("4. Удалить задачу");
                    Console.WriteLine("5. Выход");
                    Console.Write("Введите ваш выбор: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            controller.ShowTasks();
                            break;
                        case "2":
                            Console.Write("Введите название задачи: ");
                            string title = Console.ReadLine();
                            Console.Write("Введите срок выполнения (гггг-мм-дд): ");
                            DateTime dueDate;
                            if (DateTime.TryParse(Console.ReadLine(), out dueDate))
                            {
                                controller.AddTask(title, dueDate);
                            }
                            else
                            {
                                Console.WriteLine("Неверный формат даты.");
                            }
                            break;
                        case "3":
                            Console.Write("Введите ID задачи для выполнения: ");
                            int completeId;
                            if (int.TryParse(Console.ReadLine(), out completeId))
                            {
                                controller.CompleteTask(completeId);
                            }
                            else
                            {
                                Console.WriteLine("Неверный формат ID.");
                            }
                            break;
                        case "4":
                            Console.Write("Введите ID задачи для удаления: ");
                            int deleteId;
                            if (int.TryParse(Console.ReadLine(), out deleteId))
                            {
                                controller.DeleteTask(deleteId);
                            }
                            else
                            {
                                Console.WriteLine("Неверный формат ID.");
                            }
                            break;
                        case "5":
                            return;
                        default:
                            Console.WriteLine("Неверный выбор. Пожалуйста,попробуйте снова .");
                            break;
                    }
                }
            }
        }
    }
}
