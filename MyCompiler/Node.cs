using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace MyCompiler
{
    public abstract class Node
    {
        abstract public Expression Execute();
    }

    public abstract class TerminalNode : Node
    {
        public double Value { get; set; }
    }

    public abstract class NonTerminalNode : Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }
        protected int priority;

        public bool IsNotEmpty()
        {
            return ((Left != null) && (Right != null));
        }
        public int GetPriority()
        {
            return priority;
        }
    }

    public class ValueNode :  TerminalNode
    {

        public ValueNode(double _value)
        {
            Value = _value;
        }

        public override Expression Execute()
        {
            return Expression.Constant(Value);
        }
    }

    public class ParameterNode : TerminalNode
    {
        public Type T;
        public string Name;

        public ParameterNode(Type t, string name)
        {
            T = t;
            Name = name;
        }


        public override Expression Execute()
        {

            return Expression.Parameter(T, Name);
            throw new NotImplementedException();
        }
    }

    public class AddNode : NonTerminalNode
    { 
        public AddNode()
        {
            Left = Right = null;
            priority = 2;
            
        }

        public AddNode(Node _left, Node _right)
        {

            Left = _left;
            Right = _right;
            priority = 2;

        }

        public override Expression Execute()
        {
            return Expression.Add(Left.Execute(), Right.Execute());
            
        }

    }

    public class SubtractNode : NonTerminalNode
    {
        public SubtractNode()
        {
            Left = Right = null;
            priority = 2;
        }

        public SubtractNode(Node _left, Node _right)
        {
            Left = _left;
            Right = _right;
            priority = 2;
        }

        public override Expression Execute()
        {
            return Expression.Subtract(Left.Execute(), Right.Execute());
        }
    }

    public class MultiplyNode : NonTerminalNode
    {
        public MultiplyNode()
        {
            Left = Right = null;
            priority = 1;
        }

        public MultiplyNode(Node _left, Node _right)
        {
            priority = 1;
            Left = _left;
            Right = _right;

        }

        public override Expression Execute()
        {
            return Expression.Multiply(Left.Execute(), Right.Execute());

        }
    }

    public class DivideNode : NonTerminalNode
    {
      public DivideNode()
        {
            Left = Right = null;
            priority = 1;
        }

        public DivideNode(Node _left, Node _right)
        {

            Left = _left;
            Right = _right;
            priority = 1;
        }

        public override Expression Execute()
        {
            return Expression.Divide(Left.Execute(), Right.Execute());

        }
    }

    public class PowerNode : NonTerminalNode
    {
        public PowerNode()
        {
            Left = Right = null;
            priority = 0;
        }

        public PowerNode(Node _base, Node _exp)
        {

            Left = _base;
            Right = _exp;
            priority = 0;
        }

        public override Expression Execute()
        {
            return Expression.Power(Left.Execute(), Right.Execute());

        }
    }
}
