namespace MatchIt.Script.Utils
{
    public static class Random2
    {
        public static string GenerateRandomString(int length)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] randomChars = new char[length];
            System.Random random = new System.Random(); // Alternatively, use UnityEngine.Random

            for (int i = 0; i < length; i++)
            {
                randomChars[i] = characters[random.Next(characters.Length)];
            }

            return new string(randomChars);
        }
    }
}