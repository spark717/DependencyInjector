using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spark
{
    public class CircularDependencyGuard
    {
        private readonly HashSet<Type> _blacklist = new HashSet<Type>(16);
        private readonly Stack<Type> _history = new Stack<Type>(16);

        public void Append(Type type)
        {
            if (_blacklist.Contains(type))
                throw new Exception($"Circular dependency detected for type <{type.Name}>{GetHistoryString()}");

            _blacklist.Add(type);
            _history.Push(type);
        }

        public void RemoveTop()
        {
            var type = _history.Pop();
            _blacklist.Remove(type);
        }

        public string GetHistoryString()
        {
            var sb = new StringBuilder();

            sb.AppendLine();
            sb.AppendLine("History:");
            
            foreach (var type in _history.Reverse())
            {
                sb.AppendLine(type.Name);
            }

            return sb.ToString();
        }
    }
}