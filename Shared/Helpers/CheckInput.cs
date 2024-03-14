namespace Shared;

public static class CheckInput
{
        public static bool IsValid(string input)
        {
            IEnumerable<string> banneds = [";","-","/","|","or","*","%","+","=","'","and","#"];

            foreach (var banned in banneds)
            {
                if(input.ToLower().Contains(banned))
                {
                    return false;
                }
            }
            return true;
        }
}
