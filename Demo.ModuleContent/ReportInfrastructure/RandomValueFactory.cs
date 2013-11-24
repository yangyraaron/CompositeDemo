using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Content
{
    public class RandomValueFactory
    {
        public static string CreateRandomString(int size = 10,bool isLowCase=false)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random(Guid.NewGuid().GetHashCode());
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (isLowCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public static int CreateRandomInt(int minValue = int.MinValue,int maxValue=int.MaxValue)
        {
            Random rd = new Random(Guid.NewGuid().GetHashCode());
            return rd.Next(minValue, maxValue);
        }

        public static decimal CreateRandomDecimal()
        {
            Random rd = new Random(Guid.NewGuid().GetHashCode());
            return (decimal) rd.NextDouble();
        }

        public static Double CreateRandomDouble()
        {
            Random rd = new Random(Guid.NewGuid().GetHashCode());
            return (double)rd.NextDouble();
        }
    }
}
