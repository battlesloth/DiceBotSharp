using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DiceBotSharp.Utility
{
    public class DieRoller
    {

        private Regex regex;
        private Random rng;


        public DieRoller()
        {
            regex = new Regex(@"^(\d*)[dD](\d+)(\+\d+|\-\d+)?$|^[dD]20([-+])$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            rng = new Random();
        }

        public bool TryParseDieRoll(string val, string user, out string msg)
        {
            var match = regex.Match(val);

            if (!match.Success)
            {
                msg = string.Empty;
                return false;
            }

            if (!string.IsNullOrEmpty(match.Groups[4].Value))
            {
                switch (match.Groups[4].Value)
                {
                    case "+":
                        msg = RollAdvantage(user);
                        return true;
                    case "-":
                        msg = RollDisadvantage(user);
                        return true;
                }
            }

            var dieCount = 1;
            var modifier = 0;
            if (!string.IsNullOrEmpty(match.Groups[1].Value))
            {
                dieCount = int.Parse(match.Groups[1].Value);
            }

            var dieSize = int.Parse(match.Groups[2].Value);

            if (!string.IsNullOrEmpty(match.Groups[3].Value))
            {
                modifier = int.Parse(match.Groups[3].Value);
            }

            if (dieSize < 2)
            {
                msg = $"Now {user} is just being foolish.";
            }
            else
            {
                msg = RollDemBones(dieCount, dieSize, modifier, user);
            }

            return true;
        }

        private string RollAdvantage(string user)
        {

            var sb = new StringBuilder();
            var first = rng.Next(1, 21);
            var second = rng.Next(1, 21);

            if (first == 1 && second == 1)
            {
                sb.AppendLine("Wow, two time loser. Double ones. Imagine being that unlucky.");
            }
            if (first > second)
            {
                sb.AppendLine($"{user} rolled D20 with advantage: {first} ({first},{second})");
            }
            else
            {
                sb.AppendLine($"{user} rolled D20 with advantage: {second} ({second},{first})");
            }

            return sb.ToString();
        }

        private string RollDisadvantage(string user)
        {
            var sb = new StringBuilder();
            var first = rng.Next(1, 21);
            var second = rng.Next(1, 21);

            if (first == 1 || second == 1)
            {
                sb.AppendLine("Lamoooooo! Critical miss biatch.");
            }
            if (first < second)
            {
                sb.AppendLine($"{user} rolled D20 with disadvantage: {first} ({first},{second})");
            }
            else
            {
                sb.AppendLine($"{user} rolled D20 with disadvantage: {second} ({second},{first})");
            }

            return sb.ToString();
        }

        private string RollDemBones(int count, int size, int mod, string user)
        {
            try
            {
                var sb = new StringBuilder();
                var rolls = new List<int>();
                
                var result = 0;
                for (int i = 0; i < count; i++)
                {
                    var next = rng.Next(1, size + 1);
                    rolls.Add(next);
                    result += next;
                }

                result += mod;

                sb.Append(user);
                sb.Append(" rolled ");
                sb.Append(count);
                sb.Append("D");
                sb.Append(size);
                
                if (mod != 0)
                {
                    if (mod > 0)
                    {
                        sb.Append("+");
                    }
                    sb.Append(mod);
                }

                sb.Append(": ");
                sb.Append(result);

                if (rolls.Count > 1)
                {
                    sb.Append(" (");
                    sb.Append(string.Join(',', rolls));
                    sb.Append(")");
                }

                return sb.ToString();
            }
            catch (Exception e)
            {
                return $"Hmmmm, sorry {user}, but something went wrong...";
            }
        }
    }
}
