Console.WriteLine("Level ID or Serial Number :");
string x = Console.ReadLine();
x = x.ToUpper().Replace("-", "").Trim();


//Level ID
if (isLevelId(x))
    Console.WriteLine(x + " -> " + getLevelId(x));
//Serial Number
else if (isLevelSN(x))
{
    int i = 0;
    while (i <= 20)
    {
        string n = (int.Parse(x) + i).ToString();
        Console.WriteLine(n + " : " + getLevelNum(n)); ;
        i++;
    }
}

else
    Console.WriteLine("Wrong Level ID or Serial Number.");

Console.ReadLine();
Environment.Exit(0);



string getLevelId(string x) {
    double wValueSum = 0;
    for (int i = 0; i < 9; i++)
    {
        wValueSum = wValueSum + thi2Dec(x[i]) * Math.Pow(30, i);
    }
    string binValueSum = Convert.ToString((long)wValueSum, 2);

    //string idA = binValueSum.Substring(0,4) ;
    //string idB = binValueSum.Substring(4, 6);
    string idC = binValueSum.Substring(10, 20);
    //string idD = binValueSum.Substring(30, 1);
    //string idE = binValueSum.Substring(31, 1);
    string idF = binValueSum.Substring(32, 12);

    string binOriLevelSN = idF + idC;
    string binConst = "00010110100000001110000001111100"; //1680E07C
    string binXorResult = xoring(binOriLevelSN, binConst, binConst.Length);

    long output = Convert.ToInt32(binXorResult, 2);
    return output.ToString();
}





string getLevelNum(string x) {
    string binStr = Convert.ToString(long.Parse(x), 2);
    binStr = binStr.PadLeft(32, '0');

    string binConst = "00010110100000001110000001111100"; //1680E07C
    string binXorResult = xoring(binStr, binConst, binConst.Length);

    string idA = "1000";
    string idB = Convert.ToString(((long.Parse(x) - 31) % 64), 2).PadLeft(6, '0');
    string idC = binXorResult.Substring(12, 20);
    string idD = "0";
    string idE = "1";
    string idF = binXorResult.Substring(0, 12);

    Int64 num = Convert.ToInt64((idA + idB + idC + idD + idE + idF), 2);
    double[] dArray = new double[9];
    for (int i = 0; i < 9; i++)
    {
        dArray[i] = num % Math.Pow(30, i + 1);
        num = num - (Int64)dArray[i];
        dArray[i] = dArray[i] / Math.Pow(30, i);
    }

    string output = dec2Thi((int)dArray[0]) + dec2Thi((int)dArray[1]) + dec2Thi((int)dArray[2]) + "-"
                      + dec2Thi((int)dArray[3]) + dec2Thi((int)dArray[4]) + dec2Thi((int)dArray[5]) + "-"
                      + dec2Thi((int)dArray[6]) + dec2Thi((int)dArray[7]) + dec2Thi((int)dArray[8]);

    return output;
}



Boolean isLevelId(string str)
{
    str = str.ToUpper().Replace("-", "").Trim();
    if (str.Length != 9) 
    { 
        return false;
    }
    else {
        for (int i = 0; i < 9; i++)
        {
            if (thi2Dec(x[i]) < 0)
                return false;
        }
        return true;
    }
        
}

Boolean isLevelSN(string str)
{
    int n;
    bool isNumeric = int.TryParse(str, out n);
    
    return isNumeric;
}

int thi2Dec(char str) {
    string[] strArray = {"0","1","2","3","4","5","6","7","8","9","B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Y" };

    int index = Array.IndexOf(strArray, str.ToString());

    if (index > -1)
    {
        return index;
    }
    else
    {
        return -1;
    }
}

string dec2Thi(int num)
{
    string[] strArray = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Y" };

    if (num < 30)
        return strArray[num];
    else
        return "null";
}

string xoring(string a, string b, int n)
{
    string ans = "";
    for (int i = 0; i < n; i++)
    {
        if (a[i] == b[i])
            ans += "0";
        else
            ans += "1";
    }
    return ans;
}