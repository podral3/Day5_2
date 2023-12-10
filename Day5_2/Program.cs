//string bruh = File.ReadAllText("D:\\projekty\\Advent\\Day5_2\\test.txt", System.Text.Encoding.UTF8);
string bruh = File.ReadAllText("D:\\projekty\\Advent\\Day5_2\\input.txt", System.Text.Encoding.UTF8);

string[] tempSplit = bruh.Split("\r\n\r\n\r");
string[] seedsString = tempSplit[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
long[] seeds = seedsString.Select(e => Int64.Parse(e)).ToArray();
string[] blocks = tempSplit[1].Split("\r\n\r").Select(e => e.Substring(e.IndexOf(':') + 1)).ToArray();
string[][] segmentsForMapping = blocks.Select(e => e.Split("\r\n", StringSplitOptions.RemoveEmptyEntries)).ToArray();//chuj wie po co to


//long[] seeds = tempSplit[0].Split(' ', StringSplitOptions.TrimEntries).Select(e => Int64.Parse(e)).ToArray();
List<long[]> seedGroups = new List<long[]>(); 

for(long i = 0; i < seeds.Length-1; i += 2)
{
    long seedRange = seeds[i+1];
    long[] seedGroup = new long[seedRange - 1];
    for(long j = 0; j < seedRange - 1; j++)
    {
        seedGroup[j] = seeds[i] + j; // tyk urwo
    }
    seedGroups.Add(seedGroup);
}

List<long[]> sadad = seedGroups; //debug

//lista z segmentami, nie ruszać
List<List<List<long>>> rangesBySegment = new List<List<List<long>>>();
List<long> destinations = new List<long>();
for (int i = 0; i < segmentsForMapping.Length; i++)
{
    List<List<long>> ranges = new List<List<long>>();
    for (int j = 0; j < segmentsForMapping[i].Length; j++)
    {
        List<long> integers = new List<long>();
        string[] ouh = segmentsForMapping[i][j].Split(' ', StringSplitOptions.TrimEntries);
        for (int k = 0; k < ouh.Length; k++) integers.Add(Int64.Parse(ouh[k]));
        ranges.Add(integers);
    }
    rangesBySegment.Add(ranges);
}
//TAKI CHUJ
foreach (long[] seedGroup in seedGroups)
{
    foreach (long seed in seedGroup)
    {
        long numberToMap = seed;
        foreach (List<List<long>> segment in rangesBySegment)
        {

            foreach (List<long> ranges in segment)
            {
                long destStart = ranges[0];
                long sourceStart = ranges[1];
                long range = ranges[2];
                if (numberToMap >= sourceStart && numberToMap <= sourceStart + range - 1)
                {
                    long odleglosc = numberToMap - sourceStart; //do testow
                    numberToMap = destStart + odleglosc;
                    break;
                }
            }
            //Console.Write($"{numberToMap} ");
        }
        Console.WriteLine(seedGroups.IndexOf(seedGroup));
        destinations.Add(numberToMap);
    }
    Console.WriteLine(seedGroups.IndexOf(seedGroup));

}
Console.WriteLine(destinations.Min());