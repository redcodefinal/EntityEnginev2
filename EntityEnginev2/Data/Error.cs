using System;
using EntityEnginev2.Engine;

namespace EntityEnginev2.Data
{
    public static class Error
    {
        public static bool SupressErrors;

        public static void Warning(string message)
        {
            Console.WriteLine("Warning: " + message);
        }

        public static void Warning(string message, Entity sender)
        {
            Console.WriteLine("Warning: " + message + "->" + sender.Name);
        }

        public static void Exception(string message)
        {
            Console.WriteLine("Error: " + message);
            if (!SupressErrors)
                throw new Exception(message);
        }

        public static void Exception(string message, Entity sender)
        {
            Console.WriteLine("Error: " + message + "->" + sender.Name);
            if (!SupressErrors)
                throw new Exception(message + "->" + sender.Name);
        }
    }
}