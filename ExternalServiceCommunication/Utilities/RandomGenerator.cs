namespace ExternalServiceCommunication.Utilities
{
    public static class RandomGenerator
    {


        public static string String()
        {
            string possibleCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            int stringLength = 8;
            return GenerateRandomString(possibleCharacters, stringLength);
        }

        private static string GenerateRandomString(string possibleCharacters, int length)
        {
            char[] randomStringArray = new char[length];
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(possibleCharacters.Length);
                randomStringArray[i] = possibleCharacters[index];
            }

            return new string(randomStringArray);
        }
    }


}
