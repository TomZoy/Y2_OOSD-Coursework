﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GCUBankModel;

namespace GCUBankGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Account account;
        string branch = "99-99-99";
        ObservableCollection<ITransaction> transactions;

        public MainWindow()
        {
            InitializeComponent();
            account = GetAccount();
            transactions = new ObservableCollection<ITransaction>();
            DataContext = account;
            lstTransactions.ItemsSource = transactions;
            txtBalance.Text = String.Format(" Balance {0:c}", account.GetBalance());
        }

        private Account GetAccount()
        {
            return new Account("00999999", 100.0m);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            // TO DO: enclose this code in an exception-handling construct
            // so that it catches any DebitException thrown and uses a 
            // message box to report the problem to the user. The message
            // should include the Amount of the transaction which has been
            // rejected

            // TO DO: also catch exceptions thrown if the user enters text
            // which is not a valid number

            decimal amount = Convert.ToDecimal(txtAmount.Text);
            string type = cmbType.Text;

            ITransaction trans;
            if (type == "Credit")
            {
                trans = new CreditTransaction(
                    TransactionIDSequence.Instance.NextID,
                    DateTime.Now,
                    amount,
                    branch);
            }
            else 
            {
                trans = new WithdrawalTransaction(
                    TransactionIDSequence.Instance.NextID,
                    DateTime.Now,
                    amount,
                    branch);
            }
            account.AddTransaction(trans);
            transactions.Add(trans);
            txtBalance.Text = String.Format(" Balance {0:c}", account.GetBalance());

        }

    }
}
