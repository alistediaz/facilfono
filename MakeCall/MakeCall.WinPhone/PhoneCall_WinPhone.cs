using System;
using System.IO;
using System.Windows;

using Xamarin.Forms;
using MakeCall.WinPhone;
using Microsoft.Phone.Tasks;

[assembly: Dependency(typeof(PhoneCall_WinPhone))]
namespace MakeCall.WinPhone
{
    public class PhoneCall_WinPhone : IPhoneCall
    {
        public void MakeQuickCall(string PhoneNumber)
        {
            try
            {
                PhoneCallTask phoneCallTask = new PhoneCallTask();
                phoneCallTask.PhoneNumber = PhoneNumber;
                phoneCallTask.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Windows Exception", MessageBoxButton.OK);
            }
        }
    }
}
