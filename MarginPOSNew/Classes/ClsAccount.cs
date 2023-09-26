using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarginPOSNew.Classes
{
    public class ClsAccount
    {
        public Boolean IsValidGSTNo(object sender)
        {
            MetroTextBox curTxt = (MetroTextBox)sender;
            if (string.IsNullOrWhiteSpace(curTxt.Text))
            { 
                //bool isValid_PanCardNo = 0;
                Regex GstNo = new Regex(@"[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[0-9]{1}[a-zA-Z]{1}[0-9]{1}");
                //Verify whether date entered in dd/MM/yyyy format.
                bool isValid_GstdNo = GstNo.IsMatch(curTxt.Text.Trim());
                if (!isValid_GstdNo)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public Boolean IsValidPAN(object sender)
        {
            MetroTextBox curTxt = (MetroTextBox)sender;
            if (string.IsNullOrWhiteSpace(curTxt.Text))
            {
                //bool isValid_PanCardNo = 0;
                Regex PANCardNo = new Regex(@"^([A-Z]){5}([0-9]){4}([A-Z]){1}?$"); //(@"^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$") 

                //Verify whether date entered in dd/MM/yyyy format.
                bool isValid_PanCardNo = PANCardNo.IsMatch(curTxt.Text.Trim());
                if (!isValid_PanCardNo)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

    }
}

