using System;
using System.Collections.Generic;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        Node root = Node.BuildTree();
        Console.WriteLine(Node.PrintTree(root));


        int result = root.Minimax(root, int.MinValue, int.MaxValue, true);

        Console.ReadLine();
    }
}



class Node
{
    List<Node> children;

    protected int value;

    public Node(int value)
    {
        this.value = value;
    }

    public Node(List<Node> children)
    {
        children.Reverse();

        this.children = children;
    }


    public void addChild(Node child)
    {
        children.Add(child);
    }

    public static Node BuildTree()
    {
         int[] values = new int[] { 3, 5, 0, 7, 5, 4, 7, 8 };
        Node[] leaves = new Node[values.Length];


        for (int i = 0; i < values.Length; i++)
        {
            leaves[i] = new Leave(values[i]);
        }

        Node t1 = new Node(new List<Node> { leaves[0], leaves[1] });
        Node t8 = new Node(new List<Node> { leaves[2], leaves[3] });
        Node t5 = new Node(new List<Node> { t8, leaves[4] });
        Node t7 = new Node(new List<Node> { leaves[6], leaves[7] });
        Node t2 = new Node(new List<Node> { t5, leaves[5], t7 });

        return new Node(new List<Node> { t1, t2 });
    }


    public static string  BuildPrintTree(Node node)
    {
        return node.ToString();
    }

    public static string PrintTree(Node node)
    {
       return BuildPrintTree(node);
    }


    public int Minimax(Node n, int alpha, int beta, bool maximizing)
    {

        Console.WriteLine("Node: " + n.ToString() + ", alpha: " + alpha.ToString() + ", beta: " + beta.ToString() + ", maximizing: " + maximizing.ToString());

        if (n is Leave)
        {
            return n.value;
        }



        if (maximizing)
        {
            int maxEval = int.MinValue;


            foreach (Node c in n.children)
            {
                int eval = Minimax(c, alpha, beta, false);
                maxEval = Math.Max(maxEval, eval);
                alpha = Math.Max(alpha, eval);

                if (beta <= alpha)
                {
                    Console.WriteLine($"pruning... on {c.ToString()}");
                    break;
                }
            }


            Console.WriteLine("value chosen: " + maxEval.ToString());

            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;

            foreach (Node c in n.children)
            {
                int eval = Minimax(c, alpha, beta, true);
                minEval = Math.Min(minEval, eval);
                beta = Math.Min(beta, eval);
                if (beta <= alpha)
                {
                    Console.WriteLine($"pruning... on {c.ToString()}");
                    break;
                }
            }

            Console.WriteLine("value chosen: " + minEval.ToString());
            return minEval;
        }



    }

    public virtual string ToString()
    {
        string s = "(";
        foreach (Node child in children)
        {
            s += BuildPrintTree(child) + ", ";
        }

        s = s.Substring(0, s.Length - 2);
        s += ")";

        return s;

    }

}

class Leave : Node
{
    public Leave(int value) : base(value)
    {
    }

    public override string ToString()
    {
        return value.ToString();
    }
}