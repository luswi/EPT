﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EPT.classes;
using System.IO;
using System.Xml.Serialization;

namespace EPT
{
    public partial class EPT : Form
    {
        public EPT()
        {
            InitializeComponent();
            //------------------
            // Number "." only
            //------------------
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
        }


        //----------------------------------------------
        // Check if number and only one "." for input.
        //----------------------------------------------
        private void ProtectText(TextBox txt, KeyPressEventArgs e)
        {
            bool IsNumber = false;
            string text = txt.Text;
            if(e.KeyChar=='\b')
            {
                e.Handled = false;
            }
            else
            {
                if(!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                    IsNumber = true;
                }
                if (!IsNumber)
                {
                    if(e.KeyChar.ToString() == ".")
                    {
                        if(text.IndexOf(".") == -1)
                        {
                            e.Handled = false;
                        }
                    }
                }
            }
        }
        //------------
        // Protect 
        //------------
        private void zdkTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            ProtectText(zdkTB, e);
        }




        //-------------------
        // Main APP section
        //-------------------

        seaClass v = new seaClass();

        private void calculateBT_Click(object sender, EventArgs e)
        {
            // Pick up values from textboxes
            v.zdk = double.Parse(zdkTB.Text);
            v.zfdk = double.Parse(zfdkTB.Text);
            



            // Results
            bool success = v.seaCalculation(v);
            if (success == true)
            {
                
                cCalcTB.Text = Convert.ToString(v.cCalc);

            }
            else
            {
                MessageBox.Show("Something goes wrong!");
            }
        }



        //--------------
        // Save to XML
        //--------------
        private void saveXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Information info = new Information();
                
                info.Data1 = cCalcTB.Text;
                // -->

                SaveXML.SaveData(info, "data.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //--------------------
        // Load data at start
        //--------------------
        private void EPT_Load(object sender, EventArgs e)
        {
            if(File.Exists("data.xml"))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Information));
                FileStream read = new FileStream("data.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                Information info = (Information)xs.Deserialize(read);
                cCalcTB.Text = info.Data1;
            }
        }
    }
}
