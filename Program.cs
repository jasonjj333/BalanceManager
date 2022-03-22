using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BalanceManager
{
    class Program {
        static void Main() {
            Random random = new Random();

            double maxCashLimit = 5000; //hardcoded
            int maxItems = 10; //hardcoded, change to itemGenerator maxItems
            double sum = 0; //test variable to verify that credit amounts = debit
            double creditAmount = 0;
            int numSplits = random.Next(1,maxItems);

            maxCashLimit = Math.Round(maxCashLimit, 2); //round maxCashLimit to 2 decimal places
            double debitTotal = random.NextDouble()*(maxCashLimit-1) + 1;
            debitTotal = Math.Round(debitTotal, 2);
            double debitBalance = debitTotal;
            int numSplitsSkipped = 0; //if debit amount is paid off before reaching max # of splits

            //generate random amount of splits
            Console.WriteLine("Number of Splits: " + numSplits + " Debit Amount: " + debitBalance);
            for(int i = numSplits; i >= 1; i--) {
                if(debitBalance >0.009) {
                    Console.WriteLine("Current split:" + i);
                    creditAmount = generateBalance(debitBalance, i);
                    Console.WriteLine("Credit Amount: "+ creditAmount);
                    debitBalance = debitBalance - creditAmount;
                    sum+=creditAmount;
                }
                else {
                    //do nothing
                    numSplitsSkipped++;
                }
            }
            Console.WriteLine("Sum = " + sum + " Debit Balance = " + debitBalance);
            Console.WriteLine("Number of Splits skipped: " + numSplitsSkipped);
            Console.WriteLine(generateTextAmount(debitTotal));
        }
        //***UPDATE if there is no remaining debit balance for remaining splits, then terminate***
        //ex. total: 250.1, split 1: 249.1, split 2: 1, split 3: 0, split 4: 0, split 5: 0, remove split 3 through 5
        static double generateBalance(double debitBalance, int numSplits) {
            //generate random credit for particular split
            Random rand = new Random();
            double creditAmount = 0.0;
            if(numSplits==1 || debitBalance <= 1) {
                return Math.Round(debitBalance, 2);
            }
            else {
                creditAmount = rand.NextDouble()*(debitBalance-1) + 1;
                creditAmount = Math.Round(creditAmount, 2);
                return creditAmount;
            }
        }
        
        //***UPDATE***
        //***OPTIONAL: change digitPlace to account for digits above 4. ex. 40,523 is 5 digits***
        //***is "," present in amount passed? ex. 4,999 or is it 4999. Currently doesnt account for ","
        static string generateTextAmount(double amount) {
            string result = "";
            string textAmount = amount.ToString();
            string wholeNumber = "";
            string decimalNumber = "";
            int indexOfDecimal = 0;
            Console.WriteLine("---Converting number to text---");
            Console.WriteLine("Numeric Amount: " + amount);

            //Split Amount into Two variables before and after decimal 250.10 -> 250 and 10
            if(textAmount.Contains(".")) {
                indexOfDecimal = textAmount.IndexOf(".");
                wholeNumber = textAmount.Substring(0, indexOfDecimal);
                decimalNumber = textAmount.Substring(indexOfDecimal+1, textAmount.Length-(indexOfDecimal+1));
                Console.WriteLine("WholeNumber: " + wholeNumber + " DecimalNumber: " + decimalNumber);
            }

            //Convert Whole Number to Text
            Boolean isFirstDigit = true;
            Boolean tenToNineteenValueUsed = false;
            int digitPlace = wholeNumber.Length;
            for(int i = 0; i < wholeNumber.Length; i ++) {
                //thousandsth place
                string digit = wholeNumber.Substring(i,1);
                if(digitPlace == 4) {
                    if(digit.Equals("0"))
                    {//do nothing
                    }
                    else if(isFirstDigit) {
                        result+= convertCapitalSingleDigit(digit) + " thousand ";
                        isFirstDigit = false;
                    }
                    else {
                        result+= convertSingleDigit(digit) + " thousand ";
                    }
                    digitPlace--;
                }
                else if(digitPlace == 3) {
                    if(digit.Equals("0"))
                    {//do nothing
                    }
                    else if(isFirstDigit) {
                        result += convertCapitalSingleDigit(digit) + " hundred ";
                        isFirstDigit = false;
                    }
                    else {
                        result += convertSingleDigit(digit) + " hundred ";
                    }
                    digitPlace--;
                }
                else if(digitPlace == 2) {
                    if(digit.Equals("0"))
                    {//do nothing
                    }
                    else if(isFirstDigit) {
                        if(digit.Equals("1")) {
                            result += convertCapitalTenToNineteen(wholeNumber.Substring(i,2)) + " ";
                            isFirstDigit = false;
                            tenToNineteenValueUsed = true;
                        }
                        else {
                            result += convertCapitalSecondDigit(digit) + " ";
                        }
                    }
                    else {
                        if(digit.Equals("0"))
                        {//do nothing
                        }
                        if(digit.Equals("1")) {
                            result += convertTenToNineteen(wholeNumber.Substring(i,2)) + " ";
                            tenToNineteenValueUsed = true;
                        }
                        else {
                            result += convertSecondDigit(digit) + " ";
                        }
                    }
                    digitPlace--;
                }
                else if(digitPlace == 1) {
                    if(digit.Equals("0"))
                    {//do nothing
                    }
                    else if(!tenToNineteenValueUsed){
                        if(isFirstDigit) {
                            result+= convertCapitalSingleDigit(digit)+" ";
                            isFirstDigit = false;
                        }
                        else {
                            result+= convertSingleDigit(digit)+ " ";
                        }
                    }
                    digitPlace--;
                }
            }

            //Add Decimal value
            if (!decimalNumber.Equals("00") && !decimalNumber.Equals("0") && decimalNumber.Length != 0) {
                result += "and " + decimalNumber + "/100";
            }

            return result;
        }

        static string convertSingleDigit(string amount) {
            if(amount.Equals("1"))
                return "one";
            else if(amount.Equals("2"))
                return "two";
            else if(amount.Equals("3"))
                return "three";
            else if(amount.Equals("4"))
                return "four";
            else if(amount.Equals("5"))
                return "five";
            else if(amount.Equals("6"))
                return "six";
            else if(amount.Equals("7"))
                return "seven";
            else if(amount.Equals("8"))
                return "eight";
            else if(amount.Equals("9"))
                return "nine";
            return "";

        }
        static string convertCapitalSingleDigit(string amount) {
            if(amount.Equals("1"))
                return "One";
            else if(amount.Equals("2"))
                return "Two";
            else if(amount.Equals("3"))
                return "Three";
            else if(amount.Equals("4"))
                return "Four";
            else if(amount.Equals("5"))
                return "Five";
            else if(amount.Equals("6"))
                return "Six";
            else if(amount.Equals("7"))
                return "Seven";
            else if(amount.Equals("8"))
                return "Eight";
            else if(amount.Equals("9"))
                return "Nine";
            return "";

        }
        static string convertTenToNineteen(string amount) {
            if(amount.Equals("10"))
                return "ten";
            else if(amount.Equals("11"))
                return "eleven";
            else if(amount.Equals("12"))
                return "twelve";
            else if(amount.Equals("13"))
                return "thirteen";
            else if(amount.Equals("14"))
                return "fourteen";
            else if(amount.Equals("15"))
                return "fifteen";
            else if(amount.Equals("16"))
                return "sixteen";
            else if(amount.Equals("17"))
                return "seventeen";
            else if(amount.Equals("18"))
                return "eighteen";
            else if(amount.Equals("19"))
                return "nineteen";
            return "";

        }
        static string convertCapitalTenToNineteen(string amount) {
            if(amount.Equals("10"))
                return "Ten";
            else if(amount.Equals("11"))
                return "Eleven";
            else if(amount.Equals("12"))
                return "Twelve";
            else if(amount.Equals("13"))
                return "Thirteen";
            else if(amount.Equals("14"))
                return "Fourteen";
            else if(amount.Equals("15"))
                return "Fifteen";
            else if(amount.Equals("16"))
                return "Sixteen";
            else if(amount.Equals("17"))
                return "Seventeen";
            else if(amount.Equals("18"))
                return "Eighteen";
            else if(amount.Equals("19"))
                return "Nineteen";
            return "";

        }

        static string convertSecondDigit(string amount) {
    
            if(amount.Equals("2"))
                return "twenty";
            else if(amount.Equals("3"))
                return "thirty";
            else if(amount.Equals("4"))
                return "fourty";
            else if(amount.Equals("5"))
                return "fifty";
            else if(amount.Equals("6"))
                return "sixty";
            else if(amount.Equals("7"))
                return "seventy";
            else if(amount.Equals("8"))
                return "eighty";
            else if(amount.Equals("9"))
                return "ninety";
            return "";

        }
        static string convertCapitalSecondDigit(string amount) {
    
            if(amount.Equals("2"))
                return "Twenty";
            else if(amount.Equals("3"))
                return "Thirty";
            else if(amount.Equals("4"))
                return "Fourty";
            else if(amount.Equals("5"))
                return "Fifty";
            else if(amount.Equals("6"))
                return "Sixty";
            else if(amount.Equals("7"))
                return "Seventy";
            else if(amount.Equals("8"))
                return "Eighty";
            else if(amount.Equals("9"))
                return "Ninety";
            return "";

        }
    }
}