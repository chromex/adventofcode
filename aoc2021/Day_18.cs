using AoCUtil;
using System;
using System.Linq;

namespace aoc2021
{
    class Day_18 : BetterBaseDay
    {
        class Node
        {
            public int val;

            private Node lNodeV;
            public Node lNode
            {
                get => lNodeV;
                set
                {
                    lNodeV = value;
                    if (lNodeV != null)
                        lNodeV.parent = this;
                }
            }

            private Node rNodeV;
            public Node rNode
            {
                get => rNodeV;
                set
                {
                    rNodeV = value;
                    if (rNodeV != null)
                        rNodeV.parent = this;
                }
            }

            public bool IsValue => lNode == null && rNode == null;

            public Node parent;

            public string Print()
            {
                if (lNode != null)
                    return $"[{lNode.Print()},{rNode.Print()}]";
                else
                    return val.ToString();
            }
        }

        private static Node Parse(Parser p)
        {
            Node n = new();

            p.Expect(Symbol.LSqBracket);

            if (p.PeekSymbol() == Symbol.LSqBracket)
                n.lNode = Parse(p);
            else
                n.lNode = new Node() { val = p.GetNumber() };

            p.Expect(Symbol.Comma);

            if (p.PeekSymbol() == Symbol.LSqBracket)
                n.rNode = Parse(p);
            else
                n.rNode = new Node() { val = p.GetNumber() };

            p.Expect(Symbol.RSqBracket);

            return n;
        }

        private static Node FindExplode(Node n, int depth)
        {
            if (!n.IsValue && depth == 5)
            {
                return n;
            }

            Node ret = null;

            if (n.lNode != null)
            {
                ret = FindExplode(n.lNode, depth + 1);
            }
            
            if (ret == null && n.rNode != null)
            {
                ret = FindExplode(n.rNode, depth + 1);
            }    

            return ret;
        }

        private static Node FindSpl(Node current)
        {
            if (current.IsValue)
            {
                if (current.val >= 10)
                {
                    current.lNode = new() { val = current.val / 2 };
                    current.rNode = new() { val = (current.val / 2) + (current.val % 2) };

                    return current;
                }

                return null;
            }

            Node ret = FindSpl(current.lNode);
            if (ret == null)
            {
                ret = FindSpl(current.rNode);
            }

            return ret;
        }

        private static void FlowLeft(Node start)
        {
            Node current = start;

            // Search for the first parent where current is not lNode
            while (current.parent.lNode == current)
            {
                current = current.parent;

                if (current.parent == null)
                {
                    // No more parents to check, no value to the left
                    return;
                }
            }

            current = current.parent.lNode;

            while (!current.IsValue)
            {
                current = current.rNode;
            }

            current.val += start.lNode.val;
        }

        private static void FlowRight(Node start)
        {
            Node current = start;

            // Search for the first parent where current is not rNode
            while (current.parent.rNode == current)
            {
                current = current.parent;

                if (current.parent == null)
                {
                    // No more parents to check, no value to the left
                    return;
                }
            }

            current = current.parent.rNode;

            while (!current.IsValue)
            {
                current = current.lNode;
            }

            current.val += start.rNode.val;
        }

        private static void Reduce(Node root)
        {
            bool found;
            do
            {
                found = false;

                Node expl = FindExplode(root, 1);

                if (expl != null)
                {
                    FlowLeft(expl);
                    FlowRight(expl);

                    expl.val = 0;
                    expl.lNode = null;
                    expl.rNode = null;

                    found = true;
                }
                else
                {
                    found = FindSpl(root) != null;
                }
            } while (found);
        }

        private static int Magnitude(Node current)
        {
            if (current.IsValue)
                return current.val;

            return 3 * Magnitude(current.lNode) + 2 * Magnitude(current.rNode);
        }

        public override string P1()
        {
            Node[] inputs = Input.Select(line => Parse(new Parser(line))).ToArray();

            Node current = inputs[0];
            for (int i = 1; i < inputs.Length; ++i)
            {
                current = new Node() { lNode = current, rNode = inputs[i] };
                Reduce(current);
                Console.WriteLine(current.Print());
            }

            return Magnitude(current).ToString();
        }

        public override string P2()
        {
            int max = 0;

            for (int i = 0; i < Input.Length; ++i)
            {
                for (int j = 0; j < Input.Length; ++j)
                {
                    if (i == j) continue;

                    Node n = new Node() { lNode = Parse(new Parser(Input[i])), rNode = Parse(new Parser(Input[j])) };
                    Reduce(n);
                    int mag = Magnitude(n);
                    if (mag > max)
                    {
                        max = mag;
                    }
                }
            }

            return max.ToString();
        }
    }
}
