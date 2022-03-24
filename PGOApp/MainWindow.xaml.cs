using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PGOApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<DbModel> database = new List<DbModel>();
        DbController db = new DbController();
        public MainWindow()
        {
            InitializeComponent();
            database.ForEach(item => resultsListbox.Items.Add(item));
            resultsListbox.DisplayMemberPath = "FullInfo";
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            resultsListbox.Items.Clear();
            database = db.SearchAccount(usernameSearchText.Text);
            database.ForEach(item => resultsListbox.Items.Add(item));
            resultsListbox.DisplayMemberPath = "FullInfo";

        }

        private void FindAllButton_Click(object sender, RoutedEventArgs e)
        {
            resultsListbox.Items.Clear();
            database = db.FindAllAccounts();
            database.ForEach(item => resultsListbox.Items.Add(item));
            resultsListbox.DisplayMemberPath = "FullInfo";
        }


        private void AddRecordButton_Click(object sender, RoutedEventArgs e)
        {

            TrimText();

            if (AreFieldsComplete())
            {

                bool sold, listed;
                int soldint, listedint;

                if (SoldCheckbox.IsChecked == true)
                    soldint = 1;
                else
                    soldint = 0;

                if (ListedCheckBox.IsChecked == true)
                    listedint = 1;
                else
                    listedint = 0;

                string compared = db.CompareUsernames(UsernameText.Text);

                if (compared == "" || compared == null)
                {
                    db.AddAccount(UsernameText.Text, PasswordText.Text, EmailText.Text, EmailPassText.Text, DetailsText.Text, soldint, listedint);
                    ClearAllTextboxes();
                    FindAllButton_Click(sender, e);
                }
                else
                    MessageBox.Show($"ACCOUNT ALREADY EXISTS IN DB");

            }
            else
            {
                MessageBox.Show("PLEASE FILL IN ALL TEXTBOXES");
            }

        }

        private void DeleteRecordButton_Click(object sender, RoutedEventArgs e)
        {
            TrimText();
            if (MessageBox.Show("DELETE THIS RECORD FROM THE DB?", "DELETION WARNING!!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //no

            }
            else
            {
                string compared = db.CompareUsernames(UsernameText.Text);
                if (compared == UsernameText.Text)
                {
                    if (!string.IsNullOrWhiteSpace(UsernameText.Text) && UsernameText.Text != "")
                    {
                        db.DeleteAccount(UsernameText.Text);
                        FindAllButton_Click(sender, e);
                        ClearAllTextboxes();
                    }
                }
                else
                    MessageBox.Show("USERNAME DOES NOT EXIST IN DB.");
            }


        }
        private void UpdateRecordButton_Click(object sender, RoutedEventArgs e)
        {
            TrimText(); if (MessageBox.Show("UPDATE THIS RECORD IN THE DB?", "OVERWRITE WARNING!!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //no

            }
            else
            {
                bool sold, listed;
                int soldint, listedint;

                if (SoldCheckbox.IsChecked == true)
                    soldint = 1;
                else
                    soldint = 0;

                if (ListedCheckBox.IsChecked == true)
                    listedint = 1;
                else
                    listedint = 0;

                db.UpdateAccount(UsernameText.Text, PasswordText.Text, EmailText.Text, EmailPassText.Text, DetailsText.Text, soldint, listedint);
                FindAllButton_Click(sender, e);
            }
        }

        private bool AreFieldsComplete()
        {
            if (UsernameText.Text != "" && PasswordText.Text != "" && EmailText.Text != "" && EmailPassText.Text != "" && DetailsText.Text != "")
            {
                return true;
            }
            else
                return false;
        }

        #region TEXT MANIPULATION
        private void TrimText()
        {
            var strList = new List<TextBox>() { UsernameText, PasswordText, EmailText, EmailPassText, DetailsText };

            foreach(TextBox textbox in strList)
            {
                if(string.IsNullOrWhiteSpace(textbox.Text))
                {
                    textbox.Text = "";
                }
            }
        }
        private void ClearAllTextboxes()
        {
            var strList = new List<TextBox>() { UsernameText, PasswordText, EmailText, EmailPassText, DetailsText }; 
            
            foreach(TextBox textbox in strList)
            {
                textbox.Text = ""; 
            }
            ListedCheckBox.IsChecked = false;
            SoldCheckbox.IsChecked = false;
        }
        #endregion

        private void resultsListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DbModel item = (DbModel)resultsListbox.SelectedItem;

            if (item != null)
            {

                if (MessageBox.Show("LOAD INFO FROM DB?", "DATABASE REQUEST", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //no

                }
                else
                {
                    //yes
                    UsernameText.Text = item.Username;
                    PasswordText.Text = item.Password;
                    EmailText.Text = item.Email;
                    EmailPassText.Text = item.Email_Pass;
                    DetailsText.Text = item.Details;
                    if (item.Sold)
                        SoldCheckbox.IsChecked = true;
                    else
                        SoldCheckbox.IsChecked = false;
                    if (item.Listed)
                        ListedCheckBox.IsChecked = true;
                    else
                        ListedCheckBox.IsChecked = false;

                }
            }
        }

    }
}
