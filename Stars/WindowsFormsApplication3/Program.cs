using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

// What this does atm - 
// reads the .csv file of HYG star catalogue and presents it as a table
// what it will do - 
// plot the positions of all stars with distance and x,y,z data, using a 
// simple spatial plot system, hopefully this can be done without dx.

public class Star
{
    public int HYG_ID;
    public int HIP_ID;
    public int HD_ID;
    public int HR_ID;
    public string Gliese_Name;
    public string BayerFlamsteed_Name;
    public string Proper_Name;
    public decimal RA;
    public decimal Dec;
    public decimal Distance;    /* crucial - what i acually need */
    public decimal PMRA;
    public decimal PMDec;
    public decimal RV;
    public decimal Mag;
    public decimal AbsMag;
    public string Spectrum;
    public decimal ColorIndex;
    public decimal X;       /* crucial - what i acually need */
    public decimal Y;       /* crucial - what i acually need */
    public decimal Z;       /* crucial - what i acually need */
    public decimal VX;      /* crucial - what i acually need */
    public decimal VY;      /* crucial - what i acually need */
    public decimal VZ;      /* crucial - what i acually need */
}  // not yet used

namespace WindowsFormsApplication3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
