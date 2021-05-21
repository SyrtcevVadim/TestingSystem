using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TestingSystem
{
    class TaskReader
    {
        /// <summary>
        /// Название задачи
        /// </summary>
        string taskName;
        /// <summary>
        /// Название задачи
        /// </summary>
        public string TaskName
        {
            get
            {
                return taskName;
            }
        }
        /// <summary>
        /// Описание задачи
        /// </summary>
        string taskDesription;
        /// <summary>
        /// Описание задачи
        /// </summary>
        public string TaskDescription
        {
            get
            {
                return taskDesription;
            }
        }

        /// <summary>
        /// Пример входных данных
        /// </summary>
        string inputExample;
        /// <summary>
        /// Пример входных данных
        /// </summary>
        public string InputExample
        {
            get
            {
                return inputExample;
            }
        }
        /// <summary>
        /// Пример выходных данных
        /// </summary>
        string outputExample;
        /// <summary>
        /// Пример выходных данных
        /// </summary>
        public string OutputExample
        {
            get
            {
                return outputExample;
            }
        }
        public TaskReader(string pathToTaskFile)
        {
            //Открываем файл для чтения

             FileStream file = new FileStream(pathToTaskFile, FileMode.Open);
             string info;
            using (StreamReader sr = new StreamReader(file))
             {
                info = sr.ReadToEnd();
             }
 



            // Разбираем строку на составляющие
            string[] peaces = info.Split("#");
            // 1 строка - имя задачи
            // 2 строка - описание задачи
            // 3 строка - пример входных данных
            // 4 строка - пример выходных данных
            taskName = peaces[0];
            taskDesription = peaces[1];
            inputExample = peaces[2];
            outputExample = peaces[3];
            file.Close();
        }
    }
}
