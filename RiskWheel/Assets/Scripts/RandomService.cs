using System;

public static class RandomService
{
   public static int GetInt(int minInclusive, int maxExclusive)
   {
       return new Random().Next(minInclusive, maxExclusive);
   }
}
