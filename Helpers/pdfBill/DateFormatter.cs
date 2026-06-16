using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Helpers.pdfBill
{
    public class DateFormatter
    {
        
        public int getCorrecteNameOfMouth(string choice)
        {
            switch (choice)
            {
                case "January":
                    return 01;

                case "February":
                    return 2;

                case "March":
                    return 3;

                case "April":
                    return 4;

                case "May":
                    return 5;

                case "June":
                    return 6;

                case "July":
                    return 7;

                case "August":
                    return 8;

                case "September":
                    return 9;

                case "October":
                    return 10;

                case "November":
                    return 11;

                case "December":
                    return 12;
            }
            return 0;
        }   

    }
}
