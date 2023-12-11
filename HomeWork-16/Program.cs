using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;


namespace HomeWork_16
{
    public class Program
    {
        /// <summary>
        ///     1. Просмотр Содержимого Директории:
        ///     - Приложение должно запрашивать у пользователя путь
        ///     к директории и отображать список всех файлов и поддиректорий в этой директории.
        /// </summary>
        static void Main(string[] args)
        {
            Console.Write("Введите путь к директории: ");
            string path = Console.ReadLine();

            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path);
                string[] directories = Directory.GetDirectories(path);

                Console.WriteLine("\nФайлы:");
                foreach (var file in files)
                    Console.WriteLine(Path.GetFileName(file));

                Console.WriteLine("\nДиректории:");
                foreach (var directory in directories)
                    Console.WriteLine(Path.GetFileName(directory));
            }
            else
            {
                Console.WriteLine("Директория не существует.");
            }

            /// <summary>
            ///  2. Создание Файла/Директории:
            ///  -Пользователь может создать новый файл или директорию в указанном месте.
            /// </summary>
            
            Console.Write("\nВведите имя нового файла или директории: ");
            string newItemName = Console.ReadLine();

            string newItemPath = Path.Combine(path, newItemName);

            if (File.Exists(newItemPath) || Directory.Exists(newItemPath))
            {
                Console.WriteLine("Файл или директория с таким именем уже существует.");
            }
            else
            {
                Console.Write("Выберите (1 - файл, 2 - директория): ");
                int choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    File.Create(newItemPath).Close();
                    Console.WriteLine("Файл успешно создан.");
                }
                else if (choice == 2)
                {
                    Directory.CreateDirectory(newItemPath);
                    Console.WriteLine("Директория успешно создана.");
                }
                else
                {
                    Console.WriteLine("Некорректный выбор.");
                }
            }

            /// <summary>
            ///     3. Удаление Файла/Директории:
            ///     -Предоставить возможность удаления файла или директории по указанному пути.
            /// </summary>
            

            Console.Write("\nВведите имя файла или директории для удаления: ");
            string itemToDelete = Console.ReadLine();
            string itemToDeletePath = Path.Combine(path, itemToDelete);

            if (File.Exists(itemToDeletePath))
            {
                File.Delete(itemToDeletePath);
                Console.WriteLine("Файл успешно удален.");
            }
            else if (Directory.Exists(itemToDeletePath))
            {
                Directory.Delete(itemToDeletePath, true);
                Console.WriteLine("Директория успешно удалена.");
            }
            else
            {
                Console.WriteLine("Файл или директория не существует.");
            }

            /// <summary>
            ///    4. Копирование и Перемещение Файлов и Директорий:
            ///    -Реализовать функции для копирования и перемещения файлов и директорий.
            /// </summary>

            Console.Write("\nВведите имя файла или директории для копирования: ");
            string itemToCopy = Console.ReadLine();
            string itemToCopyPath = Path.Combine(path, itemToCopy);

            Console.Write("Введите путь к новой директории (или оставьте пустым для копирования в текущую): ");
            string destinationDirectory = Console.ReadLine();
            string destinationPath = Path.Combine(destinationDirectory, itemToCopy);

            if (File.Exists(itemToCopyPath))
            {
                File.Copy(itemToCopyPath, destinationPath, true);
                Console.WriteLine("Файл успешно скопирован.");
            }
            else if (Directory.Exists(itemToCopyPath))
            {
                // Второй параметр true указывает, что нужно копировать рекурсивно (для директорий)
                CopyDirectory(itemToCopyPath, destinationPath, true);
                Console.WriteLine("Директория успешно скопирована.");
            }
            else
            {
                Console.WriteLine("Файл или директория не существует.");
            }

            /// <summary>
            ///     5. Чтение и Запись в Файл:
            ///     -Предоставить возможность чтения и записи текста в файл.
            /// </summary>

            Console.Write("\nВведите имя файла для чтения/записи: ");
            string fileName = Console.ReadLine();
            string filePath = Path.Combine(path, fileName);

            Console.Write("Выберите действие (1 - чтение, 2 - запись): ");
            int fileAction = int.Parse(Console.ReadLine());

            if (fileAction == 1)
            {
                if (File.Exists(filePath))
                {
                    string content = File.ReadAllText(filePath);
                    Console.WriteLine($"\nСодержимое файла {fileName}:\n{content}");
                }
                else
                {
                    Console.WriteLine($"Файл {fileName} не существует.");
                }
            }
            else if (fileAction == 2)
            {
                Console.Write("Введите текст для записи в файл: ");
                string content = Console.ReadLine();
                File.WriteAllText(filePath, content);
                Console.WriteLine($"Текст успешно записан в файл {fileName}.");
            }
            else
            {
                Console.WriteLine("Некорректный выбор.");
            }
        }
        static void CopyDirectory(string sourceDir, string destDir, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDir);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException($"Исходная директория не существует: {sourceDir}");
            }

            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDir, file.Name);
                file.CopyTo(temppath, true);
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDir, subdir.Name);
                    CopyDirectory(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
