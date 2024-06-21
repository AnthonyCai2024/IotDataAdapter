namespace DataConvertConApp;

public class TimeHelper
{
    public static void Test()
    {
        var cTime = "2024-06-21 08:39:47.8950062";


        var createTime = Convert.ToDateTime(cTime);
        var curTime = DateTime.Now;

        // var result = createTime.CompareTo(curTime);
        //
        // Console.WriteLine($"createTime:{createTime},curTime:{curTime},result:{result}");
        //
        // DateTime time1 = DateTime.Now;
        // DateTime time2 = DateTime.Now.AddMilliseconds(1234); // Example: 1234 milliseconds later

        // Calculate the difference
        TimeSpan difference = curTime - createTime;

        // Get the total milliseconds
        double millisecondsDifference = difference.TotalMilliseconds;

        Console.WriteLine($"Time1: {curTime}");
        Console.WriteLine($"Time2: {createTime}");
        Console.WriteLine($"Difference: {difference}");
        Console.WriteLine($"Difference in milliseconds: {millisecondsDifference}");

    }
}