using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;



namespace MyCompiler
{
    class Parcer
    {
        public Node Parce(string input)
        {
           
            Regex r = new Regex(@"^[\d*(.\d*) \s \+ \- \* \/ \^ a-z A-Z]*$", RegexOptions.Compiled);
            if (!r.IsMatch(input))
            {
                throw new ArgumentException("Input string contains wrong symbols");
            }

            List<Node> tokens = new List<Node>();

            foreach (Match m in Regex.Matches(input, @"(\d+(.\d*)?|\+|\-|\*|\/|\^|[a-zA-Z]+)"))
            {  
                Node n = StringToNode(m.Value);
                if (n != null)
                    tokens.Add(n);             
            }

            Node rootNode = Merge(tokens);
            
            return rootNode;
        }

        public Node Merge (List<Node> nodes)
        {
            if ((nodes is null) || nodes.Count == 0)
                throw new ArgumentNullException("Can't merge empty list");

          
            for (int priority = 0; priority < 3; priority++)
            {
                List<Node> temp = new List<Node>();           

                for (int i = 0 ; i < nodes.Count; i++)
                {
                    if (nodes[i] is TerminalNode t)
                    {
                        temp.Add(t);
                       
                    }
                    if (nodes[i] is NonTerminalNode sign)
                    {
                        if (sign.GetPriority() != priority)
                        {
                            temp.Add(sign);
                        }
                        else
                        {                          
                            if ((temp.Count == 0)||(i == nodes.Count-1))
                                throw new ArgumentException("missing left or right operand");

                            else if (IsValidArgument(temp[temp.Count - 1]) && IsValidArgument(nodes[i + 1]))
                            {
                                sign.Left = temp[temp.Count - 1];
                                sign.Right = nodes[i + 1];
                                temp[temp.Count - 1] = sign;
                                i++;
                            }
                            else throw new ArgumentException("left or right expression is not valid argument");
                        }
                    }
                }
                nodes = temp;
            }
            return nodes[0];
        }

        private bool IsValidArgument(Node n)
        {
            
            if (n is null)
            {
                return false;
            }
            if (n is TerminalNode v)
            {
                return true; }
            if (n is NonTerminalNode sign)
            {
                return sign.IsNotEmpty();
            }
            return false;
        }

        public Node StringToNode(string s)
        {
            s.Replace(" ", string.Empty);

            Match N = Regex.Match(s, @"(\+|\-|\*|\/|\^)");
            if (N.Success)
            {
                switch (N.Value)
                {
                    case "+":                       
                           return new AddNode();
                    case "-":
                        return new SubtractNode();
                    case "*":
                        return new MultiplyNode();
                    case "/":
                        return new DivideNode();
                    case "^":
                        return new PowerNode();
                    default:
                        break;
                }
            }

            Match Param = Regex.Match(s, @"[a-zA-Z]+");
            if (Param.Success)
            {
                return new ParameterNode( typeof(double));
            }

            Match M = Regex.Match(s, @"\d+(.\d*)?");
            if (M.Success)
            {
                return new ValueNode(Double.Parse(M.Value, CultureInfo.GetCultureInfo("en-GB")));
            }

            return null;
        }
    }
}
