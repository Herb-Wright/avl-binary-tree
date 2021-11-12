using System;

namespace avl_binary_tree
{
    class Program
    {
        static void Main(string[] args)
        {
            bool is_running = true;
            Binary_Tree Tree = new Binary_Tree();
            while(is_running)
            {
                Console.WriteLine("type 'add', 'delete', 'reset', 'print', or 'quit' to do stuff:");
                switch (Console.ReadLine().ToLower())
                {
                    case "add":
                        Console.WriteLine("what # do you want to add");
                        int x;
                        if(Int32.TryParse(Console.ReadLine(), out x))
                        {
                            Tree.add(x);
                            Console.WriteLine("success");
                        }
                        else
                        {
                            Console.WriteLine("that was not an integer, dummy");
                        }
                        break;
                    case "delete":
                        Console.WriteLine("what # do you want to delete?");
                        int y;
                        if (Int32.TryParse(Console.ReadLine(), out y))
                        {
                            Tree.delete(y);
                            Console.WriteLine("success");
                        }
                        else
                        {
                            Console.WriteLine("that was not an integer, silly");
                        }
                        break;
                    case "reset":
                        Tree = new Binary_Tree();
                        Console.WriteLine("it has been done");
                        break;
                    case "print":
                        Console.WriteLine(Tree.to_string());
                        break;
                    case "quit":
                        is_running = false;
                        break;
                    default:
                        Console.WriteLine("that's not a real command, loser");
                        break;
                }
            }
            Console.WriteLine("thanks for stopping by");
        }
    }
}
