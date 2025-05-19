using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using LoadExcelTOSQL.Entity;

namespace LoadExcelTOSQL.BL
{
    internal class ExcelCompagny
    {
        public List<Address> AdressList = new List<Address>();
        public List<Company> CompagnyList = new List<Company>();
        public List<Employee> PersonyList = new List<Employee>();
        public List<CompanyAddress> CompagnyAdressList = new List<CompanyAddress>();
        //public List<CompagnyPerson> CompagnyPersonyList = new List<CompagnyPerson>();



        public void getComnpagnyDataFromExcel()
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWShProvider;
            Excel.Range rangePr;
                      
            int cCnt;
            int rw = 0;
            int cl = 0;

            string path = @"D:\Send Bill\Send Bill\ExcelFolder\grille_de Suivie A_H_M_2024.xlsx";// ConfigurationManager.AppSettings["PathFollowedGrid"];


            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWShProvider = (Excel.Worksheet)xlWorkBook.Worksheets["Compagny"];

            rangePr = xlWShProvider.UsedRange;
            rw = rangePr.Rows.Count;
            cl = rangePr.Columns.Count;


            for (int i = 3; i <= rw; i++)
            {

                Address AdressTemp = new Address();
                Company CompagnyTemp = new Company();
                Employee EmployeeTemp = new Employee();

                CompanyAddress CompagnyAdressTemp = new CompanyAddress();
              //  CompagnyPerson CompagnyEmployeeTemp = new CompagnyPerson();

                Guid AdressGuid = Guid.NewGuid();
                Guid CompagnyGuid = Guid.NewGuid();
                Guid PersonGuid = Guid.NewGuid();


         /*       CompagnyTemp.CompanyId = CompagnyGuid;
                CompagnyTemp.Name = (string)(rangePr.Cells[i, 1] as Excel.Range).Text;
                CompagnyTemp..companyCode = (string)(rangePr.Cells[i, 2] as Excel.Range).Text;
                CompagnyTemp.companyStatus = (string)(rangePr.Cells[i, 3] as Excel.Range).Text;


                


                AdressTemp.adressID = AdressGuid;
                AdressTemp.civicNumber = (string)(rangePr.Cells[i, 4] as Excel.Range).Text;
                AdressTemp.suite = (string)(rangePr.Cells[i, 5] as Excel.Range).Text;
                AdressTemp.zipCode = (string)(rangePr.Cells[i, 6] as Excel.Range).Text;
                AdressTemp.city = (string)(rangePr.Cells[i, 7] as Excel.Range).Text;
                AdressTemp.state = (string)(rangePr.Cells[i, 8] as Excel.Range).Text;



                EmployeeTemp.EmployeeID = PersonGuid;
                EmployeeTemp.name = (string)(rangePr.Cells[i, 9] as Excel.Range).Text;
                EmployeeTemp.mail = (string)(rangePr.Cells[i, 10] as Excel.Range).Text;
                EmployeeTemp.phone = (string)(rangePr.Cells[i, 11] as Excel.Range).Text;

                CompagnyAdressTemp.adressID = AdressGuid;
                CompagnyAdressTemp.compagnyID = CompagnyGuid;

                CompagnyEmployeeTemp.personID = PersonGuid;
                CompagnyEmployeeTemp.compagnyID = CompagnyGuid;

                AdressList.Add(AdressTemp);
                CompagnyList.Add(CompagnyTemp);
                CompagnyAdressList.Add(CompagnyAdressTemp);

                PersonyList.Add(EmployeeTemp);
                CompagnyPersonyList.Add(CompagnyEmployeeTemp);*/
            }


            //xlWorkBook.Close(true, null, null);
             xlApp.Quit();

            Marshal.ReleaseComObject(xlWShProvider);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);


        }

        public void SaveData()
        {


      /*  CompagnyDBContex compagnyDBContex = new CompagnyDBContex();

            compagnyDBContex.Compagnies.AddRange(CompagnyList);
            compagnyDBContex.Adresses.AddRange(AdressList);
           compagnyDBContex.Employees.AddRange(PersonyList);

            compagnyDBContex.CompagnyAdresses.AddRange(CompagnyAdressList);
            compagnyDBContex.CompagnyPersons.AddRange(CompagnyPersonyList);*/

     /*       string pl =  "person List" + Environment.NewLine;

            foreach (var item in PersonyList)
            {
                pl += item.personID + Environment.NewLine;
            }

            string cpl = "CompagnyPersonyList" + Environment.NewLine; ;

            foreach (var item in CompagnyPersonyList)
            {
                cpl += item.personID + Environment.NewLine;
            }


            //(pl + Environment.NewLine + cpl);
     */

           // compagnyDBContex.SaveChanges();

        }


        public void GetWorkedDayCompagny()
        {

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWShProvider;
            Excel.Range rangePr;

            int cCnt;
            int rw = 0;
            int cl = 0;

            string path = @"D:\Send Bill\Send Bill\ExcelFolder\grille_de Suivie A_H_M_2024.xlsx";// ConfigurationManager.AppSettings["PathFollowedGrid"];


            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWShProvider = (Excel.Worksheet)xlWorkBook.Worksheets["January"];

            rangePr = xlWShProvider.UsedRange;
            rw = rangePr.Rows.Count;
            cl = rangePr.Columns.Count;






            //xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWShProvider);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);




        }


    }
}
